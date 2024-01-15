using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace NebulaNexus
{
    internal class Program
    {

        public static List<Planet> PlanetsList = new List<Planet>();
        private static Planet Planet1;

        // static Planet Planet1 = new Planet("Nexus", 9748, "Icy", 1000, 3, 0, true, "Andromeda", 1000, 1000, 1000, 1);
        static Star Star1 = new Star("Cepheda", 88575875f, 5421.3f, 14000800090, 99999999999999, "Andromeda", 1);
        private static Ship Ship1;

        static List<Star> StarsList = new List<Star>() {Star1};
        static List<Planet> KnownPlanets = new List<Planet>();
        private static List<Ship> AvailableShips = new List<Ship>();

        public static Player Player1; // proč musí být po listech?

        public static void Main(string[] args)
        {
            PlanetGeneratorManager pgManager = new PlanetGeneratorManager();



            for (int i = 0; i < 10; i++)
            {
                PlanetsList.Add(pgManager.CreatePlanet(i));
            }
            foreach (var planet in PlanetsList)
            {
                Console.WriteLine($"Planet Name: {planet.Name}");
                // Console.WriteLine($"Radius: {planet.Radius}");
                Console.WriteLine($"Type: {planet.PlanetType}");
                Console.WriteLine($"Population: {planet.Population}");
                // Console.WriteLine($"Technological Level: {planet.TechnologicalLevel}");
                // Console.WriteLine($"Military Power: {planet.MilitaryPower}");
                // Console.WriteLine($"Democracy: {planet.IsDemocratic}");
                Console.WriteLine($"Solar System: {planet.SolarSystem}");
                // Console.WriteLine($"X Coordinate: {planet.X}");
                // Console.WriteLine($"Y Coordinate: {planet.Y}");
                // Console.WriteLine($"Z Coordinate: {planet.Z}");
                // Console.WriteLine($"Index: {planet.Id}");
                Console.WriteLine();
            }
            // Random randGen = new Random();
            // var trueChance = 10;
            // var totalCount = 9999999;
            // var trueCount = 0;
            // var falseCount = 0;
            // for (int i = 0; i < totalCount; i++)
            // {
            //     int x = randGen.Next(0, 100) < trueChance ? 1 : 0;
            //     if (x == 1)
            //     {
            //         trueCount++;
            //     }
            //     else
            //     {
            //         falseCount++;
            //     }
            // }
            //
            // Console.WriteLine("true " + $"{trueCount:N4}");
            // Console.WriteLine("false " +  $"{falseCount:N4}");

            // Planet1 = planetsList[0];
            // KnownPlanets.Add(Planet1);
            // Player1 = new Player("David", Planet1, Planet1, Ship1, knownPlanets, Planet1.X, Planet1.Y, Planet1.Z, 1);
            // Ship1  = new Ship("Galactic Cruiser", "Exploration Vessel", 900, 0.72, true, false, 0, 5, null, Planet1, 1);
            // AvailableShips.Add(Ship1);
            // Introduction();
            // FrequencyChecker(50);
        }

        private static void FrequencyChecker(int range)
        {
            PlanetGeneratorManager pgManager = new PlanetGeneratorManager();

            var counts = new Dictionary<string, int>();

            for (int i = 0; i < range; i++)
            {
                var generatedType = pgManager.GenerateType();

                if (counts.ContainsKey(generatedType))
                {
                    counts[generatedType]++;
                }
                else
                {
                    counts[generatedType] = 1;
                }
            }

            foreach (var kvp in counts.OrderBy(x => x.Value))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
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
                        ShowAllPlanets(PlanetsList);
                        break;
                    case ConsoleKey.S:
                        Console.WriteLine("\n");
                        ShowAllStars(StarsList);
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
}