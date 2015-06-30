using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WiRK.Terminator.UnitTests
{
	[TestClass]
	public class RobotUnitTests
	{
		[TestMethod]
		public void Robot_ExecuteRegister_UTurn()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.UTurn);

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(0, robot.Position.Y, "Position.Y");
			Assert.AreEqual(1, robot.Position.X, "Position.X");
			Assert.AreEqual(Orientation.Left, robot.Facing, "Facing");
		}

		[TestMethod]
		public void Robot_ExecuteRegister_RotateLeft()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.RotateLeft);

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(0, robot.Position.Y, "Position.Y");
			Assert.AreEqual(1, robot.Position.X, "Position.X");
			Assert.AreEqual(Orientation.Top, robot.Facing, "Facing");
		}

		[TestMethod]
		public void Robot_ExecuteRegister_RotateRight()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.RotateRight);

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(0, robot.Position.Y, "Position.Y");
			Assert.AreEqual(1, robot.Position.X, "Position.X");
			Assert.AreEqual(Orientation.Bottom, robot.Facing, "Facing");
		}

		[TestMethod]
		public void Robot_ExecuteRegister_BackUp()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.BackUp);

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(0, robot.Position.Y, "Position.Y");
			Assert.AreEqual(0, robot.Position.X, "Position.X");
			Assert.AreEqual(Orientation.Right, robot.Facing, "Facing");
		}

		[TestMethod]
		public void Robot_ExecuteRegister_Move1()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.Move1);

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(0, robot.Position.Y, "Position.Y");
			Assert.AreEqual(2, robot.Position.X, "Position.X");
			Assert.AreEqual(Orientation.Right, robot.Facing, "Facing");
		}

		[TestMethod]
		public void Robot_ExecuteRegister_Move2()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.Move2);

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(0, robot.Position.Y, "Position.Y");
			Assert.AreEqual(3, robot.Position.X, "Position.X");
			Assert.AreEqual(Orientation.Right, robot.Facing, "Facing");
		}

		[TestMethod]
		public void Robot_ExecuteRegister_Move3()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.Move3);

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(0, robot.Position.Y, "Position.Y");
			Assert.AreEqual(4, robot.Position.X, "Position.X");
			Assert.AreEqual(Orientation.Right, robot.Facing, "Facing");
		}

		[TestMethod]
		public void Robot_ExecuteRegister_Move1OffBoard()
		{
			// Arrange
			Robot robot = RobotForMoveTest(ProgramCardType.Move1);
			robot.Facing = Orientation.Top;

			// Act
			robot.ExecuteMove(1 /* register */);

			// Assert
			Assert.AreEqual(-1, robot.Position.Y, "Position.Y");
			Assert.AreEqual(-1, robot.Position.X, "Position.X");
		}

		private Robot RobotForMoveTest(ProgramCardType cardType)
		{
			int card = GetCardOfType(cardType);
			var position = new Coordinate {X = 1, Y = 0};
			var robot = new Robot { Position = position, Facing = Orientation.Right };
			robot.DealCard(card);
			robot.PlaceCard(card, 1 /* register */);
			var game = new Game { Board = { Squares = Maps.GetMap(Maps.MapLayouts.ScottRallyMap) }, Robots = new List<Robot> { robot } };
			game.Initialize();

			return robot;
		}

		private int GetCardOfType(ProgramCardType cardType)
		{
			return ProgramCard.ProgramCardPriorities.First(x => x.Item1 == cardType).Item2.First();
		}
	}
}
