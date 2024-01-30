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
        public Coordinate Coordinates { get; set; }
        public float RadiusLY { get; }
        public long Radius { get; }
        public List<Planet> Planets { get; set; }
        public Star MainStar { get; set; }

        public SolarSystem(string name, long radius)
        {
            Planets = new List<Planet>();
            Name = name;
            Id = UniqueID.GenerateID();
            Coordinates = new Coordinate();
            Radius = radius;
            RadiusLY = (float) (radius / 9.461e12);
        }
        public override string ToString() // pro selection u travel
        {
            return Name;
        }

    }
}