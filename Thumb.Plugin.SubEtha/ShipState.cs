﻿using System;
using SubEtha.Domain;

namespace Thumb.Plugin.SubEtha
{
    public class ShipState : IShipState
    {
        public DateTime TimeStamp { get; set; }

        public string Type { get; set; }
        public int ShipID { get; set; }
        public string Name { get; set; }
        public string Ident { get; set; }
        public bool? ShieldsUp { get; set; }
        public decimal? HullIntegrity { get; set; }

        public ShipState()
        {
            HullIntegrity = 1;
        }
    }
}
