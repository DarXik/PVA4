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
        public float Radius { get; }
        public BigInteger RadiusKm;
        public List<Planet> Planets { get; set; }
        public Star MainStar { get; }

        public SolarSystem(string name, float radius, List<Planet> planets, Star mainStar, BigInteger x, BigInteger y, BigInteger z)
        {
            Planets = planets;
            MainStar = mainStar;
            mainStar.SolarSystem = this; // potřebuje setter u Star ??
            Name = name;
            Id = UniqueID.GenerateID();
            Coordinates = new Coordinate(x, y, z);
            Radius = radius;
            RadiusKm = (BigInteger) (Radius * 9.461 * Math.Pow(10, 12));

            foreach (var planet in Planets)
            {
                planet.SolarSystem = this;
            }
        }
    }
}