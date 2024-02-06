namespace NebulaNexus
{
    public class Planet : IGameObject
    {
        public string Name { get; }
        public int Id { get; }
        public int Radius { get; }
        public string PlanetType { get; }
        public long Population;
        public int TechnologicalLevel;
        public int MilitaryPower;
        public bool IsDemocratic;
        public SolarSystem SolarSystem { get; }
        public Coordinate Coordinates { get; set; }

        public Planet(string name, int radius, string planetType, SolarSystem solarSystem, long population, int technologicalLevel,
            int militaryPower, bool isDemocratic)
        {
            PlanetType = planetType;
            Population = population;
            TechnologicalLevel = technologicalLevel;
            IsDemocratic = isDemocratic;
            Id = UniqueId.GenerateId();
            MilitaryPower = militaryPower;
            Name = name;
            Radius = radius;
            SolarSystem = solarSystem;
            Coordinates = new Coordinate();
        }

        public override string ToString() // pro selection u travel
        {
            return Name;
        }
    }
}