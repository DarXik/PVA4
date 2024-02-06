using System.Collections.Generic;

namespace NebulaNexus
{
    public class Player : IGameObject
    {
        public string Name { get; }
        public int Id { get; }
        public int TechnologicalLevel;
        public int DiplomacyLevel;
        public int Trustworthiness;
        public bool IsAlive { get; set; }

        public List<Planet> KnownPlanets { get; set; }
        public List<Ship> Fleet { get; set; }

        public Planet HomePlanet { get; }
        public Planet CurrentPlanet;
        public Ship CurrentShip;

        public Player(string name, Planet homePlanet, List<Planet> knownPlanets)
        {
            Name = name;
            HomePlanet = homePlanet;
            CurrentShip = null;
            CurrentPlanet = homePlanet;
            Id = UniqueId.GenerateId();
            TechnologicalLevel = homePlanet.TechnologicalLevel;
            DiplomacyLevel = 0;
            Trustworthiness = 10;
            IsAlive = true;
            KnownPlanets = knownPlanets;
            Fleet = new List<Ship>();
        }
    }
}