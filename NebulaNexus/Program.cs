using System;
using System.Collections.Generic;
using Spectre.Console;

namespace NebulaNexus
{
    internal class Program
    {
        static Planet Planet1 = new Planet("Nexus", 9748, "Icy", 1000, 3, 0, true, "Andromeda", 1);
        static Planet Planet2 = new Planet("Aldoria", 5001, "EarthLike", 10004561, 3, 2, true, "Alpha Centauri", 2);
        static Star Star1 = new Star("Cepheda", 88575875f, 5421.3f, 14000800090, 99999999999999, "Andromeda", 1);
        static Player Player1 = new Player("David", 1);


        public static void Main(string[] args)
        {
            var planetsList = new List<Planet>() {Planet1, Planet2};
            var starsList = new List<Star>() {Star1};

            ShowAllPlanets(planetsList);
            ShowAllStars(starsList);
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
                    $"{planet.Radius.ToString()} km",
                    $"{planet.PlanetType}",
                    $"{planet.Population.ToString()}",
                    $"{planet.TechnologicalLevel.ToString()}",
                    $"{planet.IsDemocratic.ToString()}",
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

    public class Player : IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int TechnologicalLevel;
        public int DiplomacyLevel;
        public int Trustworthiness;
        public bool IsAlive { get; set; }

        public List<Planet> ControlledPlanets { get; set; }
        public List<Ship> Fleet { get; set; }

        public Player(string name, int id)
        {
            Name = name;
            Id = id + 2000;
            TechnologicalLevel = 3;
            DiplomacyLevel = 0;
            Trustworthiness = 10;
            IsAlive = true;

            ControlledPlanets = new List<Planet>();
            Fleet = new List<Ship>();
        }
    }

    public class Ship : IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class Star : IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public float Mass;
        public float Temperature;
        public long Age;
        public float AvailableEnergy;
        public string SolarSystem;

        public Star(string name, float mass, float temperature, long age, float availableEnergy, string solarSystem, int id)
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

    public class Planet : IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long Radius;
        public string PlanetType;
        public long Population;
        public int TechnologicalLevel;
        public int MilitaryForce;
        public bool IsDemocratic;
        public string SolarSystem;

        public Planet(string name, long radius, string planetType, long population, int technologicalLevel, int militaryForce, bool isDemocratic, string solarSystem, int id)
        {
            PlanetType = planetType;
            Population = population;
            TechnologicalLevel = technologicalLevel;
            IsDemocratic = isDemocratic;
            Id = id;
            MilitaryForce = militaryForce;
            Name = name;
            Radius = radius;
            SolarSystem = solarSystem;
        }
    }
}