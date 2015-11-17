using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Wumpus
{
	class GameControl
	{
		public static Cave GameCave = new Cave();
		public static Map GameMap = new Map();
		private static TriviaList triviaList = new TriviaList();
		public static Player Player = new Player();
        public static Helicopter helicopter = new Helicopter(new Rectangle(320, 120, 140, 165));
		private static Random rnd = new Random(30);
		private int currentRoom;
		//private int startRoom = rnd.Next(0, 30);


        public static void Initialize()
        {
			WumpusGame.GameState = GameState.Menu;
            Player = new Player(new Rectangle(375, 200, Player.rectangleSize, Player.rectangleSize), 3, 0);
            Player.CurrentRoom = 1;
        }

		public static string[] newTrivia()
		{
			Trivia trivia = triviaList.GetTrivia();
            string[] result = { trivia.Question, trivia.Answer1, trivia.Answer2, trivia.Answer3, trivia.Answer4, trivia.Answer.ToString() };
            return result;
		}
		public bool shotArrow(int a)
		{
			if (a == GameMap.OsamaRoom)
			{
				return true;
			}
			else
			{
				Player.useArrow();
				return false;
			} 
		}
		public int[] GetConnectedRooms()
		{
			return GameCave.getConnectedRooms(currentRoom);
		}

		public int CurrentRoom
		{
			get { return currentRoom; }
			set { currentRoom = value; }
		}
		public static string DisplayHazards()
		{ 
			GameMap.AdjRooms = GameCave.getAdjRooms(Player.CurrentRoom);
			GameMap.CheckForHazards();
			int apache1 = GameMap.Helicopter1;
			int apache2 = GameMap.Helicopter2;
			int oil1 = GameMap.OIL1;
			int oil2 = GameMap.OIL2;
			int osamaRoom = GameMap.OsamaRoom;
			string hazards = GameMap.Warnings;

			return hazards;
		}
		public static void TriviaCorrect()
		{
			Player.addGold(1);
            WumpusGame.TriviaState = TriviaState.Correct;
		}
		public static void TriviaIncorrect()
		{
			Player.subtractGold(1);
			WumpusGame.TriviaState = TriviaState.Incorrect;
		}
		public int[] DisplayInv()
		{
			int a = Player.Arrows;
			int b = Player.Gold;
			int[] ab = new int[] { a, b };

			// Arrow , Gold
			return ab;
		}
	}
}
