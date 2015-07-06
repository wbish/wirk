using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WiRK.Terminator
{
    public class Map
    {
		public IEnumerable<IEnumerable<ITile>> Squares { internal get; set; }

		/// <summary>
		/// Get the ITile object at a given coordinate
		/// </summary>
		/// <param name="location">X and Y coordinate</param>
		/// <returns>ITile at coordinate. Null if out of bounds.</returns>
	    public ITile Tile(Coordinate location)
	    {
		    return Tile<ITile>(location);
	    }

		/// <summary>
		/// Get the ITile object at a given coordinate
		/// </summary>
		/// <param name="location">X and Y coordinate</param>
		/// <returns>ITile at coordinate. Null if out of bounds.</returns>
		public T Tile<T>(Coordinate location) where T : class, ITile
		{
			if (Squares == null)
			{
				throw new Exception("Map not initialized");
			}

			if (location.Y >= Squares.Count() || location.Y < 0)
				return default(T);

			List<ITile> row = Squares.ElementAt(location.Y).ToList();

			if (location.X >= row.Count() || location.X < 0)
				return default(T);

			ITile tile = row.ElementAt(location.X);

			Trace.Assert(tile != null, "Square should not be null");

			return tile as T;
		}

		public Coordinate Position(ITile tile)
		{
			if (tile == null)
			{
				throw new ArgumentNullException("tile");
			}

			for (int y = 0; y < Squares.Count(); ++y)
			{
				for (int x = 0; x < Squares.ElementAt(y).Count(); ++x)
				{
					if (Squares.ElementAt(y).ElementAt(x) == tile)
					{
						return new Coordinate {X = x, Y = y};
					}
				}
			}

			return Coordinate.OutOfBounds;
		}
    }
}
