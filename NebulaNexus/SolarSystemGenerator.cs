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
            "Andromeda", "Nova Ecliptic Realm", "Hyperion Star Cluster", "Astralis", "Shili", "Nova System", "Umbraflora Haven", "Galaxion"
        };

        private static HashSet<Planet> usedPlanets = new HashSet<Planet>();
        private static int seed = (int)DateTime.Now.Ticks;
        private Random rnd = new Random(seed);

        StarGeneratorManager sgManager = new StarGeneratorManager();
        PlanetGeneratorManager pgManager = new PlanetGeneratorManager();

        public SolarSystem GenerateSolarSystem()
        {
            var radius = GenerateRadius();
            var solarSystem = new SolarSystem(GenerateName(), radius, GenerateCoord());
            solarSystem.MainStar = sgManager.CreateStar(solarSystem);


            if (pgManager.PossiblePlanetNames.Count > 0)
            {
                for (int i = 0; i < rnd.Next(1, pgManager.PossiblePlanetNames.Count / 2); i++)
                {
                    solarSystem.Planets.Add(pgManager.CreatePlanet(solarSystem));
                }
            }

            return solarSystem;
        }

        private string GenerateName()
        {
            var chosenName = PossibleSolarSystems[rnd.Next(PossibleSolarSystems.Count)];
            PossibleSolarSystems.Remove(chosenName);
            return chosenName;
        }

        private Coordinate GenerateCoord()
        {
            var x = ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, 30) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);
            var y = ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, 30) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);
            var z = ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, 30) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);

            return new Coordinate(x, y, z);
        }

        private long GenerateRadius()
        {
            var maxValue = 1.51e12;
            var minValue = 5.1e4;
            var value = (minValue + rnd.NextDouble() * (maxValue - minValue));
            return (long) value;
        }

        public static List<Planet> AssignPlanets(List<Planet> planets)
        {
            var rnd = new Random();

            int remainingPlanetsCount = planets.Count - usedPlanets.Count;


            double[] probabilities = {0.20, 0.25, 0.5, 0.05};
            var probabilitiesSum = probabilities.Sum();
            int[] possibleDividers = {2, 3, 4, 5};

            var randomValue = rnd.NextDouble() * probabilitiesSum;

            int maxCount = 2;

            for (int i = 0; i < possibleDividers.Length; i++)
            {
                randomValue -= probabilities[i];

                if (randomValue <= 0)
                {
                    maxCount = remainingPlanetsCount / possibleDividers[i];
                    Debugger.AssignedPlanetsModifier.Add(i);
                }
            }

            List<Planet> localList = new List<Planet>();

            // EnsureSystem(planets, usedPlanets);

            while (localList.Count < maxCount && usedPlanets.Count < planets.Count)
            {
                var randomIndex = rnd.Next(planets.Count);
                var item = planets[randomIndex];

                if (!usedPlanets.Contains(item))
                {
                    localList.Add(item);
                    usedPlanets.Add(item);
                }
            }

            return localList;
        }
    }
}