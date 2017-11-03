﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Howatworks.PlayerJournal.StationServices
{
    public class MissionAccepted : JournalEntryBase
    {
        // TODO: is this localised?
        public string Name { get; set; }
        public string Faction { get; set; }
        public int MissionID { get; set; }

        #region Optional
        public string Commodity { get; set; }
        public string Commodity_Localised { get; set; }
        public int Count { get; set; }
        public string Target { get; set; }
        public string TargetType { get; set; }
        public string TargetFaction { get; set; }
        public DateTime Expiry { get; set; }
        public string DestinationSystem { get; set; }
        public string DestinationStation { get; set; }
        public int PassengerCount { get; set; }
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool PassengerVIPs { get; set; }
        public bool PassengerWanted { get; set; }
        // TODO: is this localised?
        public string PassengerType { get; set; }
        #endregion

    }
}
