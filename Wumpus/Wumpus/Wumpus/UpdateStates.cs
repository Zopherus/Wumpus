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
        public static void UpdateCave() 
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
                WumpusGame.GameState = GameState.Map;
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
                if (strings[5] == "1")
                {
                    GameControl.TriviaCorrect();
                    triviaWinCounter++;
                }
                else
                {
                    GameControl.TriviaIncorrect();
                }
            }
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.B))
            {
                if (strings[5] == "2")
                {
                    GameControl.TriviaCorrect();
                    triviaWinCounter++;
                }
                else
                {
                    GameControl.TriviaIncorrect();
                }
            }
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.C))
            {
                if (strings[5] == "3")
                {
                    GameControl.TriviaCorrect();
                    triviaWinCounter++;
                }
                else
                {
                    GameControl.TriviaIncorrect();
                }
            }
            if (WumpusGame.KeyboardState.IsKeyDown(Keys.D))
            {
                if (strings[5] == "4")
                {
                    GameControl.TriviaCorrect();
                    triviaWinCounter++;
                }
                else
                {
                    GameControl.TriviaIncorrect();
                }
            }
        }

        public static void UpdateTriviaLose() 
        {
            if (WumpusGame.MouseState.LeftButton == ButtonState.Pressed && WumpusGame.OldMouseState.LeftButton == ButtonState.Released)
            {
                WumpusGame.GameState = GameState.Cave;
            }
        }

        public static void UpdateTriviaWin() 
        {
            if (WumpusGame.MouseState.LeftButton == ButtonState.Pressed && WumpusGame.OldMouseState.LeftButton == ButtonState.Released)
            {
                WumpusGame.GameState = GameState.Cave;
            }
        }
    }
}
