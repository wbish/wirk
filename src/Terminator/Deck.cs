using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRC.Terminator
{
	public class Deck
	{
		private readonly static Random Dealer = new Random(1337);
		internal const int CardsPerRobot = 9;

		private List<int> Cards { get; set; }

		public Deck()
		{
			Cards = new List<int>();

			for (var i = ProgramCard.LowestPriorityCard; i <= ProgramCard.HighestPriorityCard; i += 10)
			{
				Cards.Add(i);
			}
		}

		public void Shuffle()
		{
			var available = new List<int>(Cards);
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
				Cards.AddRange(robot.PickUpCards());
			}
		}

		public void Deal(IEnumerable<Robot> robots)
		{
			foreach (var robot in robots)
			{
				robot.DealCard(Cards.First());
				Cards.RemoveAt(0);
			}
		}
	}
}
