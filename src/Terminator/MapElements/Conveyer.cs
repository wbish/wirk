using System;
using System.Collections.Generic;

namespace WiRK.Terminator
{
	public class Conveyer : Floor
	{
		public List<Orientation> Entrances { get; protected set; }

		public Orientation Exit { get; protected set; }

		public Rotation ConveyerRotation(Orientation entrance)
		{
			//if (Enter == Orientation.Top && Exit == Orientation.Bottom
			//	|| Enter == Orientation.Bottom && Exit == Orientation.Top
			//	|| Enter == Orientation.Left && Exit == Orientation.Right
			//	|| Enter == Orientation.Right && Exit == Orientation.Left)
			//	return Rotation.None;

			//if (Enter == Orientation.Top && Exit == Orientation.Right
			//	|| Enter == Orientation.Right && Exit == Orientation.Bottom
			//	|| Enter == Orientation.Bottom && Exit == Orientation.Left
			//	|| Enter == Orientation.Left && Exit == Orientation.Top)
			//	return Rotation.Clockwise;

			//if (Enter == Orientation.Top && Exit == Orientation.Left
			//	|| Enter == Orientation.Left && Exit == Orientation.Bottom
			//	|| Enter == Orientation.Bottom && Exit == Orientation.Right
			//	|| Enter == Orientation.Right && Exit == Orientation.Top)
			//	return Rotation.CounterClockwise;

			throw new InvalidOperationException();
		}

		public Conveyer(Orientation enter, Orientation exit)
			: this(new[] { enter }, exit)
		{

		}

		public Conveyer(IEnumerable<Orientation> enter, Orientation exit)
		{
			Entrances = new List<Orientation>(enter);
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
