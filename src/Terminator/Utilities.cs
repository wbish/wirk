using System;

namespace WiRK.Terminator
{
	public static class Utilities
	{
		public static Orientation GetOppositeOrientation(Orientation orientation)
		{
			switch (orientation)
			{
				case Orientation.Top:
					return Orientation.Bottom;
				case Orientation.Bottom:
					return Orientation.Top;
				case Orientation.Left:
					return Orientation.Right;
				case Orientation.Right:
					return Orientation.Left;
				default:
					throw new ArgumentException("orientation");
			}
		}
	}
}
