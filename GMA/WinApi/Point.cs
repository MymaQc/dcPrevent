namespace dcPrevent.GMA.WinApi {

    internal readonly struct Point {

        public readonly int X;
        public readonly int Y;

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Point a, Point b) => !(a == b);

        public bool Equals(Point other) => other.X == X && other.Y == Y;

        public override bool Equals(object obj) => obj != null && !(obj.GetType() != typeof(Point)) && Equals((Point)obj);

        public override int GetHashCode() => X * 397 ^ Y;

    }

}