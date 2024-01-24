﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NebulaNexus
{
    public class PlanetGeneratorManager
    {
        Random rnd = new Random();

        public readonly List<string> PossiblePlanetNames = new List<string>()
        {
            "Aetheria", "Aldoria", "Arden", "Astoria", "Astrionex",
            "Bespin", "Celestaria", "Crest", "Dagobah", "Elysium",
            "Eos", "Emberlyn", "Endor", "Epsilon", "Gala",
            "HB", "Haven", "Iris", "K-10", "Kashyyk",
            "Lagoon", "Larkspire", "Lunarastra", "Lunaris Prime", "Maris",
            "Mars", "Nova", "Nexus", "Nyx", "Orion",
            "Orionis", "Phantasmagora", "Pluto", "Rigel", "Solarnyx",
            "Spectra", "Sylvaris", "Umbraflux", "Voltria", "Xenepha",
            "Zephyron", "Zyra"
        };

        public Planet CreatePlanet()
        {
            var generatedType = GenerateType();
            var generatedRadius = GenerateRadius();
            var generatedPopulation = GeneratePopulation(generatedRadius, generatedType);
            var generatedTechLevel = GenerateTechnologicalLevel(generatedPopulation);
            var generatedMiliLevel = GenerateMilitaryPower(generatedTechLevel);
            var generatedDemocracy = GenerateDemocracy(generatedTechLevel, generatedMiliLevel);


            var planet1 = new Planet(
                GenerateName(), generatedRadius, generatedType,
                generatedPopulation, generatedTechLevel, generatedMiliLevel, generatedDemocracy,
                GenerateCoord(generatedType), GenerateCoord(generatedType), GenerateCoord(generatedType));

            return planet1;
        }

        // bool doubleWordSystem, string systemPrefix
        private string GenerateName()
        {
            var chosenName = PossiblePlanetNames[rnd.Next(PossiblePlanetNames.Count)];
            //
            PossiblePlanetNames.Remove(chosenName);
            // if (doubleWordSystem && systemPrefix.Length > 0) // dvojitý název
            // {
            //     return systemPrefix + " " + chosenName;
            // }
            // else
            // {
            return chosenName;
            // }
        }

        // DOŘEŠIT
        // private string[] GenerateSolarSystem()
        // {
        //     var nameAndSystem = new string[2];
        //
        //     var randomIndex = rnd.Next(Program.PossibleSolarSystems.Length);
        //     var splitArray = Program.PossibleSolarSystems[randomIndex].Split(' ');
        //
        //     if (splitArray.Length > 1) // dvojitý název
        //     {
        //         nameAndSystem[0] = GenerateName(true, splitArray[0]);
        //         nameAndSystem[1] = Program.PossibleSolarSystems[randomIndex];
        //     }
        //     else
        //     {
        //         nameAndSystem[0] = GenerateName(false, splitArray[0]);
        //         nameAndSystem[1] = Program.PossibleSolarSystems[randomIndex];
        //     }
        //
        //     return nameAndSystem;
        // }

        private long GenerateRadius()
        {
            return rnd.Next(3000, 70000 + 1);
        }

        private string GenerateType()
        {
            string[] possiblePlanetTypes =
            {
                "Earth-like", "High-Elevation Plateaus", "Mountainous", "Oceanic", "Icy", "Sandy", "Tropical", "Rocky", "Mettalic", "Crystalline",
                "Quicksand", "Subterranean", "Gaseous", "Arid", "Radioactive", "Lava", "Badland", "Bioluminescent", "Time-disorted", "unknown"
            };

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

        private long GeneratePopulation(long radius, string planetType)
        {
            long population = 0;

            if (planetType == "Radioactive" || planetType == "Lava" || planetType == "Arid" || planetType == "Gaseous")
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

            return Math.Abs(population - (population % 10));
        }

        private int GenerateTechnologicalLevel(long population)
        {
            if (population > 90000000)
            {
                return rnd.Next(1, 150 + 1) <= 25 ? 5 : rnd.Next(1, 5);
            }
            else if (population > 0)
            {
                return rnd.Next(0, 5 + 1);
            }
            else
            {
                return 0;
            }
        }

        private int GenerateMilitaryPower(int techLevel)
        {
            if (techLevel > 2)
            {
                return rnd.Next(2, 5 + 1);
            }
            else
            {
                if (techLevel != 0)
                {
                    return rnd.Next(0, 2 + 1);
                }
                else
                {
                    return 0;
                }
            }
        }

        private bool GenerateDemocracy(int techLevel, int miliLevel)
        {
            int democracyChance;
            if (techLevel > 0 && miliLevel > 0)
            {
                if (techLevel > miliLevel)
                {
                    democracyChance = 80;
                }
                else if (miliLevel == 5 && techLevel < 4)
                {
                    democracyChance = 30;
                }
                // else if (Math.Abs(techLevel - miliLevel) > Math.Min(techLevel, miliLevel) * 40 / 100)
                else if (miliLevel - techLevel > 2)
                {
                    democracyChance = 49;
                }
                else
                {
                    democracyChance = 65;
                }
            }
            else if (techLevel == 0 && miliLevel == 0)
            {
                return false;
            }
            else if (techLevel == miliLevel)
            {
                democracyChance = 52;
            }
            else
            {
                democracyChance = 65;
            }

            var trueCount = 0;
            var falseCount = 0;
            for (int i = 0; i < 1000; i++)
            {
                int x = rnd.Next(0, 100) < democracyChance ? 1 : 0;
                if (x == 1)
                {
                    trueCount++;
                }
                else
                {
                    falseCount++;
                }
            }

            if (trueCount > falseCount)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private float GenerateCoord(string planetType)
        {
            // if (planetType.ToLower() != "unknown")
            // {
            int modifier_1 = rnd.Next(0, 2) == 0 ? -1 : 1; // Either -1 or 1

            float coord = (float) (rnd.NextDouble() * rnd.Next(1000, 10000) * modifier_1);
            while (Math.Abs(coord) <= 1000)
            {
                coord = (float) (rnd.NextDouble() * rnd.Next(1000, 10000) * modifier_1);
            }

            return coord;
            //     }
            //     else
            //     {
            //         return -1;
            //     }
            // }
        }
    }
}