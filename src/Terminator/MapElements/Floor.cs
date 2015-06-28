using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRC.Terminator
{
	public abstract class Floor : ISquare
	{
		List<Tuple<EdgeLocation, IEdge>> Edges { get; set; }

		public IEdge Top { get; set; }

		public IEdge Right { get; set; }

		public IEdge Bottom { get; set; }

		public IEdge Left { get; set; }

		private void PutEdge(EdgeLocation edgeLocation, IEdge edge)
		{

		}

		public IEdge GetEdge(EdgeLocation edgeLocation)
		{
			if (Edges == null)
				return null;

			Tuple<EdgeLocation, IEdge> edge = Edges.FirstOrDefault(x => x.Item1 == edgeLocation);

			if (edge != null)
				return edge.Item2;

			return null;
		}
	}
}
