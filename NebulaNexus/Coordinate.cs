namespace NebulaNexus
{
    public struct Coordinate
    {
        public object X { get; set; }
        public object Y { get; set; }
        public object Z { get; set; }

        public Coordinate(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}