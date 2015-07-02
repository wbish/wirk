using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	/// <summary>
	/// Represents a wall that pushes a robot if a robot is present on the register in which
	/// the pusher is set to activate.
	/// </summary>
	public class WallPusherEdge : WallEdge
	{
		public WallPusherEdge(params int[] registers)
		{
			Registers = registers.ToList();
		}

		/// <summary>
		/// The registers the pushers will be active on
		/// </summary>
		public List<int> Registers { get; protected set; }

		public void Push(Robot robot, int register)
		{
			if (!Registers.Contains(register))
				return;

			Coordinate target = robot.Position;
			Floor floorTile = robot.Game.Board.GetTile(robot.Position) as Floor;

			if (floorTile == null)
				return;

			foreach (var edge in floorTile.Edges)
			{
				if (edge.Item2 == this)
				{
					if (edge.Item1 == Orientation.Top)
						target.Y += 1;
					else if (edge.Item1 == Orientation.Bottom)
						target.Y -= 1;
					else if (edge.Item1 == Orientation.Left)
						target.X += 1;
					else
						target.X -= 1;

					if (floorTile.GetEdge(Utilities.GetOppositeOrientation(edge.Item1)) != null)
					{
						// There is an opposite edge. I'm not exactly sur ewhat is supposed to happen. Most likely just not move
						return;
					}

					ITile targetTile = robot.Game.Board.GetTile(target);
					if (targetTile == null || targetTile is Pit)
					{
						// pushed to death;
						robot.Position = new Coordinate {X = -1, Y = -1};
						return;
					}

					var targetFloor = (Floor) targetTile;
					if (targetFloor.GetEdge(edge.Item1) != null)
					{
						// blocked from entering!
						return;
					}

					robot.Position = target;

					return;
				}
			}
		}
	}
}
