using System;
using System.Collections.Generic;
using System.Linq;
using WiRK.Terminator.MapElements;

namespace WiRK.Terminator
{
	public class Game
	{
		private int _register;

		public Game()
		{
			Cards = new Deck();
			Robots = new List<Robot>();
			Board = new Map();
		}

		public Map Board { get; set; }

		public IEnumerable<Robot> Robots { get; set; }

		public Deck Cards { get; set; }

		public void Initialize()
		{
			foreach (var robot in Robots)
			{
				robot.Initialize(this);
			}
		}

		/// <summary>
		/// Called at the start of each turn. 
		/// </summary>
		public void StartTurn()
		{
			// Deal cards
			Cards.Deal(Robots);

			// RoboRally refers to the first register as register 1 and not register 0.
			_register = 1;
		}

		/// <summary>
		/// Executes the next register.
		/// - Robots move
		/// - Board elements move
		///		- Express conveyers move 1 space
		///		- Express and normal conveyers move 1 space
		///		- Pushers push if active
		///		- Gears rotate 90 degrees
		/// - Lasers fire
		/// </summary>
		/// <returns>Registers remaining to execute</returns>
		public int ExecuteNextRegister()
		{
			if (_register <= Constants.RobotRegisters)
			{
				ExecuteMoves();

				ExecuteExpressConveyers();

				ExecuteAllConveyers();

				ExecutePushers();

				ExecuteGears();

				++_register;
			}

			return Constants.RobotRegisters - _register - 1;
		}

		/// <summary>
		/// Called at the end of each turn.
		/// - Claims cards back to the deck
		/// - Wrenches heal, etc
		/// </summary>
		public void EndTurn()
		{
			// TODO: Do end of turn stuff, e.g. heal on wrench

			// Pick up unlocked cards
			Cards.Reclaim(Robots);
		}

		private void ExecuteMoves()
		{
			var robotsCardsByPriority = Robots
				.Select(robot => new Tuple<Robot, int>(robot, robot.CardAtRegister(_register)))
				.OrderByDescending(x => x.Item2)
				.ToList();

			// Highest priority card executes first
			foreach (var robotCard in robotsCardsByPriority)
			{
				Robot robot = robotCard.Item1;
				robot.ExecuteMove(_register);
			}
		}

		private void ExecuteExpressConveyers()
		{
			// Express conveyers convey 1 square
			foreach (var robot in Robots)
			{
				var conveyer = Board.SquareAtCoordinate(robot.Position) as ExpressConveyer;
				if (conveyer != null)
				{
					conveyer.Convey(robot);
				}
			}
		}

		private void ExecuteAllConveyers()
		{
			// Express and Normal conveyers convey 1 square
			foreach (var robot in Robots)
			{
				var conveyer = Board.SquareAtCoordinate(robot.Position) as Conveyer;
				if (conveyer != null)
				{
					conveyer.Convey(robot);
				}
			}
		}

		private void ExecutePushers()
		{
			// Pushers push if active for register
			foreach (var robot in Robots)
			{
				var floor = Board.SquareAtCoordinate(robot.Position) as Floor;
				if (floor != null)
				{
					var pusher = floor.GetPusher();
					if (pusher != null)
					{
						pusher.Push(robot, _register);
					}
				}
			}
		}

		private void ExecuteGears()
		{
			// Gears rotate 
			foreach (var robot in Robots)
			{
				var gear = Board.SquareAtCoordinate(robot.Position) as Gear;
				if (gear != null)
				{
					gear.Rotate(robot);
				}
			}
		}
	}
}
