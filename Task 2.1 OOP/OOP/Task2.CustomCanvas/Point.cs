namespace CustomCanvas
{
    public struct Point
	{
		public readonly int X;
		public readonly int Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Point Shift(int dx, int dy)
        {
			checked
            {
				int x = X + dx;
				int y = Y + dy;

				return new Point(x, y);
			}
        }

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (!(obj is Point))
				return false;

			Point another = (Point)obj;
			return another.X == X && another.Y == Y;
		}

		public override int GetHashCode() => (X << 16) ^ Y;

		public override string ToString() => string.Format("[{0},{1}]", X, Y);

		public static bool operator ==(Point p1, Point p2) => p1.X == p2.X && p1.Y == p2.Y;
		public static bool operator !=(Point p1, Point p2) => !(p1 == p2);
		public static Point operator +(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);
		public static Point operator -(Point p1, Point p2) => new Point(p1.X - p2.X, p1.Y - p2.Y);
	}
}
