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

        public SolarSystem(string name, float radius, List<Planet> planets, Star mainStar, float x, float y, float z)
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

    public class SolarSystemGenerator
    {
        public static readonly List<string> PossibleSolarSystems = new List<string>()
        {
            "Andromeda", "Nova Ecliptic Realm", "Hyperion Star Cluster", "Astralis", "Shili", "Nova System", "Umbraflora Haven", "Galaxion"
        };

        private Random rnd = new Random();

        public SolarSystem GenerateSolarSystem(List<Planet> planets, Star star)
        {
            return new SolarSystem(GenerateName(), GenerateRadius(), planets, star, GenerateCoord(), GenerateCoord(), GenerateCoord());
        }

        private string GenerateName()
        {
            var chosenName = PossibleSolarSystems[rnd.Next(PossibleSolarSystems.Count)];
            PossibleSolarSystems.Remove(chosenName);
            return chosenName;
        }

        private float GenerateCoord()
        {
            var modifier = rnd.Next(0, 2) == 0 ? -1 : 1;

            // return (long) (rnd.NextDouble() + long.MaxValue);
            return (float) rnd.NextDouble();
        }

        private float GenerateRadius()
        {
            // return (float) ((rnd.NextDouble() + 0.4) * Math.Pow(10, -rnd.Next(2, 5)));
            return (float) rnd.NextDouble();
        }
    }
}