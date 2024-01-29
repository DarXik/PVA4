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

        // minValue = long[0]; maxValue = long[1]
        public static void AssignCoordianates(SolarSystem solarSystem)
        {
            var coordinatesArray = new long[,]
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
        }
        private static int seed = (int) DateTime.Now.Ticks;
        private static Random rnd = new Random(seed);
        public static long CheckSystemCoordinates(long radius, string identifier)
        {
            if (UsedSystemCoordinates.Count > 0)
            {
                long result = 0;
                if (identifier.ToLower() == "x")
                {
                    foreach (var value in UsedSystemCoordinates.Values)
                    {
                        do
                        {
                            result = ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, 30) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);
                        } while (result >= value[0, 0] + (value[0, 0] < 0 ? -radius : radius) && result <= value[1, 0] + (value[1, 0] < 0 ? -radius : radius));
                    }

                    return result;
                }
                else if (identifier.ToLower() == "y")
                {
                    foreach (var value in UsedSystemCoordinates.Values)
                    {
                        do
                        {
                            result = ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, 30) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);
                        } while (result >= value[1, 0] + (value[0, 0] < 0 ? -radius : radius) && result <= value[1, 0] + (value[1, 1] < 0 ? -radius : radius));
                    }

                    return result;
                }
                else if (identifier.ToLower() == "z")
                {
                    foreach (var value in UsedSystemCoordinates.Values)
                    {
                        do
                        {
                            result = ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, 30) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);
                        } while (result >= value[2, 0] + (value[0, 0] < 0 ? -radius : radius) && result <= value[1, 0] + (value[2, 1] < 0 ? -radius : radius));
                    }

                    return result;
                }

                return 0;
            }

            return ((long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, 30) | (uint) rnd.Next(10000000)) * (rnd.Next(0, 2) == 0 ? -1 : 1);
        }

        public static long[] CheckCoordinates(SolarSystem solarSystem, string identifier)
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