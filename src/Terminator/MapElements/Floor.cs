using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WiRK.Terminator
{
	public class Floor : ITile
	{
		private List<Tuple<Orientation, IEdge>> _edges;

		public List<Tuple<Orientation, IEdge>> Edges 
		{ 
			get
			{
				if (_edges == null)
					_edges = new List<Tuple<Orientation, IEdge>>(4);
				return _edges;
			}
			set 
			{ 
				_edges = value; 
			}
		}

		[JsonIgnore]
		public IEdge Top 
		{
			get { return GetEdge(Orientation.Top); }
			set { PutEdge(Orientation.Top, value); }
		}

		[JsonIgnore]
		public IEdge Right
		{
			get { return GetEdge(Orientation.Right); }
			set { PutEdge(Orientation.Right, value); }
		}

		[JsonIgnore]
		public IEdge Bottom
		{
			get { return GetEdge(Orientation.Bottom); }
			set { PutEdge(Orientation.Bottom, value); }
		}

		[JsonIgnore]
		public IEdge Left
		{
			get { return GetEdge(Orientation.Left); }
			set { PutEdge(Orientation.Left, value); }
		}

		public void PutEdge(Orientation edgeLocation, IEdge edge)
		{
			var oldEdge = Edges.FirstOrDefault(x => x.Item1 == edgeLocation);
			if (oldEdge != null)
				Edges.Remove(oldEdge);

			Edges.Add(new Tuple<Orientation, IEdge>(edgeLocation, edge));
		}

		public IEdge GetEdge(Orientation edgeLocation)
		{
			Tuple<Orientation, IEdge> edge = Edges.FirstOrDefault(x => x.Item1 == edgeLocation);

			if (edge != null)
				return edge.Item2;

			return null;
		}

		public WallPusherEdge GetPusher()
		{
			var tuple = Edges.FirstOrDefault(x => x.Item2 is WallPusherEdge);

			if (tuple != null)
				return tuple.Item2 as WallPusherEdge;

			return null;
		}

		public IEnumerable<Tuple<Orientation,WallLaserEdge>> GetLasers()
		{
			return Edges.Where(x => x.Item2 is WallLaserEdge).Select(x => new Tuple<Orientation, WallLaserEdge>(x.Item1, (WallLaserEdge)x.Item2));
		}
	}
}
