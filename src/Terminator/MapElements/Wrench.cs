namespace WiRK.Terminator
{
	public class Wrench : Floor
	{
		public override void Execute(Game game, TileExecution execution)
		{
			if (execution == TileExecution.Wrench)
			{
				// TODO: HEAL
			}

			base.Execute(game, execution);
		}
	}
}
