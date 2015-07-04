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
		public ITile GetTile(Coordinate location)
		{
			if (Squares == null)
			{
				throw new Exception("Map not initialized");
			}

			if (location.Y >= Squares.Count() || location.Y < 0)
				return null;

			List<ITile> row = Squares.ElementAt(location.Y).ToList();

			if (location.X >= row.Count() || location.X < 0)
				return null;

			ITile tile = row.ElementAt(location.X);

			Trace.Assert(tile != null, "Square should not be null");

			return tile;
		}
    }
}
