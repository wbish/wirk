using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRC.Terminator
{
	public class Game
	{
		private int _register;

		public Game()
		{
			Cards = new Deck();
		}

		public Map Board { get; protected set; }

		public IEnumerable<Robot> Robots { get; set; }

		public Deck Cards { get; set; } 

		public void StartTurn()
		{
			// Deal cards
			Cards.Deal(Robots);

			// RoboRally refers to the first register as register 1 and not register 0.
			_register = 1;
		}

		public void ExecuteNextRegister()
		{
			if (_register > Robot.Registers)
				return;

			var robotsCardsByPriority = Robots
				.Select(robot => new Tuple<Robot, int>(robot, robot.CardPriorityAtRegister(_register)))
				.OrderByDescending(x => x.Item2)
				.ToList();

			// Highest priority card executes first
			foreach (var robotCard in robotsCardsByPriority)
			{
				Robot robot = robotCard.Item1;
				robot.ExecuteRegister(this, _register);
			}

			++_register;
		}

		public void EndTurn()
		{
			// TODO: Do end of turn stuff, e.g. heal on wrench

			// Pick up unlocked cards
			Cards.Reclaim(Robots);
		}
	}
}
