using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using WiRC.Terminator;

namespace WiRC.Abacus
{
    public class Simulator
    {
	    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
	    {
		    TypeNameHandling = TypeNameHandling.All
	    };

	    private Game _game;

		public void LoadGame(string json)
		{
			_game = JsonConvert.DeserializeObject<Game>(json, SerializerSettings);
		}

	    public string SaveGame()
	    {
		    if (_game == null)
			    throw new InvalidProgramException("Game not initialized");

			return JsonConvert.SerializeObject(_game, SerializerSettings);
	    }

		public void SetRobotPosition(Robot robot, Coordinate position)
		{
			robot.Position = position;
		}

		public void SetRobotCards(Robot robot, IEnumerable<int> cardPriorities)
		{
			foreach (var card in cardPriorities)
			{
				robot.DealCard(card);
			}
		}

		/// <summary>
		/// Calculate all possible move permutations given a robots set of cards
		/// </summary>
		/// <param name="robot">Robot</param>
		/// <returns>List of permutations</returns>
		public static List<List<ProgramCardType>> CalculateMovePermutations(Robot robot)
		{
			var permutations = new List<List<ProgramCardType>>();

			// This is the list of cards that can be placed. Locked registers are not included in this list
			// and should be appended to the end of each list if necessary
			IEnumerable<ProgramCardType> cards = robot.CardsToPlace().Select(ProgramCard.GetCardByPriority);
			var root = new PermutationNode();

			BuildCardPermutationTree(root, cards);

			BuildCardPermutationList(permutations, root);
			
			return permutations;
		}

		/// <summary>
		/// Given a permutation tree, we will convert each branch path into a list of cards represnting a unique permutation.
		/// </summary>
		/// <param name="permutations">List of permutations</param>
		/// <param name="node">Tree root</param>
		/// <param name="cardStack">Card stack for keeping track of branches</param>
		private static void BuildCardPermutationList(List<List<ProgramCardType>> permutations, PermutationNode node, Stack<ProgramCardType> cardStack = null)
		{
			if (node == null)
				throw new ArgumentNullException("node");
			if (permutations == null)
				throw new ArgumentNullException("permutations");

			cardStack = cardStack ?? new Stack<ProgramCardType>(5);

			if (cardStack.Count == Robot.Registers || node.Children == null)
			{
				var p = new List<ProgramCardType>(cardStack);
				p.Reverse();
				permutations.Add(p);
				return;
			}

			foreach (var child in node.Children)
			{
				if (child.Card == null)
				{
					throw new InvalidOperationException();
				}

				cardStack.Push(child.Card.Value);
				BuildCardPermutationList(permutations, child, cardStack);
				cardStack.Pop();
			}
		}

		private static void BuildCardPermutationTree(PermutationNode node, IEnumerable<ProgramCardType> cardsToPlace, int depth = Robot.Registers)
		{
			if (depth == 0)
				return;

			var children = new List<PermutationNode>();
			var cards = new List<ProgramCardType>(cardsToPlace);
			var distinctCards = new List<ProgramCardType>(cards.Distinct());

			foreach (var distinctCard in distinctCards)
			{
				var child = new PermutationNode {Card = distinctCard};
				var remainingCards = new List<ProgramCardType>(cards);
				remainingCards.Remove(distinctCard);
				BuildCardPermutationTree(child, remainingCards, depth - 1);

				children.Add(child);
			}

			node.Children = children;
		}
    }
}
