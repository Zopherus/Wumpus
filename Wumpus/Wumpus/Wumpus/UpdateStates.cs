using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Wumpus
{
    class UpdateStates
    {
		private static string[] TriviaQuestions = new string[5];
        private static int TriviaWinCounter = 0;

        private static bool EnteredHelicopterRoom = false;
        private static Timer HelicopterTimer;
        private static bool EnteredOsamaRoom = false;
        private static Timer OsamaTimer;

        public static void UpdateCave(GameTime gameTime) 
        {
            if (WumpusGame.MouseState.LeftButton == ButtonState.Pressed && WumpusGame.OldMouseState.LeftButton == ButtonState.Released)
            {
                //Define a Rectangle for this button?
                //Make sure Rectangle scales off screenwidth/height
                if (WumpusGame.MouseState.X > 40 && WumpusGame.MouseState.X < 190 && WumpusGame.MouseState.Y > 475 && WumpusGame.MouseState.Y < 565)
                {
                    WumpusGame.GameState = GameState.Map;
                }
            }

            if (WumpusGame.KeyboardState.IsKeyDown(Keys.Right) || WumpusGame.KeyboardState.IsKeyDown(Keys.D))
            {
                WumpusGame.Player.MoveRight();
            }
            else if (WumpusGame.KeyboardState.IsKeyDown(Keys.Up) || WumpusGame.KeyboardState.IsKeyDown(Keys.W))
            {
                WumpusGame.Player.MoveUp();
            }
            else if (WumpusGame.KeyboardState.IsKeyDown(Keys.Left) || WumpusGame.KeyboardState.IsKeyDown(Keys.A))
            {
                WumpusGame.Player.MoveLeft();
            }
            else if (WumpusGame.KeyboardState.IsKeyDown(Keys.Down) || WumpusGame.KeyboardState.IsKeyDown(Keys.S))
            {
                WumpusGame.Player.MoveDown();
            }

			if (Map.Helicopter1 == WumpusGame.Player.CurrentRoom || Map.Helicopter2 == WumpusGame.Player.CurrentRoom)
			{
				//When first enter helicopter room
				if (!EnteredHelicopterRoom)
				{
					EnteredHelicopterRoom = true;
                    // 3000 milliseconds == 3 seconds
                    HelicopterTimer = new Timer(3000);
				}

                HelicopterTimer.tick(gameTime);
                if (HelicopterTimer.TimeMilliseconds > HelicopterTimer.Interval)
                {
                    WumpusGame.Player.HelicopterCarryOff();
                    HelicopterTimer.reset();
                    EnteredHelicopterRoom = false;
                }
			}
            else
            {
                EnteredHelicopterRoom = false;
            }

			if (Map.OsamaRoom == WumpusGame.Player.CurrentRoom)
			{
				if (!EnteredOsamaRoom)
				{
					EnteredOsamaRoom = true;
                    // 3000 milliseconds == 3 seconds
                    OsamaTimer = new Timer(3000);
					TriviaWinCounter = 0;
				}
                OsamaTimer.tick(gameTime);
				if (OsamaTimer.TimeMilliseconds > OsamaTimer.Interval)
				{
					if (TriviaQuestionCounter < 5 && TriviaWinCounter < 3 && TriviaQuestionCounter - TriviaWinCounter < 3)
					{
						WumpusGame.TriviaQuestions = GameControl.newTrivia();
						WumpusGame.GameState = GameState.Trivia;
						TriviaQuestionCounter++;
					}
					else if (TriviaWinCounter >= 3)
					{
						WumpusGame.Player.HelicopterCarryOff();
						EnteredOsamaRoom = false;
					}
					else if (TriviaQuestionCounter - TriviaWinCounter >= 3)
					{
						WumpusGame.GameState = GameState.Lose;
					}
				}
			}
        }

        public static void UpdateHelp() 
        {
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.Left))
            {
                WumpusGame.GameState = GameState.Menu;
            }
        }

        public static void UpdateHighscore() 
        {
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.Left))
            {
				WumpusGame.GameState = GameState.Menu;
            }
        }

        public static void UpdateLose() 
        {
            if (WumpusGame.MouseState.LeftButton == ButtonState.Pressed && WumpusGame.OldMouseState.LeftButton == ButtonState.Pressed)
            {
                WumpusGame.GameState = GameState.Menu;
            }
        }

        public static void UpdateMap() 
        {
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.Left))
                WumpusGame.GameState = GameState.Cave;
        }

        public static void UpdateMenu() 
        {
            if (WumpusGame.MouseState.LeftButton == ButtonState.Pressed && WumpusGame.OldMouseState.LeftButton == ButtonState.Released)
            {
                Point mousePoint = new Point(WumpusGame.MouseState.X, WumpusGame.MouseState.Y);
                if (Menu.NewGameRectangle.Contains(mousePoint))
                {
                    WumpusGame.GameState = GameState.Cave;
                }
                else if (Menu.HighScoreRectangle.Contains(mousePoint))
                {
                    WumpusGame.GameState = GameState.Highscore;
                }
                else if (Menu.HelpRectangle.Contains(mousePoint))
                {
                    WumpusGame.GameState = GameState.Help;
                }
                else if (Menu.ExitRectangle.Contains(mousePoint))
                { 
                    Program.game.Exit();
                }
            }
        }

        public static void UpdateShop() { }

        public static void UpdateTrivia() 
        {
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.A))
            {
                if (TriviaQuestions[5] == "1")
                {
                    //GameControl.TriviaCorrect();
                    TriviaWinCounter++;
                }
                else
                {
                    //GameControl.TriviaIncorrect();
                }
            }
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.B))
            {
                if (TriviaQuestions[5] == "2")
                {
                    //GameControl.TriviaCorrect();
                    TriviaWinCounter++;
                }
                else
                {
                    //GameControl.TriviaIncorrect();
                }
            }
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.C))
            {
                if (TriviaQuestions[5] == "3")
                {
                   // GameControl.TriviaCorrect();
                    TriviaWinCounter++;
                }
                else
                {
                    //GameControl.TriviaIncorrect();
                }
            }
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.D))
            {
                if (TriviaQuestions[5] == "4")
                {
                    //GameControl.TriviaCorrect();
                    TriviaWinCounter++;
                }
                else
                {
                    //GameControl.TriviaIncorrect();
                }
            }
			//If the question has been answered
			if (WumpusGame.TriviaState != TriviaState.NotAnswered)
			{
				if (WumpusGame.MouseState.LeftButton == ButtonState.Pressed && WumpusGame.OldMouseState.LeftButton == ButtonState.Released)
				{
					WumpusGame.GameState = GameState.Cave;
				}
			}
        }
    }
}
