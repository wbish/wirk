using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WiRK.Abacus;
using WiRK.Terminator;

namespace Abacus.UnitTests
{
	[TestClass]
	public class SimulatorUnitTests
	{
		[TestMethod]
		public void Simulator_CalculateMovePermutations_7Cards_2TypeRepeatedTwice()
		{
			// Arrange
			var robot = new Robot();
			robot.DealCard(10);		// UTurn
			robot.DealCard(20);		// UTurn
			robot.DealCard(70);		// RotateLeft
			robot.DealCard(80);		// RotateRight
			robot.DealCard(430);	// BackUp
			robot.DealCard(490);	// Move1
			robot.DealCard(500);	// Move1

			// Act
			var moves = Simulator.CalculateMovePermutations(robot);

			// Assert
			Assert.AreEqual(690, moves.Count);
		}

		[TestMethod]
		public void Simulator_CalculateMovePermutations_6UniqueCards()
		{
			// Arrange
			var robot = new Robot();
			robot.DealCard(10);		// UTurn
			robot.DealCard(70);		// RotateLeft
			robot.DealCard(80);		// RotateRight
			robot.DealCard(430);	// BackUp
			robot.DealCard(490);	// Move1
			robot.DealCard(790);	// Move3

			// Act
			var moves = Simulator.CalculateMovePermutations(robot);

			// Assert
			Assert.AreEqual(Permutations.Factorial(6), moves.Count);
		}

		[TestMethod]
		public void Simulator_CalculateMovePermutations_7UniqueCards()
		{
			// Arrange
			var robot = new Robot();
			robot.DealCard(10);		// UTurn
			robot.DealCard(70);		// RotateLeft
			robot.DealCard(80);		// RotateRight
			robot.DealCard(430);	// BackUp
			robot.DealCard(490);	// Move1
			robot.DealCard(670);	// Move2
			robot.DealCard(790);	// Move3

			// Act
			var moves = Simulator.CalculateMovePermutations(robot);

			// Assert
			Assert.AreEqual(Permutations.Factorial(7) / Permutations.Factorial(2), moves.Count);
		}

		[TestMethod]
		public void Simulator_PermutationCounts_7Cards_2TypeRepeatedTwice()
		{
			// Arrange
			var robot = new Robot();
			robot.DealCard(10);		// UTurn
			robot.DealCard(20);		// UTurn
			robot.DealCard(70);		// RotateLeft
			robot.DealCard(80);		// RotateRight
			robot.DealCard(430);	// BackUp
			robot.DealCard(490);	// Move1
			robot.DealCard(500);	// Move1

			// Act
			int permutations = PermutationCounts(robot);

			// Assert
			Assert.AreEqual(630, permutations);
		}

		private static int PermutationCounts(Robot robot)
		{
			// BUG: This math does not match our function for calculating permutations. FWIW, I trust the code more than this math.

			// Calculate the number of unique permutations possible with the cards to place.
			// For example, if we need to place the following cards
			// (2) Rotate Left, (1) Rotate Right, (1) Move 1, (1) Move 2 and (2) Move 3
			// Given there are 7 cards to place and the number of cards we need is 5
			// we are calculating P(n,r) where n = 7 and r = 5.
			// Also, given Rotate Left is repeated twice we have x1 = 2 and x2 = 2 for the
			// Move 3 repetitions.
			// Permutations = P(n,r) / (x1!x2!)
			// Permutations = P(7,5) / (2!2!)
			// Permutations = (7! / (7 - 5)!) / (2!2!)
			// Permutations = (5040 / 2) / (4)
			// Permutations = 2520 / 4
			// Permutations = 630

			List<int> cardsToPlace = robot.CardsToPlace().ToList();
			List<ProgramCardType> cardTypesToPlace = cardsToPlace.Select(ProgramCard.GetCardByPriority).ToList();
			int n = cardTypesToPlace.Count();
			int r = Math.Min(n, Constants.RobotRegisters);
			List<Tuple<ProgramCardType, int>> cardDuplicateCounts = Permutations.GetDuplicateItemCounts(cardTypesToPlace).ToList();

			double dividend = (Permutations.Factorial(n) / Permutations.Factorial(n - r));
			double divisor = cardDuplicateCounts.Aggregate<Tuple<ProgramCardType, int>, double>(1, (current, dupe) => current * Permutations.Factorial(dupe.Item2));

			return (int)(dividend / divisor);
		}
	}
}
