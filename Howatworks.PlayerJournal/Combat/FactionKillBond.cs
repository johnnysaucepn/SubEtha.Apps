﻿namespace Howatworks.PlayerJournal.Combat
{
    public class FactionKillBond : JournalEntryBase
    {
        public int Reward { get; set; }
        public string AwardingFaction { get; set; }
        public string VictimFaction { get; set; }
    }
}
