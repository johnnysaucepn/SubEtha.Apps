﻿namespace Howatworks.SubEtha.Journal.Other
{
    public class SendText : JournalEntryBase
    {
        public string To { get; set; } // Note: player name, or channel name
        public string Message { get; set; }
    }
}