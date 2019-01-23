﻿using System.Diagnostics.CodeAnalysis;

namespace Howatworks.PlayerJournal.Serialization.StationServices
{
    public class SellShipOnRebuy : JournalEntryBase
    {
        public string ShipType { get; set; }
        public string System { get; set; }
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public int SellShipID { get; set; } // TODO: Check capitalisation
        public long ShipPrice { get; set; }
    }
}
