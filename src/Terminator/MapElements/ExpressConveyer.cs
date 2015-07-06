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

		public override void Execute(Game game, TileExecution execution)
		{
			if (execution == TileExecution.ExpressConveyer)
			{
				Convey(RobotOnTile(game));
			}

			base.Execute(game, execution);
		}
	}
}
