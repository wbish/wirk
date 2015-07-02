using System;
using System.Collections.Generic;
using System.Linq;

namespace WiRK.Terminator
{
	public class Robot
	{
		internal Game Game { get; set; }

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
			Game = game;
		}

		public void ExecuteMove(int register)
		{
			if (register > Constants.RobotRegisters || register < 1)
				throw new ArgumentException("Invalid register");

			int priority = CardAtRegister(register);
			ProgramCardType card = ProgramCard.GetCardByPriority(priority);

			ExecuteMove(card);
		}

		internal void ExecuteMove(ProgramCardType card)
		{
			ITile currentTile = Game.Board.GetTile(Position);
			if (currentTile == null)
				return; // This robot is not on the board

			switch (card)
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
					throw new InvalidOperationException("Invalid move");
			}
		}

		private void UTurn()
		{
			RotateLeft();
			RotateLeft();
		}

		internal void RotateLeft()
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

		internal void RotateRight()
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
			var current = Game.Board.GetTile(Position) as Floor;

			if (current == null)
			{
				// Robot is dead -- off the board
				return;
			}

			// If we are facing an edge, then we cannot move out of this tile
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
			ITile targetTile = Game.Board.GetTile(target);

			if (targetTile == null || targetTile.GetType() == typeof(Pit))
			{
				// BUG: Do we need a IsDead property or is invalid position good enough?
				Position = new Coordinate {X = -1, Y = -1};
				return;
			}

			var targetFloor = (Floor) targetTile;
			Orientation opposite = Utilities.GetOppositeOrientation(Facing);

			// Make sure there is no edge blocking our entrance in the target floor tile
			IEdge targetFloorEdge = targetFloor.GetEdge(opposite);
			if (targetFloorEdge != null)
				return;

			// BUG: We need to handle pushing robots

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

		public int CardAtRegister(int register)
		{
			if (register > Constants.RobotRegisters || register < 1)
				throw new ArgumentException("Invalid register");

			return Cards.First(x => x.Register == register).Card;
		}

		public void DealCard(int priority, int register = 0)
		{
			Cards.Add(new RobotCard { Card = priority, Register = register});
		}

		/// <summary>
		/// Cards that can be placed. Not current placed.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<int> CardsToPlace()
		{
			return new List<int>(from card in Cards where card.Register == 0 select card.Card);
		}

		/// <summary>
		/// Place a card on a register
		/// </summary>
		/// <param name="card">Card to place</param>
		/// <param name="register">Register to set on</param>
		public void PlaceCard(int card, int register)
		{
			RobotCard c = Cards.First(x => x.Card == card);
			c.Register = register;
		}

		/// <summary>
		/// Picks up all cards not locked by damage
		/// </summary>
		/// <returns>Cards picked up</returns>
		public IEnumerable<int> PickUpCards()
		{
			var cardsToPickUp = new List<int>();
			int keepCardsAtRegistersOrBelow = Constants.RobotRegisters - LockedRegisters();

			Cards = Cards.OrderBy(x => x.Register).ToList();
			int count = Cards.Count;
			for (int i = 0; i < count; ++i)
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

		/// <summary>
		/// Unplace a card from a register.
		/// </summary>
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
