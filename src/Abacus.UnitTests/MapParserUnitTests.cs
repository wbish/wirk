using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace WiRK.Abacus.UnitTests
{
	[TestClass]
	public class MapParserUnitTests
	{
		[TestMethod]
		public void TestMapToJson()
		{
			var name = "Scott's Rally Map";
			string s = MapParser.MapToJson(name, Maps.GetMap(Maps.MapLayouts.ScottRallyMap));
			Console.WriteLine(s);

			var reader = new JsonTextReader(new StringReader(s));
			var map = new JsonSerializer().Deserialize<SerializedMap>(reader);

			Assert.AreEqual(name, map.Name);
			Assert.AreEqual(12, map.Width);
			Assert.AreEqual(24, map.Height);
			Assert.AreEqual(120, map.Conveyors.Count);
			Assert.AreEqual(14, map.Gears.Count);
			Assert.AreEqual(14, map.Wrenches.Count);
			Assert.AreEqual(8, map.Pits.Count);
			Assert.AreEqual(0, map.Flags.Count);
			Assert.AreEqual(64, map.Walls.Count);
			Assert.AreEqual(6, map.Lasers.Count);
			Assert.AreEqual(18, map.Pushers.Count);
		}

		[TestMethod]
		public void TestJsonSerializationRoundtrip()
		{
			var name = "Scott's Rally Map";
			string start = MapParser.MapToJson(name, Maps.GetMap(Maps.MapLayouts.ScottRallyMap));
			Console.WriteLine(start);

			var deserializedMap = MapParser.JsonToMap(start);
			string after = MapParser.MapToJson(name, deserializedMap);

			Assert.AreEqual(start, after);
		}
	}
}
