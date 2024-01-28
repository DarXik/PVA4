using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;

namespace NebulaNexus
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Program : Debugger
    {
        private static List<Planet> PlanetsList = new List<Planet>();
        private static List<Planet> KnownPlanets = new List<Planet>();
        private static List<Star> StarsList = new List<Star>();
        private static List<Ship> AvailableShips = new List<Ship>();
        private static List<SolarSystem> SolarSystemsList = new List<SolarSystem>();
        // ReSharper disable once InconsistentNaming
        private static Player Player1;

        public static Task Main(string[] args)
        {
            Setup();

            GeneratePlayer();
            return Introduction();
        }


        private static void Setup()
        {
            var systemGenerator = new SolarSystemGenerator();
            var numberOfSystems = SolarSystemGenerator.PossibleSolarSystems.Count;


            // SOLAR SYSTEMS
            for (var index = 0; index < numberOfSystems; index++)
            {
                var solarSystem = systemGenerator.GenerateSolarSystem();
                SolarSystemsList.Add(solarSystem);
            }

            foreach (var system in SolarSystemsList)
            {
                foreach (var planet in system.Planets)
                {
                    PlanetsList.Add(planet);
                }
            }

            foreach (var star in SolarSystemsList)
            {
                StarsList.Add(star.MainStar);
            }


            GeneratedSystems = SolarSystemsList.Count;
            GeneratedStars = StarsList.Count;
            GeneratedPlanets = PlanetsList.Count;
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
                        .Title("\nThese are you options to continue: ")
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
                        Shower.ShowAllPlanets(PlanetsList);
                        break;
                    case "show all stars":
                        Console.WriteLine("\n");
                        Shower.ShowAllStars(StarsList);
                        break;
                    case "show all solar systems":
                        Console.WriteLine("\n");
                        Shower.ShowAllSolarSystems(SolarSystemsList);
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
            while (true)
            {
                var option1 = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Your options are: ")
                        .PageSize(4)
                        .HighlightStyle("bold yellow4")
                        .AddChoices(
                            "to show stats and ships",
                            "to show known planets",
                            "for travel to known places",
                            "for exploration",
                            "to go back to menu"
                        ));

                switch (option1)
                {
                    case "to show stats and ships":
                        Console.WriteLine("\n");
                        Shower.ShowPlayerStats(Player1, AvailableShips);
                        break;
                    case "to show known planets":
                        Shower.ShowAllPlanets(KnownPlanets);
                        Console.WriteLine("\n");
                        break;
                    case "for travel to known places":
                        await PlanetTraveller.TravelToPlanet(KnownPlanets, SolarSystemsList, Player1);
                        Console.WriteLine("\n");
                        break;
                    case "for exploration":
                        Console.WriteLine("\n");
                        break;
                    case "to go back to menu":
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
    }
}