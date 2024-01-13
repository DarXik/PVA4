using System;
using System.Collections.Generic;
using System.Numerics;
using Spectre.Console;

// ReSharper disable All

namespace NebulaNexus
{
    internal class Program
    {
        static Planet Planet1 = new Planet("Nexus", 9748, "Icy", 1000, 3, 0, true, "Andromeda", 1000, 1000, 1000, 1);
        static Planet Planet2 = new Planet("Aldoria", 5001, "EarthLike", 10004561, 3, 2, true, "Alpha Centauri", 900, 9000, 100, 2);
        static Star Star1 = new Star("Cepheda", 88575875f, 5421.3f, 14000800090, 99999999999999, "Andromeda", 1);
        static Ship Ship1 = new Ship("Galactic Cruiser", "Exploration Vessel", 900, 0.72, true, false, 0, 5, null, Planet1, 1);


        public static List<Planet> planetsList = new List<Planet>() { };
        static List<Star> starsList = new List<Star>() {Star1};
        static List<Planet> knownPlanets = new List<Planet>() {Planet2};
        private static List<Ship> AvailableShips = new List<Ship>() {Ship1};

        static Player Player1 = new Player("David", Planet2, Planet2, Ship1, knownPlanets, Planet1.X, Planet1.Y, Planet1.Z, 1); // proč musí být po listech?

        static string[] possibleNamesPlanet = {"Nexus", "Aldoria", "Celestaria", "Orionis", "Lunaris Prime", "Lunaris Nova", "Astrionex"};

        public static void Main(string[] args)
        {
            PlanetGeneratorManager pgManager = new PlanetGeneratorManager();
            for (int i = 0; i < possibleNamesPlanet.Length; i++)
            {
                Planet planet1 = new Planet(possibleNamesPlanet[i], pgManager.GenerateRadius(), pgManager.GenerateType(), pgManager.GeneratePopulation(),
                    pgManager.GenerateTechnologicalLevel(), pgManager.GenerateMilitaryPower(), pgManager.GenerateDemocracy(), pgManager.GenerateSolarSystem(),
                    pgManager.GenerateX(), pgManager.GenerateY(), pgManager.GenerateZ(), i + 1);

                planetsList.Add(planet1);
            }


            foreach (var planet in planetsList)
            {
                Console.WriteLine($"Planet Name: {planet.Name}");
                Console.WriteLine($"Radius: {planet.Radius}");
                Console.WriteLine($"Type: {planet.PlanetType}");
                Console.WriteLine($"Population: {planet.Population}");
                Console.WriteLine($"Technological Level: {planet.TechnologicalLevel}");
                Console.WriteLine($"Military Power: {planet.MilitaryPower}");
                Console.WriteLine($"Democracy: {planet.IsDemocratic}");
                Console.WriteLine($"Solar System: {planet.SolarSystem}");
                Console.WriteLine($"X Coordinate: {planet.X}");
                Console.WriteLine($"Y Coordinate: {planet.Y}");
                Console.WriteLine($"Z Coordinate: {planet.Z}");
                Console.WriteLine($"Index: {planet.Id}");
                Console.WriteLine();
            }

            // Introduction();
        }

        private static void Introduction()
        {
            AnsiConsole.Markup("Welcome to [green]Nebula Nexus[/] - space exploration and conquest");

            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("These are you options to continue: ");
                Console.WriteLine("Press [p] to show all planets");
                Console.WriteLine("Press [s] to show all stars");
                Console.WriteLine("Press [x] for exit and save");
                Console.WriteLine("Press [o] for player options");


                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.P:
                        Console.WriteLine("\n");
                        ShowAllPlanets(planetsList);
                        break;
                    case ConsoleKey.S:
                        Console.WriteLine("\n");
                        ShowAllStars(starsList);
                        break;
                    case ConsoleKey.X:
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.O:
                        Console.WriteLine("\n");
                        PlayerOptions();
                        break;
                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("This key does nothing.. try again");
                        break;
                }
            }
        }

        private static void PlayerOptions()
        {
            Console.WriteLine("Your player options are: ");

            while (true)
            {
                Console.WriteLine("[s] to show stats and ships");
                Console.WriteLine("[p] to show known planets");
                Console.WriteLine("[t] for travel to known Solar Systems");
                Console.WriteLine("[e] for exploration");
                Console.WriteLine("[x] to go back to menu");


                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.S:
                        Console.WriteLine("\n");
                        ShowPlayerStats(Player1, AvailableShips);
                        break;
                    case ConsoleKey.P:
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.T:
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.E:
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.X:
                        Console.WriteLine("\n");
                        Introduction();
                        return;
                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("This key does nothing.. try again");
                        break;
                }
            }
        }

        private static void ShowPlayerStats(Player player, List<Ship> availableShips)
        {
            var table = new Table()
                .Border(TableBorder.Ascii2)
                // .BorderColor(Color.Silver)
                .AddColumn(new TableColumn("[olive]Name[/]"))
                .AddColumn(new TableColumn("[olive]Technological Level[/]"))
                .AddColumn(new TableColumn("[olive]Diplomacy Level[/]"))
                .AddColumn(new TableColumn("Trustworthiness"))
                .AddColumn(new TableColumn("Home Planet"))
                .AddColumn(new TableColumn("Home Solar System"));


            table.AddRow(
                $"{player.Name}",
                $"{player.TechnologicalLevel}",
                $"{player.DiplomacyLevel}",
                $"{player.Trustworthiness}",
                $"{player.HomePlanet.Name}",
                $"{player.HomePlanet.SolarSystem}"
            );


            table.Columns[4].Width(10);
            // table.Collapse();
            AnsiConsole.Write(table);
            Console.WriteLine("\n");

            var tableShips = new Table()
                .Border(TableBorder.Ascii2)
                .AddColumn(new TableColumn("Name"))
                .AddColumn(new TableColumn("Type"))
                .AddColumn(new TableColumn("Max Speed"))
                .AddColumn(new TableColumn("Fuel"))
                .AddColumn(new TableColumn("Military Power"))
                .AddColumn(new TableColumn("Technological Level"))
                // .AddColumn(new TableColumn("Weaponry"))
                .AddColumn(new TableColumn("Location"));

            foreach (var ship in availableShips)
            {
                tableShips.AddRow(
                    $"{ship.Name}",
                    $"{ship.ShipType}",
                    $"{ship.Speed} km/h",
                    $"{ship.Fuel * 100} %",
                    $"{ship.MilitaryPower}",
                    $"{ship.TechnologicalLevel}",
                    $"Planet: {ship.CurrentPlanet.Name}"
                );
            }

            table.Columns[4].Width(10);
            // table.Collapse();
            AnsiConsole.Write(tableShips);
            Console.WriteLine("\n");
        }

        private static void ShowAllPlanets(List<Planet> planetsList)
        {
            var table = new Table()
                .Border(TableBorder.Ascii2)
                // .BorderColor(Color.Silver)
                .AddColumn(new TableColumn("[olive]Name[/]"))
                .AddColumn(new TableColumn("[olive]Radius[/]"))
                .AddColumn(new TableColumn("[olive]Planet Type[/]"))
                .AddColumn(new TableColumn("Population"))
                .AddColumn(new TableColumn("Technological Level"))
                .AddColumn(new TableColumn("Democratic"))
                .AddColumn(new TableColumn("Solar System"));

            foreach (var planet in planetsList)
            {
                table.AddRow(
                    $"{planet.Name}",
                    $"{planet.Radius} km",
                    $"{planet.PlanetType}",
                    $"{planet.Population}",
                    $"{planet.TechnologicalLevel}",
                    $"{planet.IsDemocratic}",
                    $"{planet.SolarSystem}"
                );
            }

            table.Columns[4].Width(10);
            // table.Collapse();
            AnsiConsole.Write(table);
        }

        private static void ShowAllStars(List<Star> starsList)
        {
            var table = new Table()
                .Border(TableBorder.Ascii2)
                // .BorderColor(Color.Silver)
                .AddColumn(new TableColumn("[maroon]Name[/]"))
                .AddColumn(new TableColumn("[maroon]Mass[/]"))
                .AddColumn(new TableColumn("[maroon]Temperature[/]"))
                .AddColumn(new TableColumn("Age"))
                .AddColumn(new TableColumn("Available Energy"))
                .AddColumn(new TableColumn("Solar System"));

            foreach (var star in starsList)
            {
                table.AddRow(
                    $"{star.Name}",
                    $"{star.Mass.ToString()} kg",
                    $"{star.Temperature.ToString()} °C",
                    $"{star.Age.ToString()} yrs",
                    $"{star.AvailableEnergy.ToString()} W",
                    $"{star.SolarSystem}"
                );
            }

            // table.Collapse();
            AnsiConsole.Write(table);
        }
    }

    public class PlanetGeneratorManager
    {
        Random rnd = new Random();

        public long GenerateRadius()
        {
            return rnd.Next(4000, 99999 + 1);
        }

        public string GenerateType()
        {
            string[] possibleTypesPlanet =
            {
                "Icy", "Earth-like", "Gaseous", "Rocky", "Sandy", "Oceanic", "Mettalic", "Time-disorted", "Lava", "Badland", "Crystalline", "Arid", "Subterranean",
                "Bioluminescent", "Tropical", "Radioactive", "High-Elevation Plateaus", "Mountainous", "Quicksand", "unknown"
            };
            return possibleTypesPlanet[rnd.Next(possibleTypesPlanet.Length)];
        }

        public long GeneratePopulation()
        {
            // byte[] data = new byte[64];
            // rnd.NextBytes(data);
            // rnd..NextInt64(); -> .NET 8
            // return (long) new BigInteger(data);
            long population = rnd.Next() * rnd.Next(0, 15);
            return Math.Abs(population);
        }

        public int GenerateTechnologicalLevel()
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

        public string GenerateSolarSystem()
        {
            string[] possibleSolarSystems =
            {
                "Andromeda", "Nova Ecliptic Realm", "Hyperion Star Cluster", "Astralis", "Shili", "unknown"
            };

            return possibleSolarSystems[rnd.Next(possibleSolarSystems.Length)];
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

    public interface IGameObject
    {
        string Name { get; set; }
        int Id { get; set; }
    }

    public abstract class Coordinates
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Coordinates(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Tuple<float, float, float> GetCoordinates()
        {
            return new Tuple<float, float, float>(X, Y, Z);
        }
    }

    public class Player : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int TechnologicalLevel;
        public int DiplomacyLevel;
        public int Trustworthiness;
        public bool IsAlive { get; set; }

        public List<Planet> KnownPlanets { get; set; }
        public List<Ship> Fleet { get; set; }

        public Planet HomePlanet;
        public Planet CurrentPlanet;
        public Ship CurrentShip;

        public Player(string name, Planet homePlanet, Planet currentPlanet, Ship currentShip, List<Planet> knownPlanets, float x, float y, float z, int id) : base(x, y, z)
        {
            Name = name;
            HomePlanet = homePlanet;
            CurrentShip = currentShip;
            CurrentPlanet = currentPlanet;
            Id = id + 2000;
            TechnologicalLevel = 3;
            DiplomacyLevel = 0;
            Trustworthiness = 10;
            IsAlive = true;
            KnownPlanets = knownPlanets;
            Fleet = new List<Ship>();
        }
    }

    public class Ship : IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string ShipType { get; set; }
        public int Speed { get; set; }
        public double Fuel { get; set; }
        public bool HasHyperDriver { get; }
        public bool IsEmpty { get; set; }
        public int MilitaryPower { get; set; }
        public int TechnologicalLevel { get; set; }
        public List<string> Weaponry { get; set; }
        public Planet CurrentPlanet { get; set; }

        public Ship(string name, string shipType, int speed, double fuel, bool hasHyperDriver, bool isEmpty, int militaryPower, int technologicalLevel, List<string> weaponry,
            Planet currentPlanet, int id)
        {
            Name = name;
            ShipType = shipType;
            Id = id;
            Speed = speed;
            Fuel = fuel;
            HasHyperDriver = hasHyperDriver;
            IsEmpty = isEmpty;
            MilitaryPower = militaryPower;
            TechnologicalLevel = technologicalLevel;
            Weaponry = weaponry;
            CurrentPlanet = currentPlanet;
        }
    }

    public class Star : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public float Mass;
        public float Temperature;
        public long Age;
        public float AvailableEnergy;
        public string SolarSystem;

        private const float X = 0.0f;
        private const float Y = 0.0f;
        private const float Z = 0.0f;

        public Star(string name, float mass, float temperature, long age, float availableEnergy, string solarSystem,
            int id) : base(X, Y, Z)
        {
            Mass = mass;
            Temperature = temperature;
            Age = age;
            AvailableEnergy = availableEnergy;
            Id = id + 1000;
            Name = name;
            SolarSystem = solarSystem;
        }
    }

    public class Planet : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long Radius;
        public string PlanetType;
        public long Population;
        public int TechnologicalLevel;
        public int MilitaryPower;
        public bool IsDemocratic;
        public string SolarSystem;

        public Planet(string name, long radius, string planetType, long population, int technologicalLevel, int militaryPower, bool isDemocratic, string solarSystem,
            float x,
            float y,
            float z,
            int id) : base(x, y, z)
        {
            PlanetType = planetType;
            Population = population;
            TechnologicalLevel = technologicalLevel;
            IsDemocratic = isDemocratic;
            Id = id;
            MilitaryPower = militaryPower;
            Name = name;
            Radius = radius;
            SolarSystem = solarSystem;
        }
    }
}