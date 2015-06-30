using System.Collections.Generic;

namespace WiRK.Terminator.MapElements
{
	public class ExpressConveyer : Conveyer
	{
		public ExpressConveyer(Orientation enter, Orientation exit)
			: base(enter, exit)
		{
		}

		public ExpressConveyer(IEnumerable<Orientation> enter, Orientation exit)
			: base(enter, exit)
		{
		}

		public ExpressConveyer()
		{
		}
	}
}
