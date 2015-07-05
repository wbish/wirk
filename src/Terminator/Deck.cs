using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	public class Deck
	{
		private readonly static Random Dealer = new Random(1337);
		internal const int CardsPerRobot = 9;

		private List<ProgramCard> Cards { get; set; }

		public Deck()
		{
			Cards = new List<ProgramCard>();

			for (var priority = ProgramCard.LowestPriorityCard; priority <= ProgramCard.HighestPriorityCard; priority += 10)
			{
				Cards.Add(new ProgramCard(priority));
			}

			Shuffle();
		}

		public void Shuffle()
		{
			var available = new List<ProgramCard>(Cards);
			Cards.Clear();

			while (available.Count > 0)
			{
				int index = Dealer.Next(0, available.Count);
				Cards.Add(available.ElementAt(index));
				available.RemoveAt(index);
			}
		}

		public void Reclaim(IEnumerable<Robot> robots)
		{
			foreach (var robot in robots)
			{
				Cards.AddRange(robot.PickUpCards().Select(x => new ProgramCard(x)));
			}
		}

		public void Deal(IEnumerable<Robot> robots)
		{
			foreach (var robot in robots)
			{
				robot.DealCard(Cards.First().Priority);
				Cards.RemoveAt(0);
			}
		}

		public ProgramCard GetCard(ProgramCardType cardType)
		{
			foreach (var card in Cards.Where(card => card.CardType == cardType))
			{
				Cards.Remove(card);
				return card;
			}

			return null;
		}
	}
}
