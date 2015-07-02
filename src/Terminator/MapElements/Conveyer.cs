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
			if (entrance == Orientation.Top && Exit == Orientation.Bottom
				|| entrance == Orientation.Bottom && Exit == Orientation.Top
				|| entrance == Orientation.Left && Exit == Orientation.Right
				|| entrance == Orientation.Right && Exit == Orientation.Left)
				return Rotation.None;

			if (entrance == Orientation.Top && Exit == Orientation.Right
				|| entrance == Orientation.Right && Exit == Orientation.Bottom
				|| entrance == Orientation.Bottom && Exit == Orientation.Left
				|| entrance == Orientation.Left && Exit == Orientation.Top)
				return Rotation.Clockwise;

			if (entrance == Orientation.Top && Exit == Orientation.Left
				|| entrance == Orientation.Left && Exit == Orientation.Bottom
				|| entrance == Orientation.Bottom && Exit == Orientation.Right
				|| entrance == Orientation.Right && Exit == Orientation.Top)
				return Rotation.CounterClockwise;

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
			Coordinate target = robot.Position;

			if (Exit == Orientation.Bottom)
				target.Y -= 1;
			else if (Exit == Orientation.Top)
				target.Y += 1;
			else if (Exit == Orientation.Left)
				target.X -= 1;
			else
				target.X += 1;

			ITile targetTile = robot.Game.Board.GetTile(target);
			if (targetTile == null || targetTile is Pit)
			{
				// Robot conveyed off the board or into a pit
				robot.Position = new Coordinate {X = -1, Y = -1};
				return;
			}

			var conveyer = targetTile as Conveyer;
			if (conveyer != null)
			{
				Rotation rotation = conveyer.ConveyerRotation(Utilities.GetOppositeOrientation(Exit));
				if (rotation == Rotation.Clockwise)
					robot.RotateRight();
				else if (rotation == Rotation.CounterClockwise)
					robot.RotateLeft();
			}

			robot.Position = target;
		}
	}
}
