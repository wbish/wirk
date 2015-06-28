using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WiRC.Terminator
{
    public class Map
    {
		public Map()
		{
		}

		public IEnumerable<IEnumerable<ISquare>> Squares { private get; set; }

		/// <summary>
		/// Get the ISquare object at a given coordinate
		/// </summary>
		/// <param name="location">X and Y coordinate</param>
		/// <returns>ISquare at coordinate. Null if out of bounds.</returns>
		public ISquare SquareAtCoordinate(Coordinate location)
		{
			if (Squares == null)
			{
				throw new Exception("Map not initialized");
			}

			if (location.X >= Squares.Count())
				return null;

			List<ISquare> row = Squares.ElementAt(location.X).ToList();

			if (location.Y >= row.Count())
				return null;

			ISquare square = row.ElementAt(location.Y);

			Trace.Assert(square != null, "Square should not be null");

			return square;
		}
    }
}
