using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRC.Terminator
{
	public class Robot
	{
		public const int Registers = 5;

		public Robot()
		{
			Cards = new List<RobotCard>(Deck.CardsPerRobot);
		}

		public Coordinate Position { get; set; }

		public int Damage { get; set; }

		private List<RobotCard> Cards { get; set; }

		public void ExecuteRegister(Game game, int register)
		{
			if (register > Registers || register < 1)
				throw new ArgumentException("Invalid register");

			throw new NotImplementedException();
		}

		public int CardPriorityAtRegister(int register)
		{
			if (register > Registers || register < 1)
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
			if (Cards.Count < Registers)
				throw new InvalidProgramException("Insufficient cards");

			throw new NotImplementedException();
		}

		public IEnumerable<int> PickUpCards()
		{
			var cardsToPickUp = new List<int>();
			int keepCardsAtRegistersOrBelow = Registers - LockedRegisters();

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
			int resetCardsAtRegisterOrBelow = Registers - LockedRegisters();

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
			return Math.Max(0, Registers - Deck.CardsPerRobot - Damage);
		}
	}
}
