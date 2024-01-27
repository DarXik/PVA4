using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace NebulaNexus
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Program : Debugger
    {
        public static List<Planet> PlanetsList = new List<Planet>();
        public static List<Planet> KnownPlanets = new List<Planet>();
        public static List<Star> StarsList = new List<Star>();
        public static List<Ship> AvailableShips = new List<Ship>();
        public static List<SolarSystem> SolarSystemsList = new List<SolarSystem>();
        public static Player Player1;

        public static Task Main(string[] args)
        {
            Setup();

            GeneratePlayer();
            return Introduction();

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


        private static void Setup()
        {
            var pgManager = new PlanetGeneratorManager();
            var ssManger = new SolarSystemGenerator();
            var numberOfSystems = SolarSystemGenerator.PossibleSolarSystems.Count;


            // SOLAR SYSTEMS
            for (var index = 0; index < numberOfSystems; index++)
            {
                var solarSystem = ssManger.GenerateSolarSystem();
                SolarSystemsList.Add(solarSystem);
            }

            // PLANETS
            for (int i = pgManager.PossiblePlanetNames.Count - 1; i >= 0; i--)
            {
                var planet = pgManager.CreatePlanet();
                SolarSystemsList[SolarSystemsList.FindIndex(system => system == planet.SolarSystem)].Planets.Add(planet);
                PlanetsList.Add(planet);
            }

            foreach (var star in SolarSystemsList)
            {
                StarsList.Add(star.MainStar);
            }


            GeneratedSystems = SolarSystemsList.Count;
            GeneratedStars = StarsList.Count;
            GeneratedPlanets = PlanetsList.Count;

            // foreach (var planet in PlanetsList)
            // {
            //     if (planet.SolarSystem == null)
            //     {
            //         var list = SolarSystemsList.OrderBy(ss => ss.Planets.Count).ToList();
            //         foreach (var ss in SolarSystemsList)
            //         {
            //             if (ss.Planets.Count > 0)
            //             {
            //                 var smallestSystem = list.First();
            //                 planet.SolarSystem = smallestSystem;
            //                 smallestSystem.Planets.Add(planet);
            //                 break;
            //             }
            //         }
            //     }
            // }
        }

        private static async Task Introduction()
        {
            // AnsiConsole.Markup("Welcome to [green]Nebula Nexus[/] - space exploration and conquest");
            AnsiConsole.Write(
                new FigletText("Nebula Nexus")
                    .LeftJustified()
                    .Color(Color.Purple));

            while (true)
            {
                var option1 = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n These are you options to continue: ")
                        .PageSize(4)
                        .HighlightStyle("bold purple")
                        .AddChoices(
                            "show all planets",
                            "show all stars",
                            "show all solar systems",
                            "player options",
                            "exit and save",
                            "debug"
                        ));


                switch (option1)
                {
                    case "show all planets":
                        ShowAllPlanets(PlanetsList);
                        break;
                    case "show all stars":
                        Console.WriteLine("\n");
                        ShowAllStars(StarsList);
                        break;
                    case "show all solar systems":
                        Console.WriteLine("\n");
                        ShowAllSolarSystems(SolarSystemsList);
                        break;
                    case "exit and save":
                        Console.WriteLine("\n");
                        return;
                    case "player options":
                        Console.WriteLine("\n");
                        await PlayerOptions();
                        break;
                    case "debug":
                        ShowDebug();
                        Console.WriteLine("\n");
                        break;
                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("This does nothing.. try again");
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

            for (int i = 0; i < 4;)
            {
                var newPlanet = PlanetsList[new Random().Next(PlanetsList.Count)];
                if (!KnownPlanets.Contains(newPlanet))
                {
                    KnownPlanets.Add(newPlanet);
                    i++;
                }
            }


            // Ship1 = new Ship("Galactic Cruiser", "Exploration Vessel", 900, 0.72, true, false, 0, 5, null, Player1.CurrentPlanet, 1);
            // AvailableShips.Add(Ship1);
        }

        private static async Task PlayerOptions()
        {
            Console.WriteLine("Your player options are: ");

            while (true)
            {
                Console.WriteLine("[s] to show stats and ships");
                Console.WriteLine("[p] to show known planets");
                // Console.WriteLine("[t] for travel to known Solar Systems");
                Console.WriteLine("[t] for travel to known planets");
                Console.WriteLine("[e] for exploration");
                Console.WriteLine("[x] to go back to menu");


                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.S:
                        Console.WriteLine("\n");
                        ShowPlayerStats(Player1, AvailableShips);
                        break;
                    case ConsoleKey.P:
                        ShowAllPlanets(KnownPlanets);
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.T:
                        await PlanetTraveller.TravelToPlanet(KnownPlanets, SolarSystemsList, Player1);
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.E:
                        Console.WriteLine("\n");
                        break;
                    case ConsoleKey.X:
                        Console.WriteLine("\n");
                        await Introduction();
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
                $"{player.HomePlanet.SolarSystem.Name}"
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
            Console.WriteLine("\n");
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
                .AddColumn(new TableColumn("[olive]Solar System Star[/]"))
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
                    $"{planet.SolarSystem.Name}",
                    $"{planet.SolarSystem.MainStar.Name}",
                    $"{planet.Coordinates.X}",
                    $"{planet.Coordinates.Y}",
                    $"{planet.Coordinates.Z}",
                    $"{planet.Id}"
                );
            }

            // table.Collapse();
            AnsiConsole.Write(table);
            // foreach (Planet planet in planetsList)
            // {
            //     Console.WriteLine($"Planet Properties for {planet.Name}:");
            //     Console.WriteLine($"Name: {planet.Name}");
            //     Console.WriteLine($"Radius: {planet.Radius:N0} km");
            //     Console.WriteLine($"Planet Type: {planet.PlanetType}");
            //     Console.WriteLine($"Population: {planet.Population:N0}");
            //     Console.WriteLine($"Technological Level: {planet.TechnologicalLevel}");
            //     Console.WriteLine($"Military Power: {planet.MilitaryPower}");
            //     Console.WriteLine($"Is Democratic: {planet.IsDemocratic}");
            //     Console.WriteLine($"Solar System: {planet.SolarSystem?.Name}");
            //     Console.WriteLine($"Coordinates: ({planet.Coordinates.X}, {planet.Coordinates.Y}, {planet.Coordinates.Z})");
            //     Console.WriteLine($"Id: {planet.Id}");
            //     Console.WriteLine();
            // }
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
                .AddColumn(new TableColumn("[maroon]X Coord.[/]"))
                .AddColumn(new TableColumn("[maroon]Y Coord.[/]"))
                .AddColumn(new TableColumn("[maroon]Z Coord.[/]"))
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
                    $"{star.SolarSystem.Name}",
                    $"{star.LocalCoordinates.X}",
                    $"{star.LocalCoordinates.Y}",
                    $"{star.LocalCoordinates.Z}",
                    $"{star.Id}"
                );
            }

            AnsiConsole.Write(table);
        }

        private static void ShowAllSolarSystems(List<SolarSystem> systemsList)
        {
            var table = new Table()
                .Border(TableBorder.Ascii2)
                .AddColumn(new TableColumn("[aqua]Solar System Name[/]"))
                .AddColumn(new TableColumn("[aqua]X Coord.[/]"))
                .AddColumn(new TableColumn("[aqua]Y Coord.[/]"))
                .AddColumn(new TableColumn("[aqua]Z Coord.[/]"))
                .AddColumn(new TableColumn("[aqua]Radius[/]"))
                .AddColumn(new TableColumn("[aqua]Radius (km)[/]"))
                .AddColumn(new TableColumn("[aqua]Planets[/]"))
                .AddColumn(new TableColumn("[aqua]Main Star Name[/]"))
                .AddColumn(new TableColumn("[aqua]Main Star Radius[/]"))
                .AddColumn(new TableColumn("[aqua]ID[/]"));
            foreach (var solarSystem in systemsList)
            {
                table.AddRow(
                    $"{solarSystem.Name}",
                    $"{solarSystem.Coordinates.X:E2}",
                    $"{solarSystem.Coordinates.Y:E2}",
                    $"{solarSystem.Coordinates.Z:E2}",
                    $"{Convert.ToDecimal(solarSystem.RadiusLY)} Lyrs",
                    $"{solarSystem.Radius:N0} km",
                    $"{string.Join(", ", solarSystem.Planets.Select(planet => planet.Name))}",
                    $"{solarSystem.MainStar.Name}",
                    solarSystem.MainStar.Radius > 0 ? $"{solarSystem.MainStar.Radius:N0} km" : "Undefined",
                    $"{solarSystem.Id}"
                );
            }

            table.Columns[9].Width(8);
            AnsiConsole.Write(table);
        }
    }
}