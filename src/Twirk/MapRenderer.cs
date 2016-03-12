using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using WiRK.Terminator;
using WiRK.Terminator.MapElements;

namespace WiRK.TwirkIt
{
	internal static class MapRenderer
	{
		internal static JObject MapToJson(List<List<ITile>> map)
		{
			var jsonMap = new JObject
			{
				["rows"] = map.Count(),
				["columns"] = map.First().Count(),
				["map"] = TileMapSprites(map),
				["doodads"] = DoodadMapSprites(map)
			};

			return jsonMap;
		}

		private static JArray DoodadMapSprites(List<List<ITile>> map)
		{
			int rowCount = map.Count();
			int columnCount = map.First().Count();
			var doodads = new JArray();

			for (int i = 0; i < rowCount; ++i)
			{
				for (int j = 0; j < columnCount; ++j)
				{
					ITile tile = map[i][j];
					var floor = tile as Floor;

					if (floor != null && floor.Edges.Any())
					{
						var d = new List<int>();

						foreach (var edge in floor.Edges)
						{
							if (edge.Item1 == Orientation.Top)
							{
								if (edge.Item2 is WallLaserEdge)
								{
									var lasers = edge.Item2 as WallLaserEdge;
									if (lasers.Lasers == 1)
										d.Add(120);
									else
										d.Add(123);
								}
								else if (edge.Item2 is WallPusherEdge)
								{
									var pusher = edge.Item2 as WallPusherEdge;
									if (pusher.Registers.Contains(1))
										d.Add(9);
									else
										d.Add(1);
								}
								else // Simple Wall
								{
									d.Add(31);
								}
							}
							else if (edge.Item1 == Orientation.Right)
							{
								if (edge.Item2 is WallLaserEdge)
								{
									var lasers = edge.Item2 as WallLaserEdge;
									if (lasers.Lasers == 1)
										d.Add(121);
									else
										d.Add(124);
								}
								else if (edge.Item2 is WallPusherEdge)
								{
									var pusher = edge.Item2 as WallPusherEdge;
									if (pusher.Registers.Contains(1))
										d.Add(10);
									else
										d.Add(2);
								}
								else // Simple Wall
								{
									d.Add(7);
								}
							}
							else if (edge.Item1 == Orientation.Bottom)
							{
								if (edge.Item2 is WallLaserEdge)
								{
									var lasers = edge.Item2 as WallLaserEdge;
									if (lasers.Lasers == 1)
										d.Add(112);
									else
										d.Add(115);
								}
								else if (edge.Item2 is WallPusherEdge)
								{
									var pusher = edge.Item2 as WallPusherEdge;
									if (pusher.Registers.Contains(1))
										d.Add(11);
									else
										d.Add(3);
								}
								else // Simple Wall
								{
									d.Add(15);
								}
							}
							else //Left
							{
								if (edge.Item2 is WallLaserEdge)
								{
									var lasers = edge.Item2 as WallLaserEdge;
									if (lasers.Lasers == 1)
										d.Add(113);
									else
										d.Add(116);
								}
								else if (edge.Item2 is WallPusherEdge)
								{
									var pusher = edge.Item2 as WallPusherEdge;
									if (pusher.Registers.Contains(1))
										d.Add(12);
									else
										d.Add(4);
								}
								else // Simple Wall
								{
									d.Add(23);
								}
							}
						}

						doodads.Add(new JArray(d));
						continue;
					}

					doodads.Add(new JArray(-1));
				}
			}

			return doodads;
		}

		private static JArray TileMapSprites(List<List<ITile>> map)
		{
			int rowCount = map.Count();
			int columnCount = map.First().Count();

			var tiles = new JArray();
			for (int i = 0; i < rowCount; ++i)
			{
				for (int j = 0; j < columnCount; ++j)
				{
					ITile tile = map[i][j];

					if (tile is ExpressConveyer)
					{
						var ec = tile as ExpressConveyer;
						if (ec.Entrances.Count == 1)
						{
							if (ec.Exit == Orientation.Top)
							{
								if (ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(20);
								else if (ec.Entrances.Any(x => x == Orientation.Left))
									tiles.Add(25);
								else //right
									tiles.Add(26);
							}
							else if (ec.Exit == Orientation.Right)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left))
									tiles.Add(21);
								else if (ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(24);
								else //bottom
									tiles.Add(18);
							}
							else if (ec.Exit == Orientation.Bottom)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left))
									tiles.Add(19);
								else if (ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(28);
								else //right
									tiles.Add(16);
							}
							else // Exit is left
							{
								if (ec.Entrances.Any(x => x == Orientation.Right))
									tiles.Add(29);
								else if (ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(27);
								else //Bottom
									tiles.Add(17);
							}
						}
						else if (ec.Entrances.Count == 2)
						{
							if (ec.Exit == Orientation.Top)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(80);
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(84);
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Right))
									tiles.Add(91);
							}
							else if (ec.Exit == Orientation.Right)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(85);
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(81);
								if (ec.Entrances.Any(x => x == Orientation.Bottom) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(88);
							}
							else if (ec.Exit == Orientation.Bottom)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(93);
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(82);
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Right))
									tiles.Add(89);
							}
							else // Exit is left
							{
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(83);
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(92);
								if (ec.Entrances.Any(x => x == Orientation.Bottom) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(90);
							}
						}
						else
						{
							throw new InvalidDataException();
						}
					}
					else if (tile is Conveyer)
					{
						var ec = tile as Conveyer;
						if (ec.Entrances.Count == 1)
						{
							if (ec.Exit == Orientation.Top)
							{
								if (ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(36);
								else if (ec.Entrances.Any(x => x == Orientation.Left))
									tiles.Add(41);
								else //right
									tiles.Add(42);
							}
							else if (ec.Exit == Orientation.Right)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left))
									tiles.Add(37);
								else if (ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(40);
								else //bottom
									tiles.Add(34);
							}
							else if (ec.Exit == Orientation.Bottom)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left))
									tiles.Add(35);
								else if (ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(44);
								else //right
									tiles.Add(32);
							}
							else // Exit is left
							{
								if (ec.Entrances.Any(x => x == Orientation.Right))
									tiles.Add(45);
								else if (ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(43);
								else //Bottom
									tiles.Add(33);
							}
						}
						else if (ec.Entrances.Count == 2)
						{
							if (ec.Exit == Orientation.Top)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(64);
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(68);
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Right))
									tiles.Add(75);
							}
							else if (ec.Exit == Orientation.Right)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(69);
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(65);
								if (ec.Entrances.Any(x => x == Orientation.Bottom) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(72);
							}
							else if (ec.Exit == Orientation.Bottom)
							{
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(77);
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(66);
								if (ec.Entrances.Any(x => x == Orientation.Left) && ec.Entrances.Any(x => x == Orientation.Right))
									tiles.Add(73);
							}
							else // Exit is left
							{
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Bottom))
									tiles.Add(67);
								if (ec.Entrances.Any(x => x == Orientation.Right) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(76);
								if (ec.Entrances.Any(x => x == Orientation.Bottom) && ec.Entrances.Any(x => x == Orientation.Top))
									tiles.Add(74);
							}
						}
						else
						{
							throw new InvalidDataException();
						}
					}
					else if (tile is Gear)
					{
						Gear gear = (Gear)tile;
						if (gear.Direction == Rotation.Clockwise)
							tiles.Add(39);
						else
							tiles.Add(38);
					}
					else if (tile is Pit)
					{
						tiles.Add(5);
					}
					else if (tile is WrenchHammer)
					{
						tiles.Add(6);
					}
					else if (tile is Wrench)
					{
						tiles.Add(14);
					}
					else if (tile is Floor)
					{
						tiles.Add(0);
					}
				}
			}
			return tiles;
		}
	}
}