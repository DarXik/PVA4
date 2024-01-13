using System;
using System.Collections.Generic;
using Spectre.Console;

namespace NebulaNexus
{
    internal class Program
    {
        static Planet Planet1 = new Planet("Nexus", 9748, "Icy", 1000, 3, 0, true, "Andromeda", 1000, 1000, 1000, 1);
        static Planet Planet2 = new Planet("Aldoria", 5001, "EarthLike", 10004561, 3, 2, true, "Alpha Centauri", 900, 9000, 100, 2);
        static Star Star1 = new Star("Cepheda", 88575875f, 5421.3f, 14000800090, 99999999999999, "Andromeda", 1);
        static Ship Ship1 = new Ship("Galactic Cruiser", "Exploration Vessel", 900, 0.72, true, false, 0, 5, null, Planet1, 1);

        static List<Planet> planetsList = new List<Planet>() {Planet1, Planet2};
        static List<Star> starsList = new List<Star>() {Star1};
        static List<Planet> knownPlanets = new List<Planet>() {Planet2};
        private static List<Ship> AvailableShips = new List<Ship>() {Ship1};

        static Player Player1 = new Player("David", Planet2, Planet2, Ship1, knownPlanets, Planet1.X, Planet1.Y, Planet1.Z, 1); // proč musí být po listech?


        public static void Main(string[] args)
        {
            Introduction();
        }

        public static void Introduction()
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

        public static void PlayerOptions()
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

        public static void ShowPlayerStats(Player player, List<Ship> availableShips)
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

        public static void ShowAllPlanets(List<Planet> planetsList)
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

        public static void ShowAllStars(List<Star> starsList)
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


    public interface IGameObject
    {
        string Name { get; set; }
        int Id { get; set; }
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

        public Player(string name, Planet homePlanet, Planet currentPlanet, Ship currentShip, List<Planet> knownPlanets, double x, double y, double z, int id) : base(x, y, z)
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

        private const double X = 0.0;
        private const double Y = 0.0;
        private const double Z = 0.0;

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

    public abstract class Coordinates
    {
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Coordinates(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Tuple<double, double, double> GetCoordinates()
        {
            return new Tuple<double, double, double>(X, Y, Z);
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
            double x,
            double y,
            double z,
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