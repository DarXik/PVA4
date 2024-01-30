using System;
using System.Collections.Generic;

namespace NebulaNexus
{
    public struct Coordinate
    {
        public long X { get; }
        public long Y { get; }
        public long Z { get; }

        public Coordinate(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public static class CoordinateGenerator
    {
        private static readonly Dictionary<SolarSystem, long[,]> UsedSystemCoordinates = new Dictionary<SolarSystem, long[,]>();


        private static long GenerateCoordinate(int counter)
        {
            int seed = (int) DateTime.Now.Ticks;
            Random rnd = new Random(seed);
            return ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, counter) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);
        }

        public static Coordinate PossibleCoordsSystem(long radius, SolarSystem solarSystem)
        {
            Coordinate createdCoordinates = new Coordinate(0, 0, 0);

            if (UsedSystemCoordinates.Count > 0)
            {
                long x = 0;
                long y = 0;
                long z = 0;

                foreach (var value in UsedSystemCoordinates.Values)
                {
                    int counter = 10;
                    do
                    {
                        x = GenerateCoordinate(counter);
                        counter++;
                    } while (x >= value[0, 0] + (value[0, 0] < 0 ? -radius : radius) && x <= value[1, 0] + (value[1, 0] > 0 ? radius : -radius));

                }

                foreach (var value in UsedSystemCoordinates.Values)
                {
                    int counter = 10;
                    do
                    {
                        y = GenerateCoordinate(counter);
                        counter++;
                    } while (y >= value[1, 0] + (value[0, 0] < 0 ? -radius : radius) && y <= value[1, 0] + (value[1, 1] < 0 ? -radius : radius));
                }

                foreach (var value in UsedSystemCoordinates.Values)
                {
                    int counter = 10;
                    do
                    {
                        z = GenerateCoordinate(counter);
                        counter++;
                    } while (z >= value[2, 0] + (value[0, 0] < 0 ? -radius : radius) && z <= value[1, 0] + (value[2, 1] < 0 ? -radius : radius));
                }


                createdCoordinates = new Coordinate(x, y, z);
            }
            else
            {
                createdCoordinates = new Coordinate(GenerateCoordinate(30), GenerateCoordinate(30), GenerateCoordinate(30));
            }

            var coordinatesArray = new[,]
            {
                {
                    (long) (solarSystem.Coordinates.X + solarSystem.MainStar.Radius + 5 * Math.Pow(10, 6)),
                    (long) (solarSystem.Coordinates.X + solarSystem.Radius - 4 * Math.Pow(10, 6))
                },
                {
                    (long) (solarSystem.Coordinates.Y + solarSystem.MainStar.Radius + 5 * Math.Pow(10, 6)),
                    (long) (solarSystem.Coordinates.Y + solarSystem.Radius - 4 * Math.Pow(10, 6))
                },
                {
                    (long) (solarSystem.Coordinates.Z + solarSystem.MainStar.Radius + 5 * Math.Pow(10, 6)),
                    (long) (solarSystem.Coordinates.Z + solarSystem.Radius - 4 * Math.Pow(10, 6))
                }
            };

            UsedSystemCoordinates.Add(solarSystem, coordinatesArray);

            return createdCoordinates;
        }

        public static long[] PossibleCoordsPlanet(SolarSystem solarSystem, string identifier)
        {
            if (UsedSystemCoordinates.TryGetValue(solarSystem, out var coordinates))
            {
                Console.WriteLine($"Solar: {solarSystem.Name}: ");
                Console.WriteLine($"X Range: {coordinates[0, 0]:N0} to {coordinates[0, 1]:N0}");
                Console.WriteLine($"Y Range: {coordinates[1, 0]:N0} to {coordinates[1, 1]:N0}");
                Console.WriteLine($"Z Range: {coordinates[2, 0]:N0} to {coordinates[2, 1]:N0}");

                if (identifier.ToLower() == "x")
                {
                    var returnArray = new long[2];
                    returnArray[0] = coordinates[0, 0];
                    returnArray[1] = coordinates[0, 1];

                    return returnArray;
                }
                else if (identifier.ToLower() == "y")
                {
                    var returnArray = new long[2];
                    returnArray[0] = coordinates[1, 0];
                    returnArray[1] = coordinates[1, 1];

                    return returnArray;
                }
                else if (identifier.ToLower() == "z")
                {
                    var returnArray = new long[2];
                    returnArray[0] = coordinates[2, 0];
                    returnArray[1] = coordinates[2, 1];

                    return returnArray;
                }
            }

            return null;
        }
    }
}