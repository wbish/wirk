using System;

namespace WiRK.Terminator
{
	public class Gear : Floor
	{
		public Rotation Direction { get; set; }

		public Gear(Rotation direction)
		{
			Direction = direction;
		}

		public void Rotate(Robot robot)
		{
			if (Direction == Rotation.Clockwise)
				robot.RotateRight();
			else
				robot.RotateLeft();
		}
	}
}
