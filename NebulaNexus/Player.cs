using System.Collections.Generic;

namespace NebulaNexus
{
    public class Player : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int TechnologicalLevel;
        public int DiplomacyLevel;
        public int Trustworthiness;
        public bool IsAlive { get; set; }

        public List<Planet> KnownPlanets { get; set; }
        public List<Ship> Fleet { get; set; }

        public Planet HomePlanet;
        public Planet CurrentPlanet;
        public Ship CurrentShip;

        public Player(string name, Planet homePlanet, Planet currentPlanet, Ship currentShip, List<Planet> knownPlanets, float x, float y, float z, int id) : base(x, y, z)
        {
            Name = name;
            HomePlanet = homePlanet;
            CurrentShip = currentShip;
            CurrentPlanet = currentPlanet;
            Id = id + 2000;
            TechnologicalLevel = 3;
            DiplomacyLevel = 0;
            Trustworthiness = 10;
            IsAlive = true;
            KnownPlanets = knownPlanets;
            Fleet = new List<Ship>();
        }
    }
}