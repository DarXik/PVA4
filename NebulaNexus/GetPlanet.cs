using System;
using System.Collections.Generic;

namespace NebulaNexus
{
    public static class GetPlanet
    {
        private static HashSet<Planet> usedPlanets = new HashSet<Planet>();

        public static List<Planet> GetPlanets(List<Planet> planets)
        {
            var rnd = new Random();

            int remainingPlanetsCount = planets.Count - usedPlanets.Count;

            int maxCount = remainingPlanetsCount / 2;

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