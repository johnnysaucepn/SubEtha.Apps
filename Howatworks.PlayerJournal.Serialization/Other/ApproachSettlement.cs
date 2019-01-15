﻿using System.Diagnostics.CodeAnalysis;

namespace Howatworks.PlayerJournal.Serialization.Other
{
    public class ApproachSettlement : JournalEntryBase
    {
        public string Name { get; set; }
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public long MarketID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
