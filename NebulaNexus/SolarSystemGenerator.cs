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

        private static HashSet<Planet> usedPlanets = new HashSet<Planet>();
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
                var modifier = rnd.Next(1, pgManager.PossiblePlanetNames.Count / rnd.Next(1, pgManager.PossiblePlanetNames.Count));
                AssignedPlanetsModifier.Add(modifier);
                modifier = 2;
                for (int i = 0; i < modifier; i++)
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
            var value = (minValue + rnd.NextDouble() * (maxValue - minValue));
            return (long) value;
        }

        // public static List<Planet> AssignPlanets(List<Planet> planets)
        // {
        //     var rnd = new Random();
        //
        //     int remainingPlanetsCount = planets.Count - usedPlanets.Count;
        //
        //
        //     double[] probabilities = {0.20, 0.25, 0.5, 0.05};
        //     var probabilitiesSum = probabilities.Sum();
        //     int[] possibleDividers = {2, 3, 4, 5};
        //
        //     var randomValue = rnd.NextDouble() * probabilitiesSum;
        //
        //     int maxCount = 2;
        //
        //     for (int i = 0; i < possibleDividers.Length; i++)
        //     {
        //         randomValue -= probabilities[i];
        //
        //         if (randomValue <= 0)
        //         {
        //             maxCount = remainingPlanetsCount / possibleDividers[i];
        //             Debugger.AssignedPlanetsModifier.Add(i);
        //         }
        //     }
        //
        //     List<Planet> localList = new List<Planet>();
        //
        //     // EnsureSystem(planets, usedPlanets);
        //
        //     while (localList.Count < maxCount && usedPlanets.Count < planets.Count)
        //     {
        //         var randomIndex = rnd.Next(planets.Count);
        //         var item = planets[randomIndex];
        //
        //         if (!usedPlanets.Contains(item))
        //         {
        //             localList.Add(item);
        //             usedPlanets.Add(item);
        //         }
        //     }
        //
        //     return localList;
        // }
    }
}