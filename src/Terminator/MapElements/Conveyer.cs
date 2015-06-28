using System;

namespace WiRC.Terminator
{
	public class Conveyer : Floor
	{
		public EdgeLocation Origin { get; protected set; }

		public EdgeLocation Exit { get; protected set; }

		public int Moves { get; protected set; }

		public Rotation ConveyerRotation()
		{
			throw new NotImplementedException();
		}
	}
}
