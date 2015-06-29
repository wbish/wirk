using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Abacus
{
	public static class Permutations
	{
		public static IEnumerable<Tuple<T, int>> GetDuplicateItemCounts<T>(IEnumerable<T> cardTypesToPlace) where T : IComparable 
		{
			return GetItemCounts(cardTypesToPlace).Where(x => x.Item2 > 1).ToList();
		}

		public static IEnumerable<Tuple<T, int>> GetItemCounts<T>(IEnumerable<T> cardTypesToPlace) where T : IComparable
		{
			var cards = new List<T>(cardTypesToPlace);

			while (cards.Any())
			{
				int count = cards.Count;
				T card = cards.First();
				cards.RemoveAll(x => x.Equals(card));
				yield return new Tuple<T, int>(card, count - cards.Count);
			}
		}

		public static double Factorial(int n)
		{
			if (n < 0)
				throw new ArgumentException("n must be more than 0");

			double result = 1;
			for (; n > 1; --n)
				result *= n;
			return result;
		}
	}
}
