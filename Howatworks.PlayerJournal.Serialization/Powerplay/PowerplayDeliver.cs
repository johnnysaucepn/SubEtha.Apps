﻿namespace Howatworks.PlayerJournal.Serialization.Powerplay
{
    public class PowerplayDeliver : JournalEntryBase
    {
        public string Power { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}
