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

		public static Orientation ClockwiseRotation(Orientation orientation)
		{
			if (orientation == Orientation.Bottom)
				return Orientation.Left;
			if (orientation == Orientation.Right)
				return Orientation.Bottom;
			if (orientation == Orientation.Top)
				return Orientation.Right;
			return Orientation.Top;
		}

		public static Orientation CounterclockwiseRotation(Orientation orientation)
		{
			if (orientation == Orientation.Bottom)
				return Orientation.Right;
			if (orientation == Orientation.Right)
				return Orientation.Top;
			if (orientation == Orientation.Top)
				return Orientation.Left;
			return Orientation.Bottom;
		}
	}
}
