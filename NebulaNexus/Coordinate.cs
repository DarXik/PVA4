namespace NebulaNexus
{
    public struct Coordinate
    {
        public object X { get; set; }
        public object Y { get; set; }
        public object Z { get; set; }

        public Coordinate(object x, object y, object z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}