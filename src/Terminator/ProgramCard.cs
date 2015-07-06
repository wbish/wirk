using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	public class ProgramCard
	{
		public int Priority { get; private set; }

		public ProgramCardType CardType { get; private set; }

		public ProgramCard(int priority)
		{
			Priority = priority;
			CardType = GetCardTypeByPriority(priority);
		}

		internal static readonly IEnumerable<Tuple<ProgramCardType, IEnumerable<int>>> ProgramCardPriorities = new List
			<Tuple<ProgramCardType, IEnumerable<int>>>
		{
			new Tuple<ProgramCardType, IEnumerable<int>>(ProgramCardType.UTurn, 
				new List<int> {10, 20, 30, 40, 50, 60}),
			new Tuple<ProgramCardType, IEnumerable<int>>(ProgramCardType.RotateLeft,
				new List<int> {70, 90, 110, 130, 150, 170, 190, 210, 230, 250, 270, 290, 310, 330, 350, 370, 390, 410}),
			new Tuple<ProgramCardType, IEnumerable<int>>(ProgramCardType.RotateRight,
				new List<int> {80, 100, 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 320, 340, 360, 380, 400, 420}),
			new Tuple<ProgramCardType, IEnumerable<int>>(ProgramCardType.BackUp, 
				new List<int> {430, 440, 450, 460, 470, 480}),
			new Tuple<ProgramCardType, IEnumerable<int>>(ProgramCardType.Move1,
				new List<int> {490, 500, 510, 520, 530, 540, 550, 560, 570, 580, 590, 600, 610, 620, 630, 640, 650, 660}),
			new Tuple<ProgramCardType, IEnumerable<int>>(ProgramCardType.Move2,
				new List<int> {670, 680, 690, 700, 710, 720, 730, 740, 750, 760, 770, 780}),
			new Tuple<ProgramCardType, IEnumerable<int>>(ProgramCardType.Move3, 
				new List<int> {790, 800, 810, 820, 830, 840}),
		};

		public static ProgramCardType GetCardTypeByPriority(int priority)
		{
			if (priority < LowestPriorityCard || priority > HighestPriorityCard)
				throw new IndexOutOfRangeException("Priority must be between 10 and 840");

			foreach (var cardType in ProgramCardPriorities.Where(cardType => cardType.Item2.Contains(priority)))
			{
				return cardType.Item1;
			}

			throw new ArgumentException("Invalid priority. Priority must be between 10 and 840 and in increments of 10.", "priority");
		}

		internal const int LowestPriorityCard = 10;
		internal const int HighestPriorityCard = 840;
	}
}
