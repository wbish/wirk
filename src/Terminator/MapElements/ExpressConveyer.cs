namespace WiRK.Terminator.MapElements
{
	public class ExpressConveyer : Conveyer
	{
		public override int Moves { get { return 2; } }

		public ExpressConveyer(Orientation enter, Orientation exit)
			: base(enter, exit)
		{
		}

		public ExpressConveyer()
		{
		}
	}
}
