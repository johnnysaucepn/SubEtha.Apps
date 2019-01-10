﻿namespace Howatworks.PlayerJournal.Serialization.Combat
{
    public class Interdiction : JournalEntryBase
    {
        public bool Success { get; set; }
        public string Interdicted { get; set; } // NOTE: Commander name
        public bool IsPlayer { get; set; }
        public int CombatRank { get; set; }
        public string Faction { get; set; } // Note: faction name
        public string Power { get; set; } // Note: power name
    }
}
