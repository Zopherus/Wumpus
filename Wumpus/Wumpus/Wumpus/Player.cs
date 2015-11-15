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
	class Player
	{
		public const int rectangleSize = 50;
		private int turns, gold, arrows, direction, counter, counterHolder, currentRoom;
		private Rectangle position;

		//constructors
        public Player(Rectangle rectangle, int arrows, int gold)
        {
            this.position = rectangle;
            this.arrows = arrows;
            this.gold = gold;
        }

		public Player(Rectangle rectangle)
		{
			this.position = rectangle;
		}

		public Player()
		{

		}

		public int Gold
		{
			get { return gold; }
		}

        public int Arrows
        {
            get { return arrows; }
            set { arrows = value; }
        }

        public int CurrentRoom
        {
            get { return currentRoom; }
            set { currentRoom = (value > 0 && value < 31) ? value : currentRoom; }
        }

		//when tries to set the position
		//doesn't change position if CheckOffScreen is false
		//Stops from going offscreen
		public Rectangle Position
		{
			get { return position; }
			set { position = CheckOffScreen(value) ? value : position;
                  checkRoomChange(value);
            }
		}

		public int CounterHolder
		{
			get { return counterHolder; }
		}
		// should be mod 4

		//counts the characters motion/frames
		public int Counter
		{
			get { return counter; }
			set 
			{ 
				counter = value % 30;
				//every 10 frames the character will change
				counterHolder = (int)Math.Truncate(counter / 10.0);
			}
		}

		//tells you which way your character is facing
		//makes sure the direction is valid
		//defaults to 1
		public int Direction
		{
			get { return direction; }
			set { 
                    direction = (value > -1 && value < 4) ? value : 1;
                }

		}

		public void addArrows()
		{
			this.arrows++;
		}

		public void useArrow()
		{
			this.arrows--;
		}

		public void addGold(int n)
		{
			this.gold += n;
		}

		public void subtractGold(int n)
		{
			this.gold = (this.gold - n > 0)? (this.gold - n) : 0;
		}

		public int calculateScore()
		{
			return 100 - this.turns + this.gold + (this.arrows * 10);
		}

		//checks if position is within hexagon
		private bool CheckOffScreen(Rectangle position)
		{
            int[] AdjRooms = Cave.Matrix[currentRoom-1].ConnectedRooms;
            if (AdjRooms.Contains<int>(Hexagon.DoorTop(GameControl.Player.CurrentRoom)) && position.Y < 75)
            {
                return true;
            }
            else if (AdjRooms.Contains<int>(Hexagon.DoorTopRight(GameControl.Player.CurrentRoom)) && (Position.Y < 2*Position.X - 790))
            {
                return true;
            }
            else if (AdjRooms.Contains<int>(Hexagon.DoorBottomRight(GameControl.Player.CurrentRoom)) && (Position.Y > -1.5*Position.X + 925))
            {
                return true;
            }
            else if (AdjRooms.Contains<int>(Hexagon.DoorBottom(GameControl.Player.CurrentRoom)) && position.Y > 285)
            {
                return true;
            }
            else if (AdjRooms.Contains<int>(Hexagon.DoorBottomLeft(GameControl.Player.CurrentRoom))&& (Position.Y > 1.5*Position.X -190))
            {
                return true;
            }
            else if (AdjRooms.Contains<int>(Hexagon.DoorTopLeft(GameControl.Player.CurrentRoom)) && (Position.Y < -2*Position.X + 750))
            {
                return true;
            }
            else 
                return Math.Pow(position.X - 375, 2) + Math.Pow(position.Y - 180, 2) < 12100;
		}

        public void checkRoomChange(Rectangle position)
        {
            int[] AdjRooms = Cave.Matrix[currentRoom-1].ConnectedRooms;
            if (AdjRooms.Contains<int>(Hexagon.DoorTop(GameControl.Player.CurrentRoom)) && position.Y < 20)
            {
                gold++;
                turns++;
                currentRoom = Hexagon.DoorTop(currentRoom);
                resetPosition();
            }
            if (AdjRooms.Contains<int>(Hexagon.DoorBottom(GameControl.Player.CurrentRoom)) && position.Y > 340)
            {
                gold++;
                turns++;
                currentRoom = Hexagon.DoorBottom(currentRoom);
                resetPosition();
            }
            if (AdjRooms.Contains<int>(Hexagon.DoorTopRight(GameControl.Player.CurrentRoom)) && (Position.Y < 2 *Position.X - 900))
            {
                gold++;
                turns++;
                currentRoom = Hexagon.DoorTopRight(currentRoom);
                resetPosition();
            }
            if (AdjRooms.Contains<int>(Hexagon.DoorBottomRight(GameControl.Player.CurrentRoom)) && (Position.Y > -1.5*Position.X + 1000))
            {
                gold++;
                turns++;
                currentRoom = Hexagon.DoorBottomRight(currentRoom);
                resetPosition();
            }
            if (AdjRooms.Contains<int>(Hexagon.DoorTopLeft(GameControl.Player.CurrentRoom)) && (Position.Y < -2 * Position.X + 600))
            {
                gold++;
                turns++;
                currentRoom = Hexagon.DoorTopLeft(currentRoom);
                resetPosition();
            }
            if (AdjRooms.Contains<int>(Hexagon.DoorBottomLeft(GameControl.Player.CurrentRoom)) && (Position.Y > 1.5 * Position.X - 50))
            {
                gold++;
                turns++;
                currentRoom = Hexagon.DoorBottomRight(currentRoom);
                resetPosition();
            }
        }

        private void resetPosition()
        {
            position = new Rectangle(375, 200, rectangleSize, rectangleSize);
        }
	}
}
