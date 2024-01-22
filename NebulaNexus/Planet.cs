namespace NebulaNexus
{
    public class Planet : IGameObject
    {
        public string Name { get; }
        public int Id { get; }
        public long Radius { get; }
        public string PlanetType { get; }
        public long Population;
        public int TechnologicalLevel;
        public int MilitaryPower;
        public bool IsDemocratic;
        public string SolarSystem { get; }
        public Coordinate Coordinates { get; }

        public Planet(string name, long radius, string planetType, long population, int technologicalLevel,
            int militaryPower, bool isDemocratic, string solarSystem, float x, float y, float z)
        {
            PlanetType = planetType;
            Population = population;
            TechnologicalLevel = technologicalLevel;
            IsDemocratic = isDemocratic;
            Id = UniqueID.GenerateID();
            MilitaryPower = militaryPower;
            Name = name;
            Radius = radius;
            SolarSystem = solarSystem;
            Coordinates = new Coordinate(x, y, z);
        }
    }
}