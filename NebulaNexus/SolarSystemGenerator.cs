using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace NebulaNexus
{
    public class SolarSystemGenerator : Debugger
    {
        public static readonly List<string> PossibleSolarSystems = new List<string>()
        {
            "Andromeda", "Nuovo Ecliptic Realm", "Hyperion Star Cluster", "Astralis", "Shili", "Nova System", "Umbraflora Haven", "Galaxion"
        };

        private static int seed = (int) DateTime.Now.Ticks;
        private Random rnd = new Random(seed);

        StarGeneratorManager sgManager = new StarGeneratorManager();
        PlanetGeneratorManager pgManager = new PlanetGeneratorManager();

        public SolarSystem GenerateSolarSystem()
        {
            var radius = GenerateRadius();
            var solarSystem1 = new SolarSystem(GenerateName(), radius);
            solarSystem1.MainStar = sgManager.CreateStar(solarSystem1);
            solarSystem1.Coordinates = CoordinateGenerator.PossibleCoordsSystem(radius, solarSystem1);

            if (pgManager.PossiblePlanetNames.Count > 0)
            {
                var modifier = rnd.Next(1, pgManager.PossiblePlanetNames.Count / 2);
                AssignedPlanetsModifier.Add(modifier);
                // modifier = 2;
                for (var i = 0; i < modifier; i++)
                {
                    solarSystem1.Planets.Add(pgManager.CreatePlanet(solarSystem1));
                }
            }

            return solarSystem1;
        }

        private string GenerateName()
        {
            var chosenName = PossibleSolarSystems[rnd.Next(PossibleSolarSystems.Count)];
            PossibleSolarSystems.Remove(chosenName);
            return chosenName;
        }

        private long GenerateRadius()
        {
            var maxValue = 1.51e12;
            var minValue = 5.1e4;
            var value = minValue + rnd.NextDouble() * (maxValue - minValue);
            return (long) value;
        }
    }
}