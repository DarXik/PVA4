using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spectre.Console;

namespace NebulaNexus
{
    public static class PlanetTraveller
    {
        public static async Task TravelToPlanet(List<Planet> planetsList, List<SolarSystem> systemsList, Player player)
        {
            Console.WriteLine("\n");

            var destinationOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose to travel to either a planet or solar system or choose random")
                    .PageSize(3)
                    .AddChoices("Known planet", "Known system", "Random"));

            switch (destinationOption.ToLower())
            {
                case "known planet":
                    AnsiConsole.Markup(
                        $"You are currently on the planet [fuchsia]{player.CurrentPlanet}[/] in [dodgerblue1]{player.CurrentPlanet.SolarSystem.Name}[/] system. \n");
                    var planetPrompt =
                        new SelectionPrompt<Planet>()
                            .Title("Choose from known planets: ")
                            .PageSize(3);

                    foreach (var planet in planetsList)
                    {
                        if (planet != player.CurrentPlanet)
                        {
                            planetPrompt.AddChoice(planet);
                        }
                    }

                    var chosenPlanet = AnsiConsole.Prompt(planetPrompt);
                    AnsiConsole.Markup(
                        $"You chose to travel to [fuchsia]{chosenPlanet}[/] in [dodgerblue1]{(player.CurrentPlanet.SolarSystem.Name == chosenPlanet.SolarSystem.Name ? $"[dodgerblue1]{chosenPlanet.SolarSystem.Name}[/]" : $"[slateblue1]{chosenPlanet.SolarSystem.Name}[/]")}[/] system. \n");

                    await TravelToPlanetAsync(chosenPlanet, player);
                    break;


                case "known system":
                    AnsiConsole.Markup("You chose to travel to a known solar system\n");
                    var systemPrompt =
                        new SelectionPrompt<SolarSystem>()
                            .Title("Choose from available systems: ")
                            .PageSize(3);
                    var knownSystems = new List<SolarSystem>();
                    foreach (var planet in planetsList)
                    {
                        if (!knownSystems.Contains(planet.SolarSystem))
                        {
                            knownSystems.Add(planet.SolarSystem);
                        }
                    }

                    foreach (var system in knownSystems)
                    {
                        systemPrompt.AddChoice(system);
                    }

                    var chosenSystem = AnsiConsole.Prompt(systemPrompt);
                    AnsiConsole.Markup($"Chosen system: [fuchsia]{chosenSystem.Name}[/]");

                    var planetInSystem =
                        new SelectionPrompt<Planet>()
                            .Title($"You know these planets in {chosenSystem.Name}")
                            .PageSize(3);

                    foreach (var planet in planetsList)
                    {
                        if (planet.SolarSystem == chosenSystem)
                        {
                            planetInSystem.AddChoice(planet);
                        }
                    }

                    var chosenPlanetInSystem = AnsiConsole.Prompt(planetInSystem);
                    AnsiConsole.Markup(
                        $"You chose to travel to [fuchsia]{chosenPlanetInSystem.Name}[/] in [dodgerblue1]{chosenPlanetInSystem.SolarSystem}[/].\n");

                    await TravelToPlanetAsync(chosenPlanetInSystem, player);
                    break;

                case "random":
                    AnsiConsole.Markup("A random place will be chosen..\n");
                    Thread.Sleep(1000);
                    await TravelToPlanetAsync(planetsList[new Random().Next(planetsList.Count)], player);
                    break;
                default:
                    Console.WriteLine("\n");
                    Console.WriteLine("This key does nothing.. try again");
                    break;
            }
        }

        private static async Task TravelToPlanetAsync(Planet chosenPlanet, Player player)
        {
            var distanceToTravel = Math.Sqrt(
                Math.Pow(player.CurrentPlanet.Coordinates.X - chosenPlanet.Coordinates.X, 2) +
                Math.Pow(player.CurrentPlanet.Coordinates.Y - chosenPlanet.Coordinates.Y, 2) +
                Math.Pow(player.CurrentPlanet.Coordinates.Z - chosenPlanet.Coordinates.Z, 2));

            var speedOfLight = 299792; // Speed of light in kilometers per second
            var shipSpeed = 1000 * speedOfLight;
            var timeToTravel = distanceToTravel / shipSpeed;
            var delay = 100;
            var increments = (int) (timeToTravel / delay); // number of increments needed to match the complete time

            var rnd = new Random();

            await AnsiConsole.Status()
                .StartAsync("Calculating directions...", async ctx =>
                {
                    await Task.Delay(rnd.Next(1500, 3000));
                    ctx.Status("Directions obtained.");

                    AnsiConsole.MarkupLine("[bold green]Directions obtained.[/]");
                });

            await AnsiConsole.Status()
                .StartAsync("Checking remaining fuel", async ctx =>
                {
                    await Task.Delay(rnd.Next(750, 1250));
                    ctx.Status("Fuel checked.");

                    AnsiConsole.MarkupLine("[bold green]Fuel checked.[/]");
                });
            await AnsiConsole.Status()
                .StartAsync("Initializing Hyperdrive...", async ctx =>
                {
                    await Task.Delay(rnd.Next(2000, 3500));
                    ctx.Status("Hyperdrive initialized.");

                    AnsiConsole.MarkupLine("[bold green]Hyperdrive initialized.[/]");
                });
            await AnsiConsole.Status()
                .StartAsync("Preparing to jump to Hyperspace...", async ctx =>
                {
                    await Task.Delay(rnd.Next(750, 2000));
                    ctx.Status("Ready to jump.");
                    AnsiConsole.MarkupLine("[bold green]Ready to jump.[/]");

                    await Task.Delay(rnd.Next(500, 1750));
                    ctx.Status("[bold aqua]Jumping to Hyperspace![/]");
                    await Task.Delay(500);
                    AnsiConsole.MarkupLine("[bold green]Jumped to Hyperspace.[/]");
                });

            await AnsiConsole.Progress()
                .AutoClear(true)
                .Columns(new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn())
                .StartAsync(async ctx =>
                {
                    var task1 = ctx.AddTask($"Travelling to [fuchsia]{chosenPlanet}[/]");
                    var counter = 0;
                    while (!ctx.IsFinished && counter < increments)
                    {
                        await Task.Delay(rnd.Next(delay, 250));
                        task1.Increment(1);
                        counter++;
                    }
                });

            player.CurrentPlanet = chosenPlanet;
            AnsiConsole.Markup(
                "\n************************************************************************************\n" +
                "\n" +
                "[underline]WELCOME![/]\n" +
                $"\nYou travelled to {chosenPlanet.PlanetType} planet [fuchsia]{chosenPlanet}[/] in [dodgerblue1]{chosenPlanet.SolarSystem.Name}[/] system, " +
                $"\ncurrent population is [yellow]{chosenPlanet.Population:N0}[/] people " +
                $"and the planet is {(chosenPlanet.IsDemocratic ? "[green]democratic[/]" : "[red]not democratic[/]")}." +
                "\n" +
                "\n************************************************************************************");
        }
    }
}