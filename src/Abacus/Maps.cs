using System.Collections.Generic;
using System.Linq;
using WiRK.Terminator;
using WiRK.Terminator.MapElements;

namespace WiRK.Abacus
{
	public static class Maps
	{
		public enum MapLayouts
		{
			ScottRallyMap,
		}

		public static IEnumerable<IEnumerable<ITile>> GetMap(MapLayouts layout)
		{
			switch (layout)
			{
				case MapLayouts.ScottRallyMap:
					return BuildScottRallyMap();
				default:
					return null;
			}
		}

		private static IEnumerable<IEnumerable<ITile>> BuildScottRallyMap()
		{
			var map = new List<IEnumerable<ITile>>(ScottRallyMap);
			map.AddRange(Transpose(map));

			return map;
		}

		private static readonly List<IEnumerable<ITile>> ScottRallyMap = new List<IEnumerable<ITile>>
			{
				new ITile[] // Row1
				{ 
					new Wrench(), 
					new Floor(), 
					new Conveyer(Orientation.Right, Orientation.Bottom) { Top = new WallEdge() }, 
					new Conveyer(Orientation.Right, Orientation.Left), 
					new Conveyer(Orientation.Right, Orientation.Left) { Top = new WallEdge() }, 
					new Conveyer(Orientation.Top, Orientation.Left), 
					new Floor(), 
					new Floor { Top = new WallEdge() }, 
					new Floor(), 
					new Floor { Top = new WallEdge() }, 
					new Floor(), 
					new Wrench() 
				},
				new ITile[] // Row2
				{
					new ExpressConveyer(Orientation.Right, Orientation.Left), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Left), 
					new Conveyer(Orientation.Top, Orientation.Bottom), 
					new Floor(), 
					new Floor(), 
					new Floor(), 
					new Conveyer(Orientation.Right, Orientation.Bottom), 
					new Conveyer(Orientation.Right, Orientation.Left), 
					new Conveyer(Orientation.Right, Orientation.Left), 
					new Pit(), 
					new Floor(), 
					new Floor()
				},
				new ITile[] // Row3
				{
					new Floor { Left = new WallEdge() },
					new ExpressConveyer(Orientation.Bottom, Orientation.Top) { Left = new WallPusherEdge(1,3,5) }, 
					new Conveyer(Orientation.Top, Orientation.Bottom), 
					new Floor(),
					new Gear(Rotation.CounterClockwise), 
					new Conveyer(Orientation.Right, Orientation.Left) { Bottom = new WallEdge() }, 
					new Conveyer(Orientation.Top, Orientation.Left), 
					new Floor(), 
					new Gear(Rotation.Clockwise), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top) { Right = new WallPusherEdge(1,3,5) },
 					new Floor { Bottom = new WallEdge() }, 
					new Floor { Right = new WallEdge() }
				},
				new ITile[] // Row4
				{
					new Floor(), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Conveyer(Orientation.Top, Orientation.Bottom), 
					new Floor(), 
					new Conveyer(Orientation.Top, Orientation.Bottom), 
					new Floor(), 
					new Gear(Rotation.CounterClockwise), 
					new Floor(), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Right), 
					new ExpressConveyer(Orientation.Left, Orientation.Top),
 					new Floor(), 
					new Floor()
				},
				new ITile[] // Row5
				{
					new Floor { Left = new WallEdge()}, 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Conveyer(new[] { Orientation.Top, Orientation.Right}, Orientation.Bottom), 
					new Conveyer(Orientation.Right, Orientation.Left), 
					new Conveyer(Orientation.Top, Orientation.Left), 
					new Gear(Rotation.CounterClockwise), 
					new Gear(Rotation.Clockwise), 
					new Floor(), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor(), 
 					new Floor(), 
					new Floor { Right = new WallEdge() } 
				},
				new ITile[] // Row6
				{
					new Floor(), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top) { Left = new WallPusherEdge(1,3,5) }, 
					new Conveyer(Orientation.Top, Orientation.Bottom), 
					new Floor(), 
					new Floor(), 
					new Gear(Rotation.Clockwise), 
					new Floor(), 
					new Floor(), 
					new ExpressConveyer(new [] { Orientation.Bottom, Orientation.Right}, Orientation.Top), 
					new ExpressConveyer(Orientation.Right, Orientation.Left) { Top = new WallPusherEdge(2,4) }, 
 					new ExpressConveyer(new [] { Orientation.Bottom, Orientation.Right}, Orientation.Left), 
					new ExpressConveyer(Orientation.Right, Orientation.Left) 
				},
				new ITile[] // Row7
				{
					new ExpressConveyer(Orientation.Left, Orientation.Right), 
					new ExpressConveyer(new [] { Orientation.Bottom, Orientation.Left}, Orientation.Top), 
					new Conveyer(Orientation.Top, Orientation.Bottom) { Right = new WallPusherEdge(1,3,5) }, 
					new Wrench(), 
					new Pit(), 
					new WrenchHammer(), 
					new Pit(), 
					new WrenchHammer(), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top) { Left = new WallPusherEdge(2,4) }, 
					new Pit(),
 					new ExpressConveyer(Orientation.Bottom, Orientation.Top) { Right = new WallPusherEdge(2,4) }, 
					new Floor()
				},
				new ITile[] // Row8
				{
					new Floor { Left = new WallEdge()}, 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Conveyer(Orientation.Top, Orientation.Bottom), 
					new Floor { Right = new WallLaserEdge()}, 
					new Floor(), 
					new Floor(), 
					new Floor(), 
					new Floor { Left = new WallLaserEdge() }, 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor(), 
 					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor { Right = new WallEdge() }
				},
				new ITile[] // Row9
				{
					new Floor(), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Gear(Rotation.CounterClockwise), 
					new Conveyer(Orientation.Left, Orientation.Right) { Bottom = new WallEdge() }, 
					new Conveyer(Orientation.Left, Orientation.Right), 
					new Conveyer(Orientation.Left, Orientation.Bottom), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Right), 
					new ExpressConveyer(Orientation.Left, Orientation.Right) { Bottom = new WallPusherEdge(1,3,5)}, 
					new ExpressConveyer(Orientation.Left, Orientation.Top), 
					new Floor(), 
 					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor()
				},
				new ITile[] // Row10
				{
					new Floor { Left = new WallEdge()}, 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor { Left = new WallLaserEdge(2) }, 
					new Floor(), 
					new Floor(), 
					new Conveyer(Orientation.Top, Orientation.Bottom), 
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor(),  
					new Floor(), 
					new Floor { Right = new WallEdge() }, 
 					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor { Right = new WallEdge() }
				},
				new ITile[] // Row11
				{
					new ExpressConveyer(Orientation.Left, Orientation.Right), 
					new ExpressConveyer(Orientation.Left, Orientation.Top), 
					new Floor(), 
					new Floor(), 
					new Floor(), 
					new Conveyer(Orientation.Top, Orientation.Bottom) { Left = new WallPusherEdge(1,3,5) },
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor(),  
					new Floor(), 
					new Floor(), 
 					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor()
				},
				new ITile[] // Row12
				{
					new Wrench(), 
					new Floor(), 
					new Floor { Bottom = new WallEdge() }, 
					new Floor(), 
					new Floor { Bottom = new WallEdge() }, 
					new Conveyer(Orientation.Top, Orientation.Bottom),
					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Floor { Bottom = new WallEdge() },  
					new Floor(), 
					new Floor { Bottom = new WallEdge() }, 
 					new ExpressConveyer(Orientation.Bottom, Orientation.Top), 
					new Wrench()
				},
			};

		private static IEnumerable<IEnumerable<ITile>> Transpose(IEnumerable<IEnumerable<ITile>> map)
		{
			var transposition = new List<List<ITile>>();
			var mapList = map.ToList();

			for (int i = mapList.Count() - 1; i >= 0; --i)
			{
				var row = new List<ITile>();
				for (int j = mapList.ElementAt(i).Count() - 1; j >= 0; --j)
				{
					row.Add(TransposeTile(mapList.ElementAt(i).ElementAt(j)));
				}
				transposition.Add(row);
			}

			return transposition;
		}

		private static ITile TransposeTile(ITile tile)
		{
			if (tile is Pit)
				return new Pit();

			var originalFloorTile = (Floor) tile;
			Floor floorTile;

			if (tile is ExpressConveyer)
				floorTile = new ExpressConveyer();
			else if (tile is Conveyer)
				floorTile = new Conveyer();
			else if (tile is WrenchHammer)
				floorTile = new WrenchHammer();
			else if (tile is Wrench)
				floorTile = new Wrench();
			else if (tile is Gear)
				floorTile = new Gear((tile as Gear).Direction);
			else
				floorTile = new Floor();

			foreach (var edge in originalFloorTile.Edges)
			{
				IEdge newEdge;

				if (edge.Item2 is WallLaserEdge)
					newEdge = new WallLaserEdge((edge.Item2 as WallLaserEdge).Lasers);
				else if (edge.Item2 is WallPusherEdge)
					newEdge = new WallPusherEdge((edge.Item2 as WallPusherEdge).Registers);
				else 
					newEdge = new WallEdge();

				floorTile.PutEdge(Utilities.GetOppositeOrientation(edge.Item1), newEdge);
			}

			var originalConveyer = originalFloorTile as Conveyer;
			if (originalConveyer != null)
			{
				var entrances = originalConveyer.Entrances.Select(Utilities.GetOppositeOrientation).ToList();

				var newConveyer = (Conveyer) floorTile;
				newConveyer.Entrances = entrances;
				newConveyer.Exit = Utilities.GetOppositeOrientation(originalConveyer.Exit);
			}

			return floorTile;
		}
	}
}
