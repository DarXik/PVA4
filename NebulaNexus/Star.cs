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
        public SolarSystem SolarSystem { get; set; }
        public Coordinate LocalCoordinates { get; }
        public Coordinate GlobalCoordinates { get; }

        public Star(string name, string type, BigInteger mass, long radius,
            double temperature, long age, object availableEnergy, SolarSystem solarSystem)
        {
            Mass = mass;
            Radius = radius;
            Temperature = temperature;
            Age = age;
            AvailableEnergy = availableEnergy;
            Id = UniqueId.GenerateId();
            Name = name;
            Type = type;
            SolarSystem = solarSystem;
            LocalCoordinates = new Coordinate(0, 0, 0);
            GlobalCoordinates = SolarSystem.Coordinates;
        }
    }
}