using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WiRK.Abacus;
using WiRK.Terminator;

namespace WiRK.TwirkIt
{
	public partial class Default : Page
	{
		private const string ActiveMap = "~/Maps/HalfScottRallyMap.rrdl";

		protected void Page_Load(object sender, EventArgs e)
		{
			ViewState["PosX"] = "0";
			ViewState["PosY"] = "0";
			ViewState["Facing"] = "0";
			ViewState["Cards"] = string.Empty;
			ViewState["Map"] = MapRenderer.MapToJson(MapParser.JsonToMap(LoadMapJson(ActiveMap)).Select(x => x.ToList()).ToList()).ToString();
		}
		
		[WebMethod]
		public static string RunSimulations(string body)
		{
			JToken json = JToken.Parse(body);

			var deck = new Deck();
			IEnumerable<int> cards = json["cards"].ToString().Split(',').Select(x => GetCardPriority(deck, x));
			List<int> robotPosition = json["robotPosition"].ToString().Split(',').Select(int.Parse).ToList();
			string robotOrientation = json["robotOrientation"].ToString();

            var robot = new Robot
			{
				Position = new Coordinate { X = robotPosition[0], Y = robotPosition[1] },
				Facing = (Orientation)Enum.Parse(typeof(Orientation), robotOrientation)
			};

			foreach (var priority in cards)
			{
				robot.DealCard(priority);
			}

			var map = MapParser.JsonToMap(LoadMapJson(ActiveMap));

			var game = new Game(new Map { Squares = map }, new List<Robot> { robot });
			game.Initialize();

			List<List<CardExecutionResult>> results = Simulator.Simulate(robot);
			List<List<CardExecutionResult>> productiveResults = results.Where(result => result.Last().Position.X != -1).ToList();

			return JsonConvert.SerializeObject(productiveResults, Formatting.Indented);
		}

		private static string LoadMapJson(string mapFile)
		{
			var path = HostingEnvironment.MapPath(mapFile);
			using (var reader = new StreamReader(new FileStream(path, FileMode.Open)))
			{
				return reader.ReadToEnd();
			}
		}

		private static int GetCardPriority(Deck deck, string card)
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