﻿using System.Diagnostics.CodeAnalysis;

namespace Howatworks.PlayerJournal.Serialization.Travel
{
    public class DockingDenied : JournalEntryBase
    {
        public string StationName { get; set; }
        public string StationType { get; set; }
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public long MarketID { get; set; } // TODO: check data type
        // TODO: consider enum - NoSpace, TooLarge, Hostile, Offences, Distance, ActiveFighter, NoReason
        public string Reason { get; set; }
    }
}
