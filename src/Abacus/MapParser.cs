using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WiRK.Terminator;
using WiRK.Terminator.MapElements;

namespace WiRK.Abacus
{
	static class MapParser
	{
		#region Save map to file
		public static string MapToJson(string name, IEnumerable<IEnumerable<ITile>> map)
		{
			var serializedMap = new SerializedMap();
			serializedMap.Name = name;
			serializedMap.Tileset = "v2";
			serializedMap.Width = map.First().Count();
			serializedMap.Height = map.Count();

			int row = 0;
			foreach (var rowTiles in map)
			{
				int column = 0;
				foreach (var tile in rowTiles)
				{
					AddTileToMap(tile, row, column, serializedMap);
					column++;
				}
				row++;
			}

			// Post-process
			// Lasers don't have a fully defined ending yet
			foreach (var laser in serializedMap.Lasers)
			{
				if (laser.Direction == Orientation.Left)
				{
					var wall = serializedMap.Walls.Where(w => w.Row == laser.Row && w.Column < laser.Column && (w.Edges.Contains("left") || w.Edges.Contains("right"))).OrderByDescending(w => w.Column).FirstOrDefault();
					laser.EndColumn = (wall.Edges.Contains("right") ? wall.Column + 1 : wall.Column);
				}
				else if (laser.Direction == Orientation.Right)
				{
					var wall = serializedMap.Walls.Where(w => w.Row == laser.Row && w.Column > laser.Column && (w.Edges.Contains("left") || w.Edges.Contains("right"))).OrderBy(w => w.Column).FirstOrDefault();
					laser.EndColumn = (wall.Edges.Contains("left") ? wall.Column - 1 : wall.Column);
				}
				else if (laser.Direction == Orientation.Top)
				{
					var wall = serializedMap.Walls.Where(w => w.Column == laser.Column && w.Row < laser.Row && (w.Edges.Contains("top") || w.Edges.Contains("bottom"))).OrderByDescending(w => w.Row).FirstOrDefault();
					laser.EndRow = (wall.Edges.Contains("bottom") ? wall.Row + 1 : wall.Row);
				}
				else if (laser.Direction == Orientation.Bottom)
				{
					var wall = serializedMap.Walls.Where(w => w.Column == laser.Column && w.Row > laser.Row && (w.Edges.Contains("top") || w.Edges.Contains("bottom"))).OrderBy(w => w.Row).FirstOrDefault();
					laser.EndRow = (wall.Edges.Contains("top") ? wall.Row - 1 : wall.Row);
				}
			}

			using (var stringWriter = new StringWriter())
			{
				var writer = new JsonTextWriter(stringWriter);
				JsonSerializer.Create().Serialize(writer, serializedMap);
				return stringWriter.ToString();
			}
		}

		private static void AddTileToMap(ITile tile, int row, int column, SerializedMap serializedMap)
		{
			if (tile.GetType() == typeof (Conveyer) || tile.GetType() == typeof (ExpressConveyer))
			{
				var conveyor = new SerializedConveyor()
				{
					Row = row,
					Column = column,
					In = (tile as Conveyer).Entrances.Select(o => o.ToString().ToLowerInvariant()).ToList(),
					Out = (tile as Conveyer).Exit.ToString().ToLowerInvariant(),
					Type = (tile.GetType() == typeof (ExpressConveyer) ? "express" : null)
				};
				serializedMap.Conveyors.Add(conveyor);
			}
			else if (tile.GetType() == typeof (Gear))
			{
				var gear = new SerializedGear()
				{
					Row = row,
					Column = column,
					Type = (tile as Gear).Direction.ToString().ToLowerInvariant()
				};
				serializedMap.Gears.Add(gear);
			}
			else if (tile.GetType() == typeof (Wrench) || tile.GetType() == typeof (WrenchHammer))
			{
				var wrench = new SerializedWrench()
				{
					Row = row,
					Column = column,
					Type = (tile.GetType() == typeof (WrenchHammer) ? "option" : null)
				};
				serializedMap.Wrenches.Add(wrench);
			}
			else if (tile.GetType() == typeof (Pit))
			{
				var pit = new SerializedPit()
				{
					Row = row,
					Column = column
				};
				serializedMap.Pits.Add(pit);
			}
			//else if (tile.GetType() == typeof(Flag))
			//{
			//	var flag = new SerializedFlag()
			//	{
			//		Row = row,
			//		Column = column,
			//		Order = (tile as Flag).Order
			//	};
			//	serializedMap.Flags.Add(flag);
			//}
			else if (tile is Floor)
			{
				if ((tile as Floor).Edges.Any())
				{
					var wall = new SerializedWall()
					{
						Row = row,
						Column = column,
						Edges = (tile as Floor).Edges.Select(p => p.Item1.ToString().ToLowerInvariant()).ToList()
					};
					serializedMap.Walls.Add(wall);
				}

				var laserWallEdge = (tile as Floor).Edges.FirstOrDefault(p => p.Item2 is WallLaserEdge);
				if (laserWallEdge != null)
				{
					// Little bit of a hack around converting directions from "wall edge orientation" to "laser direction"
					var laserDirection = (Orientation) (((int) laserWallEdge.Item1 + 2)%4);
					var laser = new SerializedLaser()
					{
						Row = row,
						Column = column,
						Damage = (laserWallEdge.Item2 as WallLaserEdge).Lasers,
						Direction = laserDirection
					};
					if (laserWallEdge.Item1 == Orientation.Left || laserWallEdge.Item1 == Orientation.Right)
						laser.EndRow = laser.Row;
					else
						laser.EndColumn = laser.Column;
					serializedMap.Lasers.Add(laser);
				}
			}
		}
		#endregion

		#region Load map from file
		public static IEnumerable<IEnumerable<ITile>> JsonToMap(string json)
		{
			var reader = new JsonTextReader(new StringReader(json));
			var deserializedMap = new JsonSerializer().Deserialize<SerializedMap>(reader);

			var map = new ITile[deserializedMap.Height,deserializedMap.Width];
			for (int j = 0; j < deserializedMap.Height; j++)
			{
				for (int i = 0; i < deserializedMap.Width; i++)
						map[j,i] = new Floor();
			}

			foreach (var conveyor in deserializedMap.Conveyors)
			{
				if (conveyor.Type == "express")
					map[conveyor.Row, conveyor.Column] = new ExpressConveyer(conveyor.In.Select(o => ParseOrientation(o)), ParseOrientation(conveyor.Out));
				else
					map[conveyor.Row, conveyor.Column] = new Conveyer(conveyor.In.Select(o => ParseOrientation(o)), ParseOrientation(conveyor.Out));
			}

			foreach (var gear in deserializedMap.Gears)
			{
				map[gear.Row, gear.Column] = new Gear(ParseRotation(gear.Type));
			}
			
			foreach (var wrench in deserializedMap.Wrenches)
			{
				if (wrench.Type == "option")
					map[wrench.Row, wrench.Column] = new WrenchHammer();
				else
					map[wrench.Row, wrench.Column] = new Wrench();
			}

			foreach (var pit in deserializedMap.Pits)
			{
				map[pit.Row, pit.Column] = new Pit();
			}

			// Flags

			foreach (var wall in deserializedMap.Walls)
			{
				(map[wall.Row, wall.Column] as Floor).Edges.AddRange(wall.Edges.Select(o => new Tuple<Orientation, IEdge>(ParseOrientation(o), new WallEdge())));
			}

			foreach (var laser in deserializedMap.Lasers)
			{
				Orientation laserWall = LaserWallEdgeOrientation(laser);
				(map[laser.Row, laser.Column] as Floor).Edges.RemoveAll(tup => tup.Item1 == laserWall);
				(map[laser.Row, laser.Column] as Floor).Edges.Add(new Tuple<Orientation, IEdge>(LaserWallEdgeOrientation(laser), new WallLaserEdge(laser.Damage)));
			}

			var jaggedArray = new List<List<ITile>>(deserializedMap.Height);
			for (int j = 0; j < deserializedMap.Height; j++)
			{
				jaggedArray.Add(new List<ITile>(deserializedMap.Width));
				for (int i = 0; i < deserializedMap.Width; i++)
				{
					jaggedArray[j].Add(map[j, i]);
				}
			}
			return jaggedArray;
		}

		private static Orientation ParseOrientation(string o)
		{
			Orientation orientation;
			if (Orientation.TryParse(o, true /*ignoreCase*/, out orientation))
				return orientation;
			throw new Exception("Invalid orientation");
		}

		// Because this is the edge the wall is on (rather than the edge the laser is pointing) it will be 
		// backwards from what you might expect
		private static Orientation LaserWallEdgeOrientation(SerializedLaser laser)
		{
			if (laser.Row == laser.EndRow)
				return laser.Column > laser.EndColumn ? Orientation.Right : Orientation.Left;
			else
				return laser.Row > laser.EndRow ? Orientation.Bottom : Orientation.Top;
		}

		private static Rotation ParseRotation(string r)
		{
			Rotation rotation;
			if (Rotation.TryParse(r, true /*ignoreCase*/, out rotation))
				return rotation;
			throw new Exception("Invalid rotation");
		}
		#endregion
	}
}
