using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	public class Robot
	{
		private Game _game;

		public Coordinate Position { get; set; }

		public Orientation Facing { get; set; }

		public int Damage { get; set; }

		public bool IsPoweredDown { get; set; }

		private List<RobotCard> Cards { get; set; }

		public Robot()
		{
			Cards = new List<RobotCard>(Deck.CardsPerRobot);
		}

		public void Initialize(Game game)
		{
			_game = game;
		}

		public void ExecuteMove(Game game, int register)
		{
			if (register > Constants.RobotRegisters || register < 1)
				throw new ArgumentException("Invalid register");

			throw new NotImplementedException();
		}

		public int CardPriorityAtRegister(int register)
		{
			if (register > Constants.RobotRegisters || register < 1)
				throw new ArgumentException("Invalid register");

			return Cards.First(x => x.Register == register).Card;
		}

		public void DealCard(int priority)
		{
			Cards.Add(new RobotCard { Card = priority, Register = 0});
		}

		public IEnumerable<int> CardsToPlace()
		{
			return from card in Cards where card.Register == 0 select card.Card;
		}

		public void PlaceCard(int card, int register)
		{
			if (Cards.Count < Constants.RobotRegisters)
				throw new InvalidProgramException("Insufficient cards");

			throw new NotImplementedException();
		}

		public IEnumerable<int> PickUpCards()
		{
			var cardsToPickUp = new List<int>();
			int keepCardsAtRegistersOrBelow = Constants.RobotRegisters - LockedRegisters();

			Cards = Cards.OrderBy(x => x.Register).ToList();
			for (int i = 0; i < Cards.Count; ++i)
			{
				if (Cards.First().Register <= keepCardsAtRegistersOrBelow)
				{
					cardsToPickUp.Add(Cards.First().Card);
					Cards.RemoveAt(0);
					continue;
				}

				break;
			}

			return cardsToPickUp;
		}

		public void ResetCards()
		{
			int resetCardsAtRegisterOrBelow = Constants.RobotRegisters - LockedRegisters();

			Cards = Cards.OrderBy(x => x.Register).ToList();
			foreach (var card in Cards)
			{
				if (card.Register <= resetCardsAtRegisterOrBelow)
				{
					card.Register = 0;
				}
			}
		}

		private int LockedRegisters()
		{
			return Math.Max(0, Constants.RobotRegisters - Deck.CardsPerRobot - Damage);
		}
	}
}
