using System;

namespace WiRK.Terminator
{
	public class Conveyer : Floor
	{
		public Orientation Enter { get; protected set; }

		public Orientation Exit { get; protected set; }

		public Rotation ConveyerRotation
		{
			get
			{
				if (Enter == Orientation.Top && Exit == Orientation.Bottom
					|| Enter == Orientation.Bottom && Exit == Orientation.Top
					|| Enter == Orientation.Left && Exit == Orientation.Right
					|| Enter == Orientation.Right && Exit == Orientation.Left)
					return Rotation.None;

				if (Enter == Orientation.Top && Exit == Orientation.Right
					|| Enter == Orientation.Right && Exit == Orientation.Bottom
					|| Enter == Orientation.Bottom && Exit == Orientation.Left
					|| Enter == Orientation.Left && Exit == Orientation.Top)
					return Rotation.Clockwise;

				if (Enter == Orientation.Top && Exit == Orientation.Left
					|| Enter == Orientation.Left && Exit == Orientation.Bottom
					|| Enter == Orientation.Bottom && Exit == Orientation.Right
					|| Enter == Orientation.Right && Exit == Orientation.Top)
					return Rotation.CounterClockwise;
				
				throw new InvalidOperationException();
			}
		}

		public Conveyer(Orientation enter, Orientation exit)
		{
			Enter = enter;
			Exit = exit;
		}

		public Conveyer()
		{
		}

		public void Convey(Robot robot)
		{
			// Convey the robot a single square
		}
	}
}
