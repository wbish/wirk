namespace WiRK.Terminator
{
	public struct Coordinate
	{
		public int X;
		public int Y;

		public static bool operator ==(Coordinate a, Coordinate b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		public static bool operator !=(Coordinate a, Coordinate b)
		{
			return !(a == b);
		}

		public static Coordinate OutOfBounds = new Coordinate {X = -1, Y = -1};
	}
}
