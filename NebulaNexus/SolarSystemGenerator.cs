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
        private Random rnd = new Random();

        StarGeneratorManager sgManager = new StarGeneratorManager();
        public SolarSystem GenerateSolarSystem()
        {
            var solarSystem = new SolarSystem(GenerateName(), GenerateRadius(), GenerateCoord(), GenerateCoord(), GenerateCoord());
            solarSystem.MainStar = sgManager.CreateStar(solarSystem);
            return solarSystem;
        }

        private string GenerateName()
        {
            var chosenName = PossibleSolarSystems[rnd.Next(PossibleSolarSystems.Count)];
            PossibleSolarSystems.Remove(chosenName);
            return chosenName;
        }

        private BigInteger GenerateCoord()
        {
            int modifier_1 = rnd.Next(0, 2) == 0 ? -1 : 1;

            // return (long) ((long) (M.ath.Pow(2, rnd.Next(30, 50))) *  * modifier_1);
            return (BigInteger) (((rnd.NextDouble() + 0.4) * Math.Pow(10, rnd.Next(10, 15))) * modifier_1 * 10000);
        }

        private long GenerateRadius()
        {
            var maxValue = 1.51e12;
            var minValue = 5.1e4;
            var value = (minValue + rnd.NextDouble() * (maxValue - minValue));
            Console.WriteLine(value);
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