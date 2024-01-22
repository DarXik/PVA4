using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Spectre.Console;

namespace NebulaNexus
{
    internal class Program
    {
        private static List<Planet> PlanetsList = new List<Planet>();
        private static List<Planet> KnownPlanets = new List<Planet>();
        private static List<Star> StarsList = new List<Star>();
        private static List<Ship> AvailableShips = new List<Ship>();
        private static Player Player1;

        public static readonly string[] possibleSolarSystems =
        {
            "Andromeda", "Nova Ecliptic Realm", "Hyperion Star Cluster", "Astralis", "Shili", "Nova System", "Umbraflora Haven", "Galaxion", "unknown"
        };

        public static void Main(string[] args)
        {
            var pgManager = new PlanetGeneratorManager();
            var sgManager = new StarGeneratorManager();

            for (int i = pgManager.PossiblePlanetNames.Count - 1; i >= 0; i--)
            {
                PlanetsList.Add(pgManager.CreatePlanet());
            }

            for (int i = 0; i < possibleSolarSystems.Length; i++)
            {
                var star1 = sgManager.CreateStar();

                bool alreadyExists = false;

                foreach (var star in StarsList)
                {
                    if (star.SolarSystem == star1.SolarSystem)
                    {
                        alreadyExists = true;
                        break;
                    }
                }

                if (!alreadyExists)
                {
                    StarsList.Add(star1);
                }
            }


            GeneratePlayer();
            Introduction();


            // var list1 = new List<double>();
            //
            // for (int j = 0; j < 100; j++)
            // {
            //     var tempList = new List<BigInteger>();
            //     for (int i = 0; i < 10000; i++)
            //     {
            //         var tempStar = sgManager.GenerateMass(1);
            //
            //         tempList.Add(tempStar);
            //     }
            //
            //     tempList.Sort();
            //     tempList.Reverse();
            //     int counter = 0;
            //     foreach (var temp in tempList)
            //     {
            //         if (temp == 0)
            //         {
            //             counter++;
            //         }
            //     }
            //
            //     BigInteger sum = 0;
            //     foreach (var t in tempList)
            //     {
            //         sum += t;
            //     }
            //
            //     double percentage = ((double) counter / tempList.Count) * 100;
            //     Console.WriteLine(sum / tempList.Count);
            //     Console.WriteLine($"Počet nul: {counter} -> {percentage}%");
            //
            //     list1.Add(percentage);
            // }
            //
            // double sum1 = 0;
            // foreach (var VARIABLE in list1)
            // {
            //     sum1 += VARIABLE;
            // }
            // Console.WriteLine($"Finální průměr: {sum1 / list1.Count}%");
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
                        return;
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

        private static void GeneratePlayer()
        {
            var possiblePlayerNames = new List<string>()
            {
                "HanSolo",
                "GalaxyGiggler",
                "JediJester",
                "NebulaNinja",
                "QuasarQuipster",
                "AstroChuckle",
                "WarpWhisperer",
                "Snicker",
                "Astrid",
                "Cale"
            };

            foreach (var planet in PlanetsList)
            {
                if (planet.Population > 0 && planet.IsDemocratic && planet.TechnologicalLevel > 2)
                {
                    Player1 = new Player(possiblePlayerNames[new Random().Next(possiblePlayerNames.Count)], planet, KnownPlanets);
                    KnownPlanets.Add(Player1.HomePlanet);
                    break;
                }
            }
            // Ship1 = new Ship("Galactic Cruiser", "Exploration Vessel", 900, 0.72, true, false, 0, 5, null, Player1.CurrentPlanet, 1);
            // AvailableShips.Add(Ship1);
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
                .AddColumn(new TableColumn("[wheat4]Name[/]"))
                .AddColumn(new TableColumn("[wheat4]Technological Level[/]"))
                .AddColumn(new TableColumn("[wheat4]Diplomacy Level[/]"))
                .AddColumn(new TableColumn("[wheat4]Trustworthiness[/]"))
                .AddColumn(new TableColumn("[wheat4]Home Planet[/]"))
                .AddColumn(new TableColumn("[wheat4]Home Solar System[/]"));


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
                .AddColumn(new TableColumn("[olive]Population[/]"))
                .AddColumn(new TableColumn("[olive]Technological Level[/]"))
                .AddColumn(new TableColumn("[olive]Military Power[/]"))
                .AddColumn(new TableColumn("[olive]Democratic[/]"))
                .AddColumn(new TableColumn("[olive]Solar System[/]"))
                .AddColumn(new TableColumn("[olive]X Coordinate[/]"))
                .AddColumn(new TableColumn("[olive]Y Coordinate[/]"))
                .AddColumn(new TableColumn("[olive]Z Coordinate[/]"))
                .AddColumn(new TableColumn("[olive]ID[/]"));

            foreach (var planet in planetsList)
            {
                table.AddRow(
                    $"{planet.Name}",
                    $"{planet.Radius:N0} km",
                    $"{planet.PlanetType}",
                    $"{planet.Population:N0}",
                    $"{planet.TechnologicalLevel}",
                    $"{planet.MilitaryPower}",
                    $"{planet.IsDemocratic}",
                    $"{planet.SolarSystem}",
                    $"{planet.Coordinates.X}",
                    $"{planet.Coordinates.Y}",
                    $"{planet.Coordinates.Z}",
                    $"{planet.Id}"
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
                .AddColumn(new TableColumn("[maroon]Star Name[/]"))
                .AddColumn(new TableColumn("[maroon]Radius[/]"))
                .AddColumn(new TableColumn("[maroon]Type[/]"))
                .AddColumn(new TableColumn("[maroon]Age[/]"))
                .AddColumn(new TableColumn("[maroon]Temperature[/]"))
                .AddColumn(new TableColumn("[maroon]Mass[/]"))
                .AddColumn(new TableColumn("[maroon]Available Energy[/]"))
                .AddColumn(new TableColumn("[maroon]Solar System[/]"))
                .AddColumn(new TableColumn("[maroon]X Coordinate[/]"))
                .AddColumn(new TableColumn("[maroon]Y Coordinate[/]"))
                .AddColumn(new TableColumn("[maroon]Z Coordinate[/]"))
                .AddColumn(new TableColumn("[maroon]ID[/]"));

            foreach (var star in starsList)
            {
                table.AddRow(
                    $"{star.Name}",
                    star.Radius > 0 ? $"{star.Radius:N0} km" : "Undefined",
                    $"{star.Type}",
                    $"{star.Age:N0} yrs",
                    star.Temperature > 0 ? $"{star.Temperature}°C" : "Undefined",
                    $"{star.Mass:e2} kg",
                    $"{star.AvailableEnergy:e2} W",
                    $"{star.SolarSystem}",
                    $"{star.Coordinates.X}",
                    $"{star.Coordinates.Y}",
                    $"{star.Coordinates.Z}",
                    $"{star.Id}"
                );
            }

            AnsiConsole.Write(table);
        }

        private static void TravelToPlanet()
        {

        }
    }
}