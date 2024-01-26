using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace NebulaNexus
{
    public class StarGeneratorManager
    {
        Random rnd = new Random();

        private readonly List<string> PossibleStarNames = new List<string>()
        {
            "Cepheda", "Luminar", "Proxima", "Antares", "Celestial Ember", "Xylos",
            "Sylvaar", "Helios Nocturna", "Althara", "Ignisar", "Vega", "Polaris"
        };

        public Star CreateStar()
        {
            var starName = GenerateName();
            var starType = GenerateType();
            // var starSolarSystem = Program.PossibleSolarSystems[rnd.Next(Program.PossibleSolarSystems.Length)];
            var starTemp = GenerateTemperature(starType.ElementAt(0).Value);
            var starRadius = GenerateRadius(starType.ElementAt(0).Value);
            var starAge = GenerateAge();
            var starMass = GenerateMass(starType.ElementAt(0).Value);
            var starAvailableEnergy = GenerateEnergy(starRadius, starTemp);

            return new Star(starName, starType.ElementAt(0).Key, starMass, starRadius, starTemp,
                starAge, starAvailableEnergy);
        }

        public void RemoveName(string name)
        {
            PossibleStarNames.Remove(name);
        }

        private long GenerateAge()
        {
            // cca 1bil-14bil yrs
            return (long) rnd.Next(1000, 14000) * rnd.Next(999999, 1009999);
        }

        private BigInteger GenerateMass(int value)
        {
            // return rnd.NextDouble() * 2 * Math.Pow(10, 30) > 0 ? rnd.NextDouble() * 2 * Math.Pow(10, 30) : 0.1 * 2 * Math.Pow(10, 30);
            // var sunMass = (BigInteger) (rnd.NextDouble() * (2 * Math.Pow(10, 30) - double.Epsilon) + double.Epsilon);

            var sunMass = (BigInteger) (rnd.NextDouble() * (2 * Math.Pow(10, 30) - 1e-10) + 1e-10);

            if (value == -1)
            {
                // black hole - 3 - 2000000 of Sun
                // (rnd.NextDouble() * (2 * Math.Pow(10, 30) - double.Epsilon) + double.Epsilon) -> 1 Sun, avoids zero
                return sunMass * rnd.Next(3, 20000000);
            }
            else if (value == 1)
            {
                // normal - 0.1 - 10 of Sun
                // return sunMass * (BigInteger) (0.1 + rnd.NextDouble() * 9.9);
                return sunMass * (BigInteger) (1 + rnd.NextDouble() * 99) / 10;
            }
            else if (value == 2)
            {
                // giant - 45 - 100 of Sun
                return sunMass * rnd.Next(45, 100);
            }
            else if (value == 3)
            {
                // subgiant - 15 - 30 of Sun
                return sunMass * rnd.Next(1, 30);
            }
            else if (value == 4)
            {
                // white dwarfs - 0.6 - 1.4 of Sun
                return sunMass * (BigInteger) (6 + rnd.NextDouble() * 8) / 10;
            }
            else
            {
                return 0;
            }
        }

        private double GenerateTemperature(int value)
        {
            if (value == -1)
            {
                // black hole - 0
                return -1;
            }
            else if (value == 1)
            {
                // normal - 2500 - 30000
                return rnd.Next(2500, 30000);
            }
            else if (value == 2)
            {
                // giant - 3000 - 5000
                return rnd.Next(3000, 5000);
            }
            else if (value == 3)
            {
                // subgiant - 3000 - 6000
                return rnd.Next(3000, 6000);
            }
            else if (value == 4)
            {
                // white dwarfs - 10k+ - 300k
                return rnd.Next(10, 300) * 1000;
            }
            else
            {
                return 0;
            }
        }

        private long GenerateRadius(int value)
        {
            if (value == -1)
            {
                // black hole - undefined
                return -1;
            }
            else if (value == 1)
            {
                // normal - 400k - 5mil
                return rnd.Next(400, 5000) * 999;
            }
            else if (value == 2)
            {
                // giant - 45mil - 450mil
                return rnd.Next(45, 450) * 999999;
            }
            else if (value == 3)
            {
                // subgiant - 5mil - 45mil
                return rnd.Next(5, 45) * 999999;
            }
            else if (value == 4)
            {
                // white dwarfs - 100k - 600k
                return rnd.Next(100, 600) * 999;
            }
            else
            {
                return 0;
            }
        }

        private object GenerateEnergy(long radius, double temp)
        {
            if (radius < 0)
            {
                return double.PositiveInfinity;
            }
            else
            {
                return (BigInteger) (4 * Math.PI * Math.Pow(radius, 2) * 5.67e-8 * Math.Pow(temp, 4));
            }
        }

        private Dictionary<string, int> GenerateType()
        {
            var possibleStarTypes = new Dictionary<string, int>
            {
                {"Normal", 1},
                {"Giant", 2},
                {"Subgiant", 3},
                {"White Dwarf", 4},
                {"Black Hole", -1}
            };
            // víc to pohrotit zhlediska planet type a population
            double[] probabilities = {0.8, 0.115, 0.065, 0.05, 0.025}; // šance pro každý typ

            var totalProbability = probabilities.Sum();

            var randomValue = rnd.NextDouble() * totalProbability; // náhodné číslo od 0 incl. do součtů šancí excl.

            for (int i = 0; i < possibleStarTypes.Count; i++)
            {
                // každou iteraci odebírá aktuálnímu typu šanci
                // pořadá probabilities odpovídá typům, takže dřívější typ odebere více od randomValue
                // loop tak má větší šanci zastavit na typu s větší šancí
                randomValue -= probabilities[i];

                if (randomValue <= 0)
                {
                    return new Dictionary<string, int>() {{possibleStarTypes.ElementAt(i).Key, possibleStarTypes.ElementAt(i).Value}};
                }
            }

            return new Dictionary<string, int>() {{possibleStarTypes.ElementAt(0).Key, possibleStarTypes.ElementAt(0).Value}};
        }

        private string GenerateName()
        {
            var randomIndex = rnd.Next(PossibleStarNames.Count());
            var chosenItem = PossibleStarNames[randomIndex];
            return chosenItem;
        }
    }
}