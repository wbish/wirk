namespace WiRK.Terminator
{
	/// <summary>
	/// Represents a wall that fires lasers. The lasers always point in the direction towards the center
	/// of the containing square. For example, if this edge is attached to the top of a square, then 
	/// the lasers will fire towards the bottom and stop when it hits the next wall.
	/// </summary>
	public class WallLaserEdge : WallEdge
	{
		public WallLaserEdge()
			: this(1)
		{
		}

		public WallLaserEdge(int lasers)
		{
			Lasers = lasers;
		}

		public int Lasers { get; set; }
	}
}
