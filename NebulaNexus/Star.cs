namespace NebulaNexus
{
    public class Star : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public float Mass;
        public float Temperature;
        public long Age;
        public float AvailableEnergy;
        public string SolarSystem;

        private const float X = 0.0f;
        private const float Y = 0.0f;
        private const float Z = 0.0f;

        public Star(string name, float mass, float temperature, long age, float availableEnergy, string solarSystem,
            int id) : base(X, Y, Z)
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
}