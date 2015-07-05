using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WiRK.Abacus;
using WiRK.Terminator;
using WiRK.Terminator.UnitTests;

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
		public void Simulator_RunSimulations_7Cards2TypeRepeatedTwice()
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
			var game = new Game { Board = { Squares = Maps.GetMap(Maps.MapLayouts.ScottRallyMap) }, Robots = new List<Robot> { robot } };
			game.Initialize();
			
			// Act
			List<List<CardExecutionResult>> results = Simulator.Simulate(robot);

			// Assert
			Assert.AreEqual(690, results.Count);
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
			Assert.AreEqual(720, moves.Count);
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
			Assert.AreEqual(2520, moves.Count);
		}
	}
}
