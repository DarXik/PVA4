using System;
using System.Collections.Generic;
using System.Linq;

namespace NebulaNexus
{
    public class Star : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Type;
        public long Radius;
        public float Temperature;
        public long Age;
        public long AvailableEnergy;
        public string SolarSystem;

        private new const float X = 0.0f;
        private new const float Y = 0.0f;
        private new const float Z = 0.0f;

        public Star(string name, string type, long radius, float temperature, long age, long availableEnergy, string solarSystem,
            int id) : base(X, Y, Z)
        {
            Radius = radius;
            Temperature = temperature;
            Age = age;
            AvailableEnergy = availableEnergy;
            Id = id + 1000;
            Name = name;
            Type = type;
            SolarSystem = solarSystem;
        }
    }

    public class StarGeneratorManager
    {
        Random rnd = new Random();

        private readonly List<string> PossibleStarNames = new List<string>()
        {
            "Cepheda", "Luminar", "Proxima", "Antares", "Celestial Ember", "Xylos",
            "Sylvaar", "Helios Nocturna", "Astralis Althara", "Ignisar", "Vega", "Polaris"
        };

        private HashSet<int> usedIds = new HashSet<int>();
        private int currentId = 1;

        private int GenerateId()
        {
            usedIds.Add(currentId);
            currentId++;
            return usedIds.Last();
        }

        public Star CreateStar()
        {
            // var star1 = new Star()
            return null;
        }

        public string GenerateSolarSystem()
        {
            var randomIndex = rnd.Next(Program.possibleSolarSystems.Length);
            var splitArray = Program.possibleSolarSystems[randomIndex].Split(' ');

            return Program.possibleSolarSystems[randomIndex];
        }

        public long GenerateAge()
        {
            return rnd.Next(100, 25000); // in mills. of years
        }

        public long GenerateRadius()
        {
            return rnd.Next(1, 100);
        }

        public string GenerateType()
        {
            string[] possibleStarTypes =
            {
                "Normal", "Giant", "Subgiant", "White Dwarf", "Black Hole"
            };

            double[] probabilities = {0.6, 0.15, 0.15, 0.05, 0.05}; // šance pro každý typ

            var totalProbability = probabilities.Sum();

            var randomValue = rnd.NextDouble() * totalProbability; // náhodné číslo od 0 incl. do součtů šancí excl.

            for (int i = 0; i < possibleStarTypes.Length; i++)
            {
                // každou iteraci odebírá aktuálnímu typu šanci
                // pořadá probabilities odpovídá typům, takže dřívější typ odebere více od randomValue
                // loop tak má větší šanci zastavit na typu s větší šancí
                randomValue -= probabilities[i];

                if (randomValue <= 0)
                {
                    return possibleStarTypes[i];
                }
            }

            return possibleStarTypes[0];
        }

        private string GenerateName()
        {
            var randomIndex = rnd.Next(PossibleStarNames.Count());
            var chosenItem = PossibleStarNames[randomIndex];
            PossibleStarNames.RemoveAt(randomIndex);
            return chosenItem;
        }
    }
}