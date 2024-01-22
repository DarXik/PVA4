using System.Numerics;

namespace NebulaNexus
{
    public class Star : IGameObject
    {
        public string Name { get; }
        public int Id { get; }
        public string Type;
        public long Radius { get; }
        public BigInteger Mass;
        public double Temperature;
        public long Age;
        public object AvailableEnergy;
        public string SolarSystem { get; }
        public Coordinate Coordinates { get; }

        public Star(string name, string type, BigInteger mass, long radius, double temperature, long age,
            object availableEnergy, string solarSystem)
        {
            Mass = mass;
            Radius = radius;
            Temperature = temperature;
            Age = age;
            AvailableEnergy = availableEnergy;
            Id = UniqueID.GenerateID();
            Name = name;
            Type = type;
            SolarSystem = solarSystem;
            Coordinates = new Coordinate(0, 0, 0);
        }
    }
}