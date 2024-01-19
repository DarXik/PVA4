using System.Numerics;

namespace NebulaNexus
{
    public class Star : Coordinates, IGameObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Type;
        public long Radius;
        public BigInteger Mass;
        public double Temperature;
        public long Age;
        public object AvailableEnergy;
        public string SolarSystem;

        private new const float X = 0.0f;
        private new const float Y = 0.0f;
        private new const float Z = 0.0f;

        // public static Star Instance;

        public Star(string name, string type, BigInteger mass, long radius, double temperature, long age, object availableEnergy, string solarSystem,
            int id) : base(X, Y, Z)
        {
            Mass = mass;
            Radius = radius;
            Temperature = temperature - 273.15;
            Age = age;
            AvailableEnergy = availableEnergy;
            Id = id + 1000;
            Name = name;
            Type = type;
            SolarSystem = solarSystem;

            // Instance = this;
        }
    }
}