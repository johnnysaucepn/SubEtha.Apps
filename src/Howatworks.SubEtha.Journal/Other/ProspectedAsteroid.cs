﻿namespace Howatworks.SubEtha.Journal.Other
{
    public class ProspectedAsteroid : JournalEntryBase
    {
        public class MaterialItem
        {
            public string Name { get; set; }
            public decimal Proportion { get; set; } // TODO: check data type
        }

        public string Content { get; set; } // TODO: consider enum (High/Medium/Low)

        public string MotherlodeMaterial { get; set; }
        public decimal Remaining { get; set; } // TODO: check data type
    }
}