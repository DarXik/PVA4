﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace NebulaNexus
{
    internal class Program : Debugger
    {
        private static List<Planet> PlanetsList = new List<Planet>();
        private static List<Planet> KnownPlanets = new List<Planet>();
        private static List<Star> StarsList = new List<Star>();
        private static List<Ship> AvailableShips = new List<Ship>();
        private static List<SolarSystem> SolarSystemsList = new List<SolarSystem>();
        private static Player Player1;

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
            var sgManager = new StarGeneratorManager();
            var ssManger = new SolarSystemGenerator();

            // PLANETS
            for (int i = pgManager.PossiblePlanetNames.Count - 1; i >= 0; i--)
            {
                PlanetsList.Add(pgManager.CreatePlanet());
            }

            Debugger.GeneratedPlanets = PlanetsList.Count;

            // STARS
            for (int i = 0; i < SolarSystemGenerator.PossibleSolarSystems.Count;)
            {
                var star1 = sgManager.CreateStar();
                StarsList.Add(star1);
                sgManager.RemoveName(star1.Name);
                i++;
                // bool alreadyExists = false;
                //
                // foreach (var star in StarsList)
                // {
                //     if (star.SolarSystem == star1.SolarSystem)
                //     {
                //         alreadyExists = true;
                //         break;
                //     }
                // }
                //
                // if (!alreadyExists)
                // {
                //
                // }
            }

            Debugger.GeneratedStars = StarsList.Count;

            // SOLAR SYSTEMS
            foreach (var star in StarsList)
            {
                var solarSystem = ssManger.GenerateSolarSystem(SolarSystemGenerator.AssignPlanets(PlanetsList), star);
                SolarSystemsList.Add(solarSystem);
            }

            foreach (var planet in PlanetsList)
            {
                if (planet.SolarSystem == null)
                {
                    var list = SolarSystemsList.OrderBy(ss => ss.Planets.Count).ToList();
                    foreach (var ss in SolarSystemsList)
                    {
                        if (ss.Planets.Count > 0)
                        {
                            var smallestSystem = list.First();
                            planet.SolarSystem = smallestSystem;
                            smallestSystem.Planets.Add(planet);
                            break;
                        }
                    }
                }
            }

            Debugger.GeneratedSystems = SolarSystemsList.Count;
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
                        ShowPlanets(PlanetsList);
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
                        ShowPlanets(KnownPlanets);
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

        private static void ShowPlanets(List<Planet> planetsList)
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
                    $"{planet.SolarSystem?.Name}",
                    $"{planet.SolarSystem?.MainStar.Name}",
                    $"{planet.Coordinates.X}",
                    $"{planet.Coordinates.Y}",
                    $"{planet.Coordinates.Z}",
                    $"{planet.Id}"
                );
            }

            table.Columns[4].Width(10);
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
                    $"{star.Coordinates.X}",
                    $"{star.Coordinates.Y}",
                    $"{star.Coordinates.Z}",
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
                .AddColumn(new TableColumn("[aqua]Solar System ID[/]"));
            foreach (var solarSystem in systemsList)
            {
                table.AddRow(
                    $"{solarSystem.Name}",
                    $"{solarSystem.Coordinates.X:N0}",
                    $"{solarSystem.Coordinates.Y:N0}",
                    $"{solarSystem.Coordinates.Z:N0}",
                    $"{Convert.ToDecimal(solarSystem.Radius)} light yrs",
                    $"{solarSystem.RadiusKm:N0} km",
                    $"{string.Join(", ", solarSystem.Planets.Select(planet => planet.Name))}",
                    $"{solarSystem.MainStar.Name}",
                    solarSystem.MainStar.Radius > 0 ? $"{solarSystem.MainStar.Radius:N0} km" : "Undefined",
                    $"{solarSystem.Id}"
                );
            }

            AnsiConsole.Write(table);
        }
    }

    public static class PlanetTraveller
    {
        public static async Task TravelToPlanet(List<Planet> planetsList, List<SolarSystem> systemsList, Player player)
        {
            Console.WriteLine("\n");

            var destinationOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose to travel to either planet or solar system or choose random")
                    .PageSize(3)
                    .AddChoices("Known planet", "Known system", "Random"));

            switch (destinationOption.ToLower())
            {
                case "known planet":
                    AnsiConsole.Markup($"You chose to travel to a known planet \n");
                    var planetPrompt =
                        new SelectionPrompt<Planet>()
                            .Title("Choose from known planets: ")
                            .PageSize(3);

                    foreach (var planet in planetsList)
                    {
                        planetPrompt.AddChoice(planet);
                    }

                    var chosenPlanet = AnsiConsole.Prompt(planetPrompt);
                    AnsiConsole.Markup($"Chosen planet: [fuchsia]{chosenPlanet}[/]");
                    Console.WriteLine("\n");

                    await TravelToPlanetAsync(chosenPlanet);

                    player.CurrentPlanet = chosenPlanet;
                    break;
                case "known system":
                    break;
                case "random":
                    break;
            }
        }

        private static async Task TravelToPlanetAsync(Planet chosenPlanet)
        {
            await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .StartAsync("Calculating directions...", async ctx =>
                {
                    await Task.Delay(2000);
                    ctx.Spinner(Spinner.Known.Arrow3);
                    ctx.SpinnerStyle(Style.Parse("red"));
                    ctx.Status("Directions obtained.");

                    AnsiConsole.MarkupLine("[bold green]Directions obtained.[/]");
                });

            await AnsiConsole.Status()
                .StartAsync("Checking remaining fuel", async ctx =>
                {
                    await Task.Delay(1000);
                    ctx.Status("Fuel checked.");

                    AnsiConsole.MarkupLine("[bold green]Fuel checked.[/]");
                });
            await AnsiConsole.Status()
                .StartAsync("Initializing Hyperdrive...", async ctx =>
                {
                    await Task.Delay(2500);
                    ctx.Status("Hyperdrive initialized.");

                    AnsiConsole.MarkupLine("[bold green]Hyperdrive initialized.[/]");
                });
            await AnsiConsole.Status()
                .StartAsync("Preparing to jump to Hyperspace...", async ctx =>
                {
                    await Task.Delay(1500);
                    ctx.Status("Ready to jump.");

                    AnsiConsole.MarkupLine("[bold green]Ready to jump.[/]");
                    await Task.Delay(1500);
                    ctx.Status("Jumping to Hyperspace!");
                    AnsiConsole.MarkupLine("[bold aqua]Jumping to Hyperspace![/]");
                    await Task.Delay(500);
                    ctx.Status("Ready to travel.");
                });

            await AnsiConsole.Progress()
                .Columns(new TaskDescriptionColumn(), new RemainingTimeColumn())
                .StartAsync(async ctx =>
                {
                    var task1 = ctx.AddTask($"Travelling to [fuchsia]{chosenPlanet}[/]");
                    while (!ctx.IsFinished)
                    {
                        await Task.Delay(25);
                        task1.Increment(1);
                    }
                });

            AnsiConsole.Markup(
                $"You travelled to {chosenPlanet.PlanetType} planet [fuchsia]{chosenPlanet}[/] in [dodgerblue1]{chosenPlanet.SolarSystem.Name}[/] system, " +
                $"current population is [yellow]{chosenPlanet.Population:N0}[/] people " +
                $"and the planet is {(chosenPlanet.IsDemocratic ? "[green]democratic[/]" : "[red]not democratic[/]")}.");
        }
    }

    public abstract class Debugger
    {
        protected static readonly List<int> AssignedPlanetsModifier = new List<int>();
        protected static int GeneratedPlanets;
        protected static int GeneratedStars;
        protected static int GeneratedSystems;

        public static void ShowDebug()
        {
            Console.WriteLine("Used modifier for assigning planets: ");
            foreach (var modifier in AssignedPlanetsModifier)
            {
                Console.Write(modifier + " ");
            }

            Console.WriteLine();
            Console.WriteLine($"Number of generated planets: {GeneratedPlanets}");
            Console.WriteLine($"Number of generated stars: {GeneratedStars}");
            Console.WriteLine($"Number of generated systems: {GeneratedSystems}");
        }
    }
}