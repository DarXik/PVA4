namespace NebulaNexus
{
    public struct Coordinate
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Coordinate(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}