using System;
using System.Collections.Generic;
using System.Linq;

namespace NebulaNexus
{
    public class PlanetGeneratorManager
    {
        Random rnd = new Random();
        private string generatedType;

        public Planet CreatePlanet(int id)
        {
            string[] nameAndSystem = GenerateSolarSystem();
            generatedType = GenerateType();
            long generatedRadius = GenerateRadius();

            var planet1 = new Planet(nameAndSystem[0], generatedRadius, generatedType, GeneratePopulation(generatedRadius),
                GenerateTechnologicalLevel(), GenerateMilitaryPower(),
                GenerateDemocracy(), nameAndSystem[1],
                GenerateX(), GenerateY(), GenerateZ(), id);

            return planet1;
        }

        private string GenerateName(bool doubleWordSystem, string systemPrefix)
        {
            var possiblePlanetNames = new List<string>()
            {
                "Nexus", "Aldoria", "Celestaria", "Orionis", "Lunaris Prime",
                "Nova", "Astrionex", "Umbraflux", "Astoria", "Epsilon",
                "Haven", "Maris", "Lagoon", "Echoes", "Pluto", "Voltria", "Spectra", "Xenepha"
            };

            int randomIndex = rnd.Next(possiblePlanetNames.Count());
            var splitList = possiblePlanetNames[randomIndex].Split(' ');
            string chosenItem = possiblePlanetNames[randomIndex];

            possiblePlanetNames.Remove(chosenItem);

            if (doubleWordSystem && systemPrefix.Length > 0) // dvojitý název
            {
                return systemPrefix + " " + chosenItem;
            }
            else
            {
                return chosenItem;
            }
        }

        private string[] GenerateSolarSystem()
        {
            string[] NameAndSystem = new string[2];
            string[] possibleSolarSystems =
            {
                "Andromeda", "Nova Ecliptic Realm", "Hyperion Star Cluster", "Astralis", "Shili", "Nova System", "Umbraflora Haven", "Galaxion", "unknown"
            };

            int randomIndex = rnd.Next(possibleSolarSystems.Length);
            var splitArray = possibleSolarSystems[randomIndex].Split(' ');

            if (splitArray.Length > 1) // dvojitý název
            {
                NameAndSystem[0] = GenerateName(true, splitArray[0]);
                NameAndSystem[1] = possibleSolarSystems[randomIndex];
            }
            else
            {
                NameAndSystem[0] = GenerateName(false, splitArray[0]);
                NameAndSystem[1] = possibleSolarSystems[randomIndex];
            }

            return NameAndSystem;
        }

        private long GenerateRadius()
        {
            return rnd.Next(3000, 99999 + 1);
        }

        public string GenerateType()
        {
            string[] possiblePlanetTypes =
            {
                "Earth-like", "High-Elevation Plateaus", "Mountainous", "Oceanic", "Icy", "Sandy", "Tropical", "Rocky", "Mettalic", "Crystalline",
                "Quicksand", "Subterranean", "Gaseous", "Arid", "Radioactive", "Lava", "Badland", "Bioluminescent", "Time-disorted", "unknown"
            };

            // var lastIndex = possiblePlanetTypes.Length - 1;
            // var randomIndex = rnd.Next(0, lastIndex + 1);
            //
            // return possiblePlanetTypes[randomIndex];
            // possiblePlanetTypes = possiblePlanetTypes.OrderBy(type => rnd.Next()).ToArray();


            var weights = Enumerable.Range(1, possiblePlanetTypes.Length - 1 + 1).Reverse().ToArray();
            var randomWeight = rnd.Next(1, weights.Sum() + 1);

            var selectedWeight = 0;
            var selectedIndex = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                selectedWeight += weights[i];

                if (randomWeight <= selectedWeight)
                {
                    selectedIndex = i;
                    break;
                }
            }

            return possiblePlanetTypes[selectedIndex];
        }

        private long GeneratePopulation(long radius)
        {
            long population = 0;

            if (generatedType == "Radioactive" || generatedType == "Lava" || generatedType == "Arid" || generatedType == "Gaseous")
            {
                population = 0;
            }
            else
            {
                if (radius < 8000)
                {
                    population = rnd.Next(0, 950000000);
                }
                else
                {
                    population = rnd.Next() * rnd.Next(0, 10);
                }
            }

            return Math.Abs(population- (population %10));
        }

        private int GenerateTechnologicalLevel()
        {
            return rnd.Next(0, 5 + 1);
        }

        public int GenerateMilitaryPower()
        {
            return rnd.Next(0, 5 + 1);
        }

        public bool GenerateDemocracy()
        {
            return rnd.Next(2) == 1;
        }

        public float GenerateX()
        {
            int modifier_1 = (rnd.Next(2) * 2) - 1; // Either -1 or 1

            float x_coord;
            do
            {
                x_coord = (float) (rnd.NextDouble() * rnd.Next(1000, 10000) * modifier_1);
            } while (Math.Abs(x_coord) <= 1000);

            return x_coord;
        }

        public float GenerateY()
        {
            int modifier_1 = rnd.Next(0, 2) == 0 ? -1 : 1; // Either -1 or 1

            float y_coord = (float) (rnd.NextDouble() * rnd.Next(1000, 10000) * modifier_1);
            while (Math.Abs(y_coord) <= 1000)
            {
                y_coord = (float) (rnd.NextDouble() * rnd.Next(1000, 10000) * modifier_1);
            }

            return y_coord;
        }

        public float GenerateZ()
        {
            int modifier_1 = rnd.Next(0, 2) == 0 ? -1 : 1; // Either -1 or 1

            float z_coord = (float) (rnd.NextDouble() * rnd.Next(1000, 10000) * modifier_1);
            while (Math.Abs(z_coord) <= 1000)
            {
                z_coord = (float) (rnd.NextDouble() * rnd.Next(1000, 10000) * modifier_1);
            }

            return z_coord;
        }
    }
}