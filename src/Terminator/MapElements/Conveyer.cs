using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	public class Conveyer : Floor
	{
		public List<Orientation> Entrances { get; internal set; }

		public Orientation Exit { get; internal set; }

		public Conveyer(Orientation enter, Orientation exit)
			: this(new[] { enter }, exit)
		{
		}

		public Conveyer(IEnumerable<Orientation> enter, Orientation exit)
		{
			Entrances = new List<Orientation>(enter);
			Exit = exit;

			if (Entrances.Any(entrance => entrance == Exit))
			{
				throw new InvalidOperationException("Cannot exit same way you enter!");
			}
		}

		public Conveyer()
		{

		}

		public override void Execute(Game game, TileExecution execution)
		{
			if (execution == TileExecution.Conveyer)
			{
				Convey(RobotOnTile(game));
			}

			base.Execute(game, execution);
		}

		protected void Convey(Robot robot)
		{
			Coordinate target = TargetCoordinate(robot.Position);

			ITile targetTile = robot.Game.Board.Tile(target);
			if (targetTile == null || targetTile is Pit)
			{
				// Robot conveyed off the board or into a pit
				robot.Position = Coordinate.OutOfBounds;
				return;
			}

			var conveyer = targetTile as Conveyer;
			if (conveyer != null)
			{
				Rotation rotation = conveyer.ConveyerRotation(Utilities.GetOppositeOrientation(Exit));
				if (rotation == Rotation.Clockwise)
					robot.Facing = Utilities.ClockwiseRotation(robot.Facing);
				else if (rotation == Rotation.CounterClockwise)
					robot.Facing = Utilities.CounterclockwiseRotation(robot.Facing);
			}

			robot.Position = target;
		}

		private Rotation ConveyerRotation(Orientation entrance)
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
				return Rotation.CounterClockwise;

			if (entrance == Orientation.Top && Exit == Orientation.Left
				|| entrance == Orientation.Left && Exit == Orientation.Bottom
				|| entrance == Orientation.Bottom && Exit == Orientation.Right
				|| entrance == Orientation.Right && Exit == Orientation.Top)
				return Rotation.Clockwise;

			throw new InvalidOperationException();
		}

		private Coordinate TargetCoordinate(Coordinate origin)
		{
			Coordinate target = origin;

			if (Exit == Orientation.Bottom)
				target.Y += 1;
			else if (Exit == Orientation.Top)
				target.Y -= 1;
			else if (Exit == Orientation.Left)
				target.X -= 1;
			else
				target.X += 1;

			return target;
		}
	}
}
