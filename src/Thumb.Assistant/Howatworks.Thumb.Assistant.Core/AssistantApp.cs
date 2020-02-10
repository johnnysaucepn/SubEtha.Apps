﻿using System;
using System.IO;
using Howatworks.SubEtha.Bindings;
using Howatworks.SubEtha.Monitor;
using Howatworks.Thumb.Assistant.Core.Messages;
using Howatworks.Thumb.Core;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Howatworks.Thumb.Assistant.Core
{
    public class AssistantApp : IThumbApp
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AssistantApp));

        private readonly JournalMonitorScheduler _monitor;
        private readonly IThumbNotifier _notifier;
        private readonly JournalEntryRouter _router;

        private readonly IConfiguration _configuration;
        private readonly WebSocketConnectionManager _connectionManager;
        private readonly StatusManager _statusManager;
        private readonly GameControlBridge _keyboard;
        private BindingMapper _bindingMapper;

        public AssistantApp(
            JournalMonitorScheduler monitor,
            IThumbNotifier notifier,
            JournalEntryRouter router,
            IConfiguration configuration,
            WebSocketConnectionManager connectionManager,
            StatusManager statusManager,
            GameControlBridge keyboard)
        {
            _monitor = monitor;
            _notifier = notifier;
            _router = router;
            _configuration = configuration;
            _connectionManager = connectionManager;
            _statusManager = statusManager;
            _keyboard = keyboard;
        }

        public void Initialize()
        {
            Log.Info("Starting up");

            _monitor.JournalEntriesParsed += (sender, args) =>
            {
                if (args == null) return;
                _router.Apply(args.Entries, args.BatchMode);
            };
            _monitor.JournalFileWatchingStarted += (sender, args) => _notifier.Notify(NotificationPriority.High, NotificationEventType.FileSystem, $"Started watching '{args.Path}'");

            _monitor.JournalFileWatchingStopped += (sender, args) => _notifier.Notify(NotificationPriority.Medium, NotificationEventType.FileSystem, $"Stopped watching '{args.Path}'");

            var bindingsPath = Path.Combine(_configuration["BindingsFolder"], _configuration["BindingsFilename"]);

            _bindingMapper = BindingMapper.FromFile(bindingsPath);

            _connectionManager.MessageReceived += (_, args) =>
            {
                var messageWrapper = JObject.Parse(args.Message);
                switch (messageWrapper["MessageType"].Value<string>())
                {
                    case "ActivateBinding":
                        var controlRequest = messageWrapper["MessageContent"].ToObject<ControlRequest>();
                        ActivateBinding(controlRequest);
                        break;
                    case "GetAvailableBindings":
                        var bindingList = _bindingMapper.GetBoundButtons("Keyboard", "Mouse");
                        var serializedMessage = JsonConvert.SerializeObject(new
                            {
                                MessageType = "AvailableBindings",
                                MessageContent = bindingList
                            },
                            Formatting.Indented);
                        _connectionManager.SendMessageToAllClients(serializedMessage);
                        break;
                    default:
                        Log.Warn($"Unrecognised message format: {args.Message}");
                        break;
                }
            };

            _connectionManager.ClientConnected += (sender, args) =>
            {
                var serializedMessage = JsonConvert.SerializeObject(new
                    {
                        MessageType = "ControlState",
                        MessageContent = _statusManager.CreateControlStateMessage()
                    },
                    Formatting.Indented);
                _connectionManager.SendMessageToAllClients(serializedMessage);
            };

            _statusManager.ControlStateChanged += (_, args) =>
            {
                _notifier.Notify(NotificationPriority.High, NotificationEventType.Update, "Updated ship status");
                var serializedMessage = JsonConvert.SerializeObject(new
                    {
                        MessageType = "ControlState", MessageContent = StatusManager.CreateControlStateMessage(args.State)
                    },
                    Formatting.Indented);
                _connectionManager.SendMessageToAllClients(serializedMessage);
            };

            var hostBuilder = new WebHostBuilder()
                .UseConfiguration(_configuration)
                // Use our existing Autofac context in the web app services
                .ConfigureServices(services => services.AddSingleton(_connectionManager))
                .UseStartup<Startup>()
                .UseKestrel()
                .UseUrls("http://*:5984");

            var host = hostBuilder.Build();

            host.RunAsync().ConfigureAwait(false); // Don't block the calling thread

            StartMonitoring();
        }

        public void Shutdown()
        {
            Log.Info("Shutting down");
            StopMonitoring();
        }

        public void StartMonitoring()
        {
            Log.Info("Starting monitoring");
            _monitor.Start();
        }

        public void StopMonitoring()
        {
            Log.Info("Stopping monitoring");
            _monitor.Stop();
        }

        public DateTimeOffset? LastEntry()
        {
            return _monitor.LastEntry();
        }

        public DateTimeOffset? LastChecked()
        {
            return _monitor.LastChecked();
        }

        private void ActivateBinding(ControlRequest controlRequest)
        {
            Log.Info($"Activated a control: '{controlRequest.BindingName}'");

            var button = _bindingMapper.GetButtonBindingByName(controlRequest.BindingName);
            if (button == null)
            {
                Log.Warn($"Unknown binding name found: '{controlRequest.BindingName}'");
            }
            else
            {
                _keyboard.TriggerKeyCombination(button);
            }
        }
    }
}