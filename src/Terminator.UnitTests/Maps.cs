using System.Collections.Generic;

namespace WiRK.Terminator.UnitTests
{
	public static class Maps
	{
		// TODO: Finish
		public static readonly List<IEnumerable<ISquare>> ScottRallyMap = new List<IEnumerable<ISquare>>
			{
				new []
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
			};
	}
}
