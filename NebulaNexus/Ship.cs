using System.Collections.Generic;

namespace NebulaNexus
{
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
}