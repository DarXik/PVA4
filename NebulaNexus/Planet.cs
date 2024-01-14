namespace NebulaNexus
{
    public class Planet : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public long Radius;
        public string PlanetType;
        public long Population;
        public int TechnologicalLevel;
        public int MilitaryPower;
        public bool IsDemocratic;
        public string SolarSystem;

        public Planet(string name, long radius, string planetType, long population, int technologicalLevel, int militaryPower, bool isDemocratic, string solarSystem,
            float x,
            float y,
            float z,
            int id) : base(x, y, z)
        {
            PlanetType = planetType;
            Population = population;
            TechnologicalLevel = technologicalLevel;
            IsDemocratic = isDemocratic;
            Id = id;
            MilitaryPower = militaryPower;
            Name = name;
            Radius = radius;
            SolarSystem = solarSystem;
        }
    }
}