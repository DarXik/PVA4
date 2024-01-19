using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace NebulaNexus
{
    public class Star : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Type;
        public long Radius;
        public double Mass;
        public double Temperature;
        public long Age;
        public BigInteger AvailableEnergy;
        public string SolarSystem;

        private new const float X = 0.0f;
        private new const float Y = 0.0f;
        private new const float Z = 0.0f;

        // public static Star Instance;

        public Star(string name, string type, double mass, long radius, double temperature, long age, BigInteger availableEnergy, string solarSystem,
            int id) : base(X, Y, Z)
        {
            Mass = mass;
            Radius = radius;
            Temperature = temperature - 273.15;
            Age = age;
            AvailableEnergy = availableEnergy;
            Id = id + 1000;
            Name = name;
            Type = type;
            SolarSystem = solarSystem;

            // Instance = this;
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
            var starTemp = GenerateTemperature();
            var starRadius = GenerateRadius();
            var starName = GenerateName();
            var starAge = GenerateAge();
            var starType = GenerateType();
            var starSolarSystem = GenerateSolarSystem();
            var starId = GenerateId();
            var starMass = GenerateMass();
            var starAvailableEnergy = GenerateEnergy(starRadius, starTemp);

            var star1 = new Star(starName, starType, starMass, starRadius, starTemp,
                starAge, starAvailableEnergy, starSolarSystem, starId);

            return star1;
        }

        private List<string> usedSystems = new List<string>();

        public string GenerateSolarSystem()
        {
            var randomIndex = rnd.Next(Program.possibleSolarSystems.Length);
            // var splitArray = Program.possibleSolarSystems[randomIndex].Split(f' ');
            var chosenSystem = Program.possibleSolarSystems[randomIndex];
            return chosenSystem;
        }

        public long GenerateAge()
        {
            return Math.Abs(rnd.Next(1000, 14000) * 1000000);
        }

        public double GenerateMass()
        {
            return rnd.NextDouble() * 2 * Math.Pow(10, 30);
        }

        public long GenerateRadius()
        {
            return rnd.Next(10, 900) * 1000;
        }

        public int GenerateTemperature()
        {
            return rnd.Next(2000, 100000);
        }

        public BigInteger GenerateEnergy(long radius, double temp)
        {
            return (BigInteger) (4 * Math.PI * Math.Pow(radius, 2) * 5.67e-8 * Math.Pow(temp, 4));
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