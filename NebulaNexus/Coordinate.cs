namespace NebulaNexus
{
    public struct Coordinate
    {
        public long X { get; }
        public long Y { get; }
        public long Z { get; }

        public Coordinate(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}