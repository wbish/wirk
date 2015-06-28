using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace WiRC.Terminator.UnitTests
{
	[TestClass]
	public class MapUnitTests
	{
		[TestMethod]
		public void OneSquareWrenchRallyMap_FirstSquareIsWrench_Deserialize()
		{
			// Act
			Map map = JsonConvert.DeserializeObject<Map>(OneSquareWrenchRallyMap, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

			// Assert
			Assert.IsNotNull(map);
			Assert.IsInstanceOfType(map.SquareAtCoordinate(new Coordinate {X = 0, Y = 0}), typeof (Wrench), "Square is Wrench");
		}

		[TestMethod]
		public void OneSquareWrenchRallyMap_FirstSquareIsWrench_Serialize()
		{
			// Arrange
			var squares = new List<List<ISquare>> {new List<ISquare> {new Wrench()}};
			var map = new Map {Squares = squares};

			// Act
			string json = JsonConvert.SerializeObject(map, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

			// Assert
			Assert.IsNotNull(json);
		}

		[TestMethod]
		public void SquareAtCoordinate_OutOfBounds_ReturnsNull()
		{
			// Arrange
			var squares = new List<List<ISquare>> { new List<ISquare> { new Wrench() } };
			var map = new Map { Squares = squares };

			// Act
			ISquare square = map.SquareAtCoordinate(new Coordinate {X = 1, Y = 0});

			// Assert
			Assert.IsNull(square);
		}

		private const string OneSquareWrenchRallyMap = @"{""$type"":""WiRC.Terminator.Map, WiRC.Terminator"",""Squares"":{""$type"":""System.Collections.Generic.List`1[[System.Collections.Generic.List`1[[WiRC.Terminator.ISquare, WiRC.Terminator]], mscorlib]], mscorlib"",""$values"":[{""$type"":""System.Collections.Generic.List`1[[WiRC.Terminator.ISquare, WiRC.Terminator]], mscorlib"",""$values"":[{""$type"":""WiRC.Terminator.Wrench, WiRC.Terminator"",""Top"":null,""Right"":null,""Bottom"":null,""Left"":null}]}]}}";
	}
}