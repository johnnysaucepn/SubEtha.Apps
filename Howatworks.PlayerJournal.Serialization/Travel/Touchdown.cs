﻿namespace Howatworks.PlayerJournal.Serialization.Travel
{
    public class Touchdown : JournalEntryBase
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool PlayerControlled { get; set; }
    }
}
