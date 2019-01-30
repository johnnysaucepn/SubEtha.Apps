﻿using System.Diagnostics.CodeAnalysis;

namespace Howatworks.PlayerJournal.Serialization.Startup
{
    public class ClearSavedGame : JournalEntryBase
    {
        public string Name { get; set; }    // NOTE: Commander name
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public string FID { get; set; }     // NOTE: Player ID
    }
}
