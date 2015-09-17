using System.Collections.Generic;
using Newtonsoft.Json;
using WiRK.Terminator;

namespace WiRK.Abacus
{
	[JsonObject]
	class SerializedMap
	{
		[JsonProperty("name")]
		public string Name;

		[JsonProperty("tileset")]
		public string Tileset;

		[JsonProperty("width")]
		public int Width;

		[JsonProperty("height")]
		public int Height;

		[JsonProperty("conveyors")]
		public List<SerializedConveyor> Conveyors = new List<SerializedConveyor>();

		[JsonProperty("gears")]
		public List<SerializedGear> Gears = new List<SerializedGear>();

		[JsonProperty("wrenches")]
		public List<SerializedWrench> Wrenches = new List<SerializedWrench>();

		[JsonProperty("pits")]
		public List<SerializedPit> Pits = new List<SerializedPit>();

		[JsonProperty("flags")]
		public List<SerializedFlag> Flags = new List<SerializedFlag>();

		[JsonProperty("walls")]
		public List<SerializedWall> Walls = new List<SerializedWall>();

		[JsonProperty("lasers")]
		public List<SerializedLaser> Lasers = new List<SerializedLaser>();
	}

	[JsonObject]
	class SerializedMapItem
	{
		[JsonProperty("row")]
		public int Row;

		[JsonProperty("column")]
		public int Column;
	}

	[JsonObject]
	class SerializedConveyor : SerializedMapItem
	{
		[JsonProperty("in")]
		public List<string> In;

		[JsonProperty("out")]
		public string Out;

		[JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Type;
	}

	[JsonObject]
	class SerializedGear : SerializedMapItem
	{
		[JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Type;
	}

	[JsonObject]
	class SerializedWrench : SerializedMapItem
	{
		[JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Type;
	}

	[JsonObject]
	class SerializedPit : SerializedMapItem
	{
	}

	[JsonObject]
	class SerializedFlag : SerializedMapItem
	{
		[JsonProperty("order")]
		public int Order;
	}

	[JsonObject]
	class SerializedWall : SerializedMapItem
	{
		[JsonProperty("order")]
		public List<string> Edges;
	}

	[JsonObject]
	class SerializedLaser : SerializedMapItem
	{
		[JsonProperty("end-row")]
		public int EndRow;

		[JsonProperty("end-column")]
		public int EndColumn;

		[JsonProperty("damage")]
		public int Damage;

		[JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Type;

		[JsonIgnore]
		public Orientation Direction;
	}
}