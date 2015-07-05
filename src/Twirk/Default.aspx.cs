using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WiRK.Abacus;
using WiRK.Terminator;

namespace WiRK.TwirkIt
{
	public partial class Default : System.Web.UI.Page
	{
		/// <summary>
		/// Set the default user state
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			ViewState["PosX"] = "0";
			ViewState["PosY"] = "0";
			ViewState["Facing"] = "0";
			ViewState["Cards"] = string.Empty;
		}

		/// <summary>
		/// Run the simulations and save result to ViewState and JS script block
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRunSimulations_OnClick(object sender, EventArgs e)
		{
			var deck = new Deck();
			List<int> position = Request.Form["robotPosition"].Split(',').Select(int.Parse).ToList();
			var cards = Request.Form["cards"].Split(',').Select(x => GetCardPriority(deck, x));

			var robot = new Robot 
			{
				Position = new Coordinate {X = position[0], Y = position[1]},
				Facing = (Orientation)Enum.Parse(typeof(Orientation), Request.Form["robotOrientation"])
			};

			foreach (var priority in cards)
			{
				robot.DealCard(priority);
			}

			var game = new Game(new Map {Squares = Maps.GetMap(Maps.MapLayouts.ScottRallyMap)}, new List<Robot> {robot});
			game.Initialize();

			List<List<CardExecutionResult>> results = Simulator.Simulate(robot);
			List<List<CardExecutionResult>> productiveResults = results.Where(result => result.Last().Position.X != -1).ToList();

			ViewState["PosX"] = position[0];
			ViewState["PosY"] = position[1];
			ViewState["Facing"] = Request.Form["robotOrientation"];
			ViewState["Cards"] = Request.Form["cards"];

			ClientScript.RegisterClientScriptBlock(GetType(), "results", "results = " + JsonConvert.SerializeObject(productiveResults, Formatting.Indented), true);
		}

		/// <summary>
		/// Get card priority from user input
		/// </summary>
		/// <param name="deck">Deck to retrieve card from</param>
		/// <param name="card">Card from user input. A priority number or card move abbreviation: U, L, R, B, 1, 2, 3</param>
		/// <returns></returns>
		private int GetCardPriority(Deck deck, string card)
		{
			if (card.Equals("U", StringComparison.InvariantCultureIgnoreCase))
				return deck.GetCard(ProgramCardType.UTurn).Priority;
			if (card.Equals("L", StringComparison.InvariantCultureIgnoreCase))
				return deck.GetCard(ProgramCardType.RotateLeft).Priority;
			if (card.Equals("R", StringComparison.InvariantCultureIgnoreCase))
				return deck.GetCard(ProgramCardType.RotateRight).Priority;
			if (card.Equals("B", StringComparison.InvariantCultureIgnoreCase))
				return deck.GetCard(ProgramCardType.BackUp).Priority;
			if (card.Equals("1", StringComparison.InvariantCultureIgnoreCase))
				return deck.GetCard(ProgramCardType.Move1).Priority;
			if (card.Equals("2", StringComparison.InvariantCultureIgnoreCase))
				return deck.GetCard(ProgramCardType.Move2).Priority;
			if (card.Equals("3", StringComparison.InvariantCultureIgnoreCase))
				return deck.GetCard(ProgramCardType.Move3).Priority;

			return new ProgramCard(int.Parse(card)).Priority;
		}
	}
}