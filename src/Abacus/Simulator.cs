using System;
using System.Collections.Generic;
using System.Linq;
using WiRK.Terminator;

namespace WiRK.Abacus
{
    public static class Simulator
    {
		public static List<List<CardExecutionResult>> RunSimulations(Robot robot)
		{
			var results = new List<List<CardExecutionResult>>(); 
			List<List<ProgramCardType>> permutations = CalculateMovePermutations(robot);
			Coordinate position = robot.Position;
			Orientation facing = robot.Facing;

			foreach (var permutation in permutations)
			{
				robot.Position = position;
				robot.Facing = facing;
				robot.PickUpCards();

				var deck = new Deck();
				var permutationResult = new List<CardExecutionResult>();
				for (int i = 0 ; i < permutation.Count; ++i)
				{
					int priority = deck.GetCard(permutation[i]);
					robot.DealCard(priority, i + 1);
				}

				robot.Game.StartTurn(false /* Deal Cards */);

				while (true)
				{
					int registersLeft = robot.Game.ExecuteNextRegister();

					permutationResult.Add(new CardExecutionResult
					{
						Card = permutation[permutationResult.Count],
						Position = robot.Position,
						Facing = robot.Facing
					});

					if (registersLeft == 0)
						break;
				}

				robot.Game.EndTurn();

				results.Add(permutationResult);
			}

			return results;
		}

		/// <summary>
		/// Calculate all possible move permutations given a robots set of cards
		/// </summary>
		/// <param name="robot">Robot</param>
		/// <returns>List of permutations</returns>
		internal static List<List<ProgramCardType>> CalculateMovePermutations(Robot robot)
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

			if (cardStack.Count == Constants.RobotRegisters || node.Children == null)
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

		private static void BuildCardPermutationTree(PermutationNode node, IEnumerable<ProgramCardType> cardsToPlace, int depth = Constants.RobotRegisters)
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
