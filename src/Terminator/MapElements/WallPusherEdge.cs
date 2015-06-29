using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	/// <summary>
	/// Represents a wall that pushes a robot if a robot is present on the register in which
	/// the pusher is set to activate.
	/// </summary>
	public class WallPusherEdge : WallEdge
	{
		public WallPusherEdge(params int[] registers)
		{
			Registers = registers.ToList();
		}

		/// <summary>
		/// The registers the pushers will be active on
		/// </summary>
		public List<int> Registers { get; protected set; }

		public void Push(Robot robot, int register)
		{
			throw new NotImplementedException();
		}
	}
}
