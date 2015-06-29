using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WiRK.Terminator.UnitTests
{
	[TestClass]
	public class ProgramCardUnitTests
	{
		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void ProgramCard_GetCardByPriority_OutOfBoundsThrows()
		{
			// Arrange
			const int priority = 850;

			// Act
			ProgramCardType cardType = ProgramCard.GetCardByPriority(priority);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void ProgramCard_GetCardByPriority_InvalidPriorityThrows()
		{
			// Arrange
			const int priority = 615;

			// Act
			ProgramCardType cardType = ProgramCard.GetCardByPriority(priority);
		}

		[TestMethod]
		public void ProgramCard_GetCardByPriority_Move1()
		{
			// Arrange
			const int priority = 560;

			// Act
			ProgramCardType cardType = ProgramCard.GetCardByPriority(priority);

			// Assert
			Assert.AreEqual(ProgramCardType.Move1, cardType, "Move 1");
		}
	}
}
