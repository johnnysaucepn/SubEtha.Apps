﻿namespace Howatworks.Matrix.Domain
{
    public interface IShipState : IState
    {
        long ShipId { get; set; }
        string Type { get; set; }
        string Name { get; set; }
        string Ident { get; set; }
        bool? ShieldsUp { get; set; }
        decimal? HullIntegrity { get; set; }
    }
}