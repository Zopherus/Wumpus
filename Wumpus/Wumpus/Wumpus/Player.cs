using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Wumpus
{
	public class Player
	{
        public const int SpriteSheetHeight = 32;
        public const int SpriteSheetWidth = 32;
		public const int rectangleSize = 50;
        public const int Speed = 5;
        public int Turns { get; private set; }
        public int Gold { get; private set; }
        public int Arrows { get; private set; }
        private int characterFrameCounter;
        public Room CurrentRoom { get; private set; }
        public Rectangle Position { get; private set; }
        public Direction Direction { get; private set; }

		//constructors
        public Player(Rectangle Position, int Arrows, int Gold)
        {
            this.Position = Position;
            this.Arrows = Arrows;
            this.Gold = Gold;
            CurrentRoom = Cave.Rooms[0];
        }

		public Player(Rectangle rectangle)
		{
			this.Position = rectangle;
            CurrentRoom = Cave.Rooms[0];
		}

		public Player()
        {
            CurrentRoom = Cave.Rooms[0];
        }

        //Stored internally as a number from 0 - 39, DrawStates only uses the tens digit
        //Make 3 map to 1, BushTexture goes back to center column after the right column
        public int CharacterFrameCounter
        {
            get
            {
                int result = characterFrameCounter / 10;
                if (result == 3)
                    result = 1;
                return result;
            }
        }


		public void addGold(int amount)
		{
            Gold += amount;
		}

		public int calculateScore()
		{
			return 100 - Turns + Gold + 10 * Arrows;
		}

        public void HelicopterCarryOff()
        {
            Random random = new Random();
            CurrentRoom = Cave.Rooms[random.Next(Cave.Rooms.Length)];
        }

		//checks if position is within hexagon
		private bool CheckOnScreen(Rectangle position)
		{
            if (CurrentRoom.ConnectedRooms[0] && position.Y < 70)
            {
                ChangeRoom(0);
                return false;
            }
            else if (CurrentRoom.ConnectedRooms[1] && (position.Y < 2 * position.X - 790))
            {
                ChangeRoom(1);
                return false;
            }
            else if (CurrentRoom.ConnectedRooms[2] && (position.Y > -1.5 * position.X + 925))
            {
                ChangeRoom(2);
                return false;
            }
            else if (CurrentRoom.ConnectedRooms[3] && position.Y > 280)
            {
                ChangeRoom(3);
                return false;
            }
            else if (CurrentRoom.ConnectedRooms[4] && (position.Y > 1.5 * position.X - 190))
            {
                ChangeRoom(4);
                return false;
            }
            else if (CurrentRoom.ConnectedRooms[5] && (position.Y < -2 * position.X + 750))
            {
                ChangeRoom(5);
                return false;
            }
            else
                return Math.Pow(position.X - 375, 2) + Math.Pow(position.Y - 180, 2) < 14400;
		}

        private void ChangeRoom(int adjRoom)
        {
            Gold++;
            Turns++;
            CurrentRoom = CurrentRoom.AdjRooms[adjRoom];
            resetPosition();
        }

        public void MoveUp()
        {
            Rectangle newPosition = new Rectangle(Position.X, Position.Y - Speed, Position.Width, Position.Height);
            if (CheckOnScreen(newPosition))
            {
                Position = newPosition;
                Direction = Direction.Up;
                IncreaseFrameCounter();
            }
        }

        public void MoveRight()
        {
            Rectangle newPosition = new Rectangle(Position.X + Speed, Position.Y, Position.Width, Position.Height);
            if (CheckOnScreen(newPosition))
            {
                Position = newPosition;
                Direction = Direction.Right;
                IncreaseFrameCounter();
            }
        }

        public void MoveDown()
        {
            Rectangle newPosition = new Rectangle(Position.X, Position.Y + Speed, Position.Width, Position.Height);
            if (CheckOnScreen(newPosition))
            {
                Position = newPosition;
                Direction = Direction.Down;
                IncreaseFrameCounter();
            }
        }

        public void MoveLeft()
        {
            Rectangle newPosition = new Rectangle(Position.X - Speed, Position.Y, Position.Width, Position.Height);
            if (CheckOnScreen(newPosition))
            {
                Position = newPosition;
                Direction = Direction.Left;
                IncreaseFrameCounter();
            }
        }

        private void IncreaseFrameCounter()
        {
            characterFrameCounter = (characterFrameCounter + 1) % 40;
        }

        private void resetPosition()
        {
            Position = new Rectangle(375, 180, rectangleSize, rectangleSize);
        }
	}
}
