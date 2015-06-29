using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	public class Robot
	{
		private Game _game;

		public Coordinate Position { get; set; }

		public Orientation Facing { get; set; }

		public int Damage { get; set; }

		public bool IsPoweredDown { get; set; }

		private List<RobotCard> Cards { get; set; }

		public Robot()
		{
			Cards = new List<RobotCard>(Deck.CardsPerRobot);
		}

		public void Initialize(Game game)
		{
			_game = game;
		}

		public void ExecuteMove(int register)
		{
			if (register > Constants.RobotRegisters || register < 1)
				throw new ArgumentException("Invalid register");

			int priority = CardPriorityAtRegister(register);
			ProgramCardType card = ProgramCard.GetCardByPriority(priority);

			switch(card)
			{
				case ProgramCardType.UTurn:
					UTurn();
					break;
				case ProgramCardType.RotateLeft:
					RotateLeft();
					break;
				case ProgramCardType.RotateRight:
					RotateRight();
					break;
				case ProgramCardType.BackUp:
					BackUp();
					break;
				case ProgramCardType.Move1:
					Move1();
					break;
				case ProgramCardType.Move2:
					Move2();
					break;
				case ProgramCardType.Move3:
					Move3();
					break;
				default:
					throw new InvalidOperationException();
			}
		}

		private void UTurn()
		{
			RotateLeft();
			RotateLeft();
		}

		private void RotateLeft()
		{
			if (Facing == Orientation.Bottom)
				Facing = Orientation.Right;
			else if (Facing == Orientation.Right)
				Facing = Orientation.Top;
			else if (Facing == Orientation.Top)
				Facing = Orientation.Left;
			else
				Facing = Orientation.Bottom;
		}

		private void RotateRight()
		{
			RotateLeft();
			RotateLeft();
			RotateLeft();
		}

		private void BackUp()
		{
			UTurn();
			Move1();
			UTurn();
		}

		private void Move1()
		{
			var current = _game.Board.SquareAtCoordinate(Position) as Floor;

			if (current == null)
				throw new InvalidOperationException("Is this robot flying?");

			// If we are facing an edge, then we cannot move out of this square
			var edge = current.GetEdge(Facing);
			if (edge != null)
				return;

			int x = Position.X;
			int y = Position.Y;
			switch (Facing)
			{
				case Orientation.Top:
					--y;
					break;
				case Orientation.Right:
					++x;
					break;
				case Orientation.Bottom:
					++y;
					break;
				case Orientation.Left:
					--x;
					break;
				default:
					throw new InvalidOperationException("Orientation");
			}

			var target = new Coordinate {X = x, Y = y};
			ISquare targetSquare = _game.Board.SquareAtCoordinate(target);

			if (targetSquare == null)
				throw new NotImplementedException("Robot fell of board");
			if (targetSquare.GetType() == typeof(Pit))
				throw new NotImplementedException("Robot fell into a pit");

			var targetFloor = (Floor) targetSquare;
			Orientation opposite = Utilities.GetOppositeOrientation(Facing);

			// Make sure there is no edge blocking our entrance in the target floor square
			IEdge targetFloorEdge = targetFloor.GetEdge(opposite);
			if (targetFloorEdge != null)
				return;

			Position = target;
		}

		private void Move2()
		{
			Move1();
			Move1();
		}

		private void Move3()
		{
			Move1();
			Move1();
			Move1();
		}

		public int CardPriorityAtRegister(int register)
		{
			if (register > Constants.RobotRegisters || register < 1)
				throw new ArgumentException("Invalid register");

			return Cards.First(x => x.Register == register).Card;
		}

		public void DealCard(int priority)
		{
			Cards.Add(new RobotCard { Card = priority, Register = 0});
		}

		public IEnumerable<int> CardsToPlace()
		{
			return from card in Cards where card.Register == 0 select card.Card;
		}

		public void PlaceCard(int card, int register)
		{
			RobotCard c = Cards.First(x => x.Card == card);
			c.Register = register;
		}

		public IEnumerable<int> PickUpCards()
		{
			var cardsToPickUp = new List<int>();
			int keepCardsAtRegistersOrBelow = Constants.RobotRegisters - LockedRegisters();

			Cards = Cards.OrderBy(x => x.Register).ToList();
			for (int i = 0; i < Cards.Count; ++i)
			{
				if (Cards.First().Register <= keepCardsAtRegistersOrBelow)
				{
					cardsToPickUp.Add(Cards.First().Card);
					Cards.RemoveAt(0);
					continue;
				}

				break;
			}

			return cardsToPickUp;
		}

		public void ResetCards()
		{
			int resetCardsAtRegisterOrBelow = Constants.RobotRegisters - LockedRegisters();

			Cards = Cards.OrderBy(x => x.Register).ToList();
			foreach (var card in Cards)
			{
				if (card.Register <= resetCardsAtRegisterOrBelow)
				{
					card.Register = 0;
				}
			}
		}

		private int LockedRegisters()
		{
			return Math.Max(0, Constants.RobotRegisters - Deck.CardsPerRobot - Damage);
		}
	}
}
