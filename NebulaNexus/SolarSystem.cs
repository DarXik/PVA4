using System;
using System.Collections.Generic;
using System.Numerics;

namespace NebulaNexus
{
    // DODĚLAT CELOU UNKNOWN VARIANTU U VŠEHO , "unknown"
    public class SolarSystem : IGameObject
    {
        public string Name { get; }
        public int Id { get; }
        public Coordinate Coordinates { get; }
        public float RadiusLY { get; }
        public long Radius { get; }
        public List<Planet> Planets { get; set; }
        public Star MainStar { get; set; }

        public SolarSystem(string name, long radius, BigInteger x, BigInteger y, BigInteger z)
        {
            Planets = new List<Planet>();
            Name = name;
            Id = UniqueID.GenerateID();
            Coordinates = new Coordinate(x, y, z);
            Radius = radius;
            RadiusLY = (float) (radius / 9.461e12);
        }
    }
}