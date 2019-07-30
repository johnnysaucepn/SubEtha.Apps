using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Howatworks.SubEtha.Monitor;
using log4net;
using Newtonsoft.Json;

namespace Howatworks.Thumb.Core
{
    public class JsonJournalMonitorState : IJournalMonitorState
    {
        private const string StorageFile = @"journalmonitor.json";

        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonJournalMonitorState));

        private readonly JsonSerializer _serializer = new JsonSerializer
        {
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        private Lazy<InMemoryJournalMonitorState> _state;

        /// <summary>
        /// Update the internal representation and save
        /// </summary>
        public DateTimeOffset? LastRead
        {
            get => _state.Value.LastRead;
            set
            {
                _state.Value.LastRead = value;
                Save();
            }
        }

        /// <summary>
        /// Update the internal representation and save
        /// </summary>
        public DateTimeOffset? LastChecked
        {
            get => _state.Value.LastChecked;
            set
            {
                _state.Value.LastChecked = value;
                Save();
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public JsonJournalMonitorState()
        {
            _state = new Lazy<InMemoryJournalMonitorState>(Load);
        }

        private InMemoryJournalMonitorState Load()
        {
            try
            {
                using (var stream = File.OpenRead(StorageFile))
                using (var reader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(reader))
                {
                    return _serializer.Deserialize<InMemoryJournalMonitorState>(jsonReader);
                }
            }
            catch (IOException ex)
            {
                Log.Warn(ex);
                return null;
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        private void Save()
        {

            try
            {
                using (var stream = File.OpenWrite(StorageFile))
                using (var writer = new StreamWriter(stream))
                {
                    _serializer.Serialize(writer, _state);
                }
            }
            catch (IOException ex)
            {
                Log.Warn(ex);
                _state = null;
            }
        }
    }
}