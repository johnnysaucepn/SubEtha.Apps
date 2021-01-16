﻿using log4net;
using System;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Assistant.WebSockets
{
    public class AssistantWebSocketHandler : WebSocketHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AssistantWebSocketHandler));

        private readonly ISubject<IncomingMessage> _messageReceived = new Subject<IncomingMessage>();
        public IObservable<IncomingMessage> MessageReceived => _messageReceived.AsObservable();

        private readonly ISubject<ConnectionChangeEvent> _connectionChanges = new Subject<ConnectionChangeEvent>();
        public IObservable<ConnectionChangeEvent> ConnectionChanges => _connectionChanges.AsObservable();

        public AssistantWebSocketHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket).ConfigureAwait(false);

            var socketId = WebSocketConnectionManager.GetId(socket);
            Log.Info($"Connected '{socketId}'");
            _connectionChanges.OnNext(new ConnectionChangeEvent(socketId, ConnectionChange.Connected));
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            await base.OnDisconnected(socket).ConfigureAwait(false);

            var socketId = WebSocketConnectionManager.GetId(socket);
            Log.Info($"Disconnected '{socketId}'");
            _connectionChanges.OnNext(new ConnectionChangeEvent(socketId, ConnectionChange.Disconnected));
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var rawString = Encoding.UTF8.GetString(buffer, 0, result.Count);

            Log.Info($"Received '{rawString}' from '{socketId}'");

            _messageReceived.OnNext(new IncomingMessage(socketId, rawString));
        }
    }
}