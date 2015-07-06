namespace WiRK.Terminator.MapElements
{
	public class WrenchHammer : Wrench
	{
		public override void Execute(Game game, TileExecution execution)
		{
			if (execution == TileExecution.Wrench)
			{
				// TODO: DEAL OPTION CARD
			}

			base.Execute(game, execution);
		}
	}
}
