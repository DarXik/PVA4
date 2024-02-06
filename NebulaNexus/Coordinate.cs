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
        private static Random rnd = new Random();

        private static long GenerateCoordinate(int counter)
        {
            // int seed = (int) DateTime.Now.Ticks;
            // Random rnd = new Random(seed);
            return (long) rnd.Next(int.MinValue, 10000000) << rnd.Next(0, counter) | (uint) rnd.Next(10000000);
        }

        public static Coordinate PossibleCoordsSystem(long radius, SolarSystem solarSystem)
        {
            var createdCoordinates = new Coordinate(0, 0, 0);

            if (UsedSystemCoordinates.Count > 0)
            {
                long x = 0;
                long y = 0;
                long z = 0;

                foreach (var value in UsedSystemCoordinates.Values)
                {
                    var counter = 10;
                    do
                    {
                        x = GenerateCoordinate(counter);
                        counter++;
                    } while (x >= value[0, 0] + (value[0, 0] < 0 ? -radius : radius) &&
                             x <= value[1, 0] + (value[1, 0] > 0 ? radius : -radius));
                }

                foreach (var value in UsedSystemCoordinates.Values)
                {
                    var counter = 10;
                    do
                    {
                        y = GenerateCoordinate(counter);
                        counter++;
                    } while (y >= value[1, 0] + (value[0, 0] < 0 ? -radius : radius) &&
                             y <= value[1, 0] + (value[1, 1] < 0 ? -radius : radius));
                }

                foreach (var value in UsedSystemCoordinates.Values)
                {
                    var counter = 10;
                    do
                    {
                        z = GenerateCoordinate(counter);
                        counter++;
                    } while (z >= value[2, 0] + (value[0, 0] < 0 ? -radius : radius) &&
                             z <= value[1, 0] + (value[2, 1] < 0 ? -radius : radius));
                }


                createdCoordinates = new Coordinate(x, y, z);
            }
            else
            {
                createdCoordinates = new Coordinate(GenerateCoordinate(rnd.Next(30)), GenerateCoordinate(rnd.Next(30)), GenerateCoordinate(rnd.Next(30)));
            }

            var coordinatesArray = new[,]
            {
                {
                    (long) (createdCoordinates.X + solarSystem.MainStar.Radius + 5 * Math.Pow(10, 6)),
                    (long) (createdCoordinates.X + solarSystem.Radius - 4 * Math.Pow(10, 6))
                },
                {
                    (long) (createdCoordinates.Y + solarSystem.MainStar.Radius + 5 * Math.Pow(10, 6)),
                    (long) (createdCoordinates.Y + solarSystem.Radius - 4 * Math.Pow(10, 6))
                },
                {
                    (long) (createdCoordinates.Z + solarSystem.MainStar.Radius + 5 * Math.Pow(10, 6)),
                    (long) (createdCoordinates.Z + solarSystem.Radius - 4 * Math.Pow(10, 6))
                }
            };

            UsedSystemCoordinates.Add(solarSystem, coordinatesArray);

            return createdCoordinates;
        }

        public static Coordinate PossibleCoordsPlanet(SolarSystem solarSystem, int radius)
        {
            var createdCoordinates = new Coordinate(0, 0, 0);

            if (UsedSystemCoordinates.TryGetValue(solarSystem, out var coordinates))
            {
                long x;
                long y;
                long z;

                do
                {
                    // max:(x + radius + 60m) - (plRadius) - min:(x) + (x)
                    x = (long) (rnd.NextDouble() * rnd.Next(99) * (coordinates[0, 1] >= 0 ? -radius : radius - coordinates[0, 0]) + coordinates[0, 0]);
                } while (x >= (coordinates[0, 0] < 0 ? -radius : radius) &&
                         x <= (coordinates[0, 1] < 0 ? -radius : radius));

                do
                {
                    y = (long) (rnd.NextDouble() * rnd.Next(99) * (coordinates[1, 1] >= 0 ? -radius : radius - coordinates[1, 0]) + coordinates[1, 0]);
                } while (y >= (coordinates[1, 0] < 0 ? -radius : radius) && y <= (coordinates[1, 1] < 0 ? -radius : radius));

                do
                {
                    z = (long) (rnd.NextDouble() * rnd.Next(99) * (coordinates[2, 1] >= 0 ? -radius : radius - coordinates[2, 0]) + coordinates[2, 0]);
                } while (z >= (coordinates[2, 0] < 0 ? -radius : radius) && z <= (coordinates[2, 1] < 0 ? -radius : radius));


                createdCoordinates = new Coordinate(x, y, z);
            }
            else
            {
                createdCoordinates = new Coordinate(0, 0, 0);
            }

            return createdCoordinates;
        }
    }
}