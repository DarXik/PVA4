using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace NebulaNexus
{
    public static class Shower
    {
        public static void ShowPlayerStats(Player player, List<Ship> availableShips)
        {
            var table = new Table()
                .Border(TableBorder.Ascii2)
                // .BorderColor(Color.Silver)
                .AddColumn(new TableColumn("[wheat4]Name[/]"))
                .AddColumn(new TableColumn("[wheat4]Tech. Lvl[/]"))
                .AddColumn(new TableColumn("[wheat4]Diplomacy Lvl[/]"))
                .AddColumn(new TableColumn("[wheat4]Trustworthiness[/]"))
                .AddColumn(new TableColumn("[wheat4]Home Planet[/]"))
                .AddColumn(new TableColumn("[wheat4]Current Planet[/]"));


            table.AddRow(
                $"{player.Name}",
                $"{player.TechnologicalLevel}",
                $"{player.DiplomacyLevel}",
                $"{player.Trustworthiness}",
                $"{player.HomePlanet.Name}",
                $"{player.CurrentPlanet.Name}"
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
                    $"{ship.Fuel} %",
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
            Console.WriteLine("\n");
            var table = new Table()
                .Border(TableBorder.Ascii2)
                // .BorderColor(Color.Silver)
                .AddColumn(new TableColumn("[olive]Name[/]"))
                .AddColumn(new TableColumn("[olive]Radius[/]"))
                .AddColumn(new TableColumn("[olive]Planet Type[/]"))
                .AddColumn(new TableColumn("[olive]Population[/]"))
                .AddColumn(new TableColumn("[olive]Tech. Lvl[/]"))
                .AddColumn(new TableColumn("[olive]Military Pwr" +
                                           "[/]"))
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
                    $"{planet.SolarSystem.Name}",
                    $"{planet.Coordinates.X:N0}",
                    $"{planet.Coordinates.Y:N0}",
                    $"{planet.Coordinates.Z:N0}",
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

        public static void ShowAllStars(List<Star> starsList)
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
                    $"{star.Id}"
                );
            }

            AnsiConsole.Write(table);
        }

        public static void ShowAllSolarSystems(List<SolarSystem> systemsList)
        {
            var table = new Table()
                .Border(TableBorder.Ascii2)
                .AddColumn(new TableColumn("[aqua]Solar System Name[/]"))
                .AddColumn(new TableColumn("[aqua]X Coord.[/]"))
                .AddColumn(new TableColumn("[aqua]Y Coord.[/]"))
                .AddColumn(new TableColumn("[aqua]Z Coord.[/]"))
                // .AddColumn(new TableColumn("[aqua]Radius[/]"))
                .AddColumn(new TableColumn("[aqua]Radius (km)[/]"))
                .AddColumn(new TableColumn("[aqua]Planets[/]"))
                .AddColumn(new TableColumn("[aqua]Main Star Name[/]"))
                .AddColumn(new TableColumn("[aqua]Main Star Radius[/]"))
                .AddColumn(new TableColumn("[aqua]ID[/]"));
            foreach (var solarSystem in systemsList)
            {
                table.AddRow(
                    $"{solarSystem.Name}",
                    $"{solarSystem.Coordinates.X:N0}",
                    $"{solarSystem.Coordinates.Y:N0}",
                    $"{solarSystem.Coordinates.Z:N0}",
                    // $"{Convert.ToDecimal(solarSystem.RadiusLY)} Lyrs",
                    $"{solarSystem.Radius:N0} km",
                    $"{string.Join(", ", solarSystem.Planets.Select(planet => planet.Name))}",
                    $"{solarSystem.MainStar.Name}",
                    solarSystem.MainStar.Radius > 0 ? $"{solarSystem.MainStar.Radius:N0} km" : "Undefined",
                    $"{solarSystem.Id}"
                );
            }

            // table.Columns[5].Width(10);
            AnsiConsole.Write(table);
        }
    }
}