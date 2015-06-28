using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WiRC.Abacus.UnitTests
{
	[TestClass]
	public class PermutationsUnitTests
	{
		[TestMethod]
		public void Permutations_Factorial_0_is_1()
		{
			// Act
			double result = Permutations.Factorial(0);

			// Assert
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void Permutations_Factorial_1_is_1()
		{
			// Act
			double result = Permutations.Factorial(0);

			// Assert
			Assert.AreEqual(1, result);
		}

		[TestMethod]
		public void Permutations_Factorial_2_is_2()
		{
			// Act
			double result = Permutations.Factorial(2);

			// Assert
			Assert.AreEqual(2, result);
		}

		[TestMethod]
		public void Permutations_Factorial_7_is_5040()
		{
			// Act
			double result = Permutations.Factorial(7);

			// Assert
			Assert.AreEqual(5040, result);
		}
	}
}
