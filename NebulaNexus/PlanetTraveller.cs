using System;
using System.Collections.Generic;
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
                    AnsiConsole.Markup($"You chose to travel to a known solar sytem\n");
                    var systemPrompt =
                        new SelectionPrompt<Planet>()
                            .Title("Choose from available systems: ")
                            .PageSize(3);

                    foreach (var planet in planetsList)
                    {
                        systemPrompt.AddChoice(planet.SolarSystem.Name);
                    }

                    var chosenSystem = AnsiConsole.Prompt(systemPrompt);
                    AnsiConsole.Markup($"Chosen planet: [fuchsia]{chosenSystem.SolarSystem.Name}[/]");
                    Console.WriteLine("\n");

                    await TravelToPlanetAsync(chosenPlanet);

                    player.CurrentPlanet = chosenPlanet;

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
}