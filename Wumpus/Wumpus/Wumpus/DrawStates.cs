using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wumpus
{
    class DrawStates
    {
        public static void DrawCave() 
        {
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, new Point(GameControl.Player.Position.X, GameControl.Player.Position.Y).ToString(), new Vector2(0, 0), Color.Black);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BottomHUDTexture, new Rectangle(0, 450, 800, 150), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.GroundTexture, new Rectangle(0, 0, 800, 450), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, GameControl.DisplayHazards(), new Vector2(0, 420), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, GameControl.Player.Gold.ToString(), new Vector2(660 - WumpusGame.MotorwerkFont.MeasureString("0").X * (int)Math.Log10((double)GameControl.Player.Gold + 1), 465), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, GameControl.Player.Arrows.ToString(), new Vector2(660 - WumpusGame.MotorwerkFont.MeasureString("0").X * (int)Math.Log10((double)GameControl.Player.Arrows + 1), 525), Color.Black);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BlackTexture, new Rectangle(0, 450, 800, 1), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BushTexture, GameControl.Player.Position, new Rectangle(32 * GameControl.Player.CounterHolder, 32 * GameControl.Player.Direction, spriteSheetWidth, spriteSheetHeight), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.MoneyCurrencyTexture, new Rectangle(675, 445, 100, 100), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BulletsTexture, new Rectangle(675, 500, 100, 100), new Rectangle(0, 0, 45, 41), Color.White);
            int[] AdjRooms = Cave.Matrix[GameControl.Player.CurrentRoom - 1].ConnectedRooms;
            for (int i = 0; i < 7; i++)
            {
                bool value = false;
                foreach (int number in AdjRooms)
                {
                    switch (i)
                    {
                        case 1:
                            if (Hexagon.DoorTop(GameControl.Player.CurrentRoom) == number)
                                value = true;
                            break;
                        case 2:
                            if (Hexagon.DoorTopRight(GameControl.Player.CurrentRoom) == number)
                                value = true;
                            break;
                        case 3:
                            if (Hexagon.DoorBottomRight(GameControl.Player.CurrentRoom) == number)
                                value = true;
                            break;
                        case 4:
                            if (Hexagon.DoorBottom(GameControl.Player.CurrentRoom) == number)
                                value = true;
                            break;
                        case 5:
                            if (Hexagon.DoorBottomLeft(GameControl.Player.CurrentRoom) == number)
                                value = true;
                            break;
                        case 6:
                            if (Hexagon.DoorTopLeft(GameControl.Player.CurrentRoom) == number)
                                value = true;
                            break;
                        default:
                            break;
                    }
                }
                if (value)
                    continue;
                else
                    WumpusGame.SpriteBatch.Draw(WumpusGame.TreeBoundaryTextures[i], new Rectangle(0, 0, 800, 450), Color.White);
            }
            //map button
            WumpusGame.SpriteBatch.Draw(WumpusGame.BlackTexture, new Rectangle(40, 475, 150, 1), Color.Black);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BlackTexture, new Rectangle(190, 475, 1, 90), Color.Black);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BlackTexture, new Rectangle(40, 475, 1, 90), Color.Black);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BlackTexture, new Rectangle(40, 565, 151, 1), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Map", new Vector2(115 - (WumpusGame.MotorwerkFont.MeasureString("Map")).X / 2, 520 - (WumpusGame.MotorwerkFont.MeasureString("Map")).Y / 2), Color.Black);

            if (GameControl.GameMap.Helicopter1 == GameControl.Player.CurrentRoom || GameControl.GameMap.Helicopter2 == GameControl.Player.CurrentRoom)
            {
                if (!value)
                {
                    value = true;
                    time = (int)gameTime.TotalGameTime.Seconds;
                }
                WumpusGame.SpriteBatch.Draw(WumpusGame.HelicopterTexture, GameControl.helicopter.Position, GameControl.helicopter.SourceRectangle, Color.White);
                if (gameTime.TotalGameTime.Seconds - time > 3)
                {
                    GameControl.Player.CurrentRoom = GameControl.GameMap.BatCarryOff();
                    value = false;
                }
            }
            if (GameControl.GameMap.OsamaRoom == GameControl.Player.CurrentRoom)
            {
                if (!value)
                {
                    value = true;
                    time = (int)gameTime.TotalGameTime.Seconds;
                    triviaWinCounter = 0;
                }
                WumpusGame.SpriteBatch.Draw(WumpusGame.OsamaTexture, new Rectangle(375, 200, 50, 50), new Rectangle(0, 0, WumpusGame.SpriteSheetWidth, WumpusGame.SpriteSheetHeight), Color.White);
                if (gameTime.TotalGameTime.Seconds - time > 3)
                {
                    int counter = 0;
                    if (counter < 5 && triviaWinCounter < 3 && counter - triviaWinCounter < 3)
                    {
                        strings = GameControl.newTrivia();
                        //All Game control should be in update states
                        GameControl.resetGameState();
                        GameControl.trivia = true;
                        counter++;
                    }
                    else if (triviaWinCounter >= 3)
                    {
                        GameControl.Player.CurrentRoom = GameControl.GameMap.BatCarryOff();
                        value = false;
                    }
                    else if (counter - triviaWinCounter >= 3)
                    {
                        GameControl.resetGameState();
                        GameControl.lose = true;
                    }
                }
            }
        }
        public static void DrawHelp() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.ShopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Help: ", new Vector2(0, 0), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "The goal of this game is to kill the wumpus", new Vector2(0, 20), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Find the wumpus in the cave and answer", new Vector2(0, 40), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "trivia questions to defeat him", new Vector2(0, 60), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Use the arrow keys to move around", new Vector2(0, 80), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Watch out for the Helicopter that will ", new Vector2(0, 100), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "move you to a random room", new Vector2(0, 1205), Color.White);

            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Press left to go back to the menu", new Vector2(0, 560), Color.White);
        }
        public static void DrawHighscore() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.ShopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
            List<StoreScore> highscore = Highscore.GetScore();
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "High Scores: ", new Vector2(0, 0), Color.White);
            int number = (highscore.Count > 10) ? 10 : highscore.Count;
            for (int i = 0; i < number; i++)
            {
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, (i + 1).ToString() + ". " + highscore[i].ToString(), new Vector2(0, 50 * i + 50), Color.White);
            }
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Press left to go back to the menu", new Vector2(0, 560), Color.White);
        }
        public static void DrawLose() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.ShopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Lose", new Vector2(400, 0), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Score: " + GameControl.Player.calculateScore().ToString(), new Vector2(350, 0), Color.White);
        }
        public static void DrawMap() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.MapTexture, new Rectangle(0, 0, 800, 600), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Current Room: " + GameControl.Player.CurrentRoom.ToString(), new Vector2(0, 0), Color.White);
            int[][] array = GameControl.GameCave.getMap();
            for (int i = 0; i < 5; i++)
            {
                foreach (int number in array[i])
                {
                    if (number % 2 == 1)
                    {
                        WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, number.ToString(), new Vector2(200 + 75 * ((number - 1) % 6), 87 + 88 * i), Color.White);
                    }
                    else
                    {
                        WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, number.ToString(), new Vector2(200 + 75 * ((number - 1) % 6), 120 + 88 * i), Color.White);
                    }
                }
            }
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Press left to go back", new Vector2(0, 560), Color.White);
        }
        public static void DrawMenu() 
        {
            //50 gap in between each rectangle
            //adding 1 to fill in the corner of rectangle
            WumpusGame.SpriteBatch.Draw(wallpaperTexture, new Rectangle(0, 0, 800, 600), Color.White);

            //New Game Rectangle
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(Menu.NewGameRectangle.X, newGameRectangle.Y, newGameRectangle.Width, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(newGameRectangle.X + newGameRectangle.Width, newGameRectangle.Y, lineWidth, newGameRectangle.Height), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(newGameRectangle.X, newGameRectangle.Y + newGameRectangle.Height, newGameRectangle.Width + 1, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(newGameRectangle.X, newGameRectangle.Y, lineWidth, newGameRectangle.Height), Color.Black);

            //High Score Rectangle
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X, highScoreRectangle.Y, highScoreRectangle.Width, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X + highScoreRectangle.Width, highScoreRectangle.Y, lineWidth, highScoreRectangle.Height), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X, highScoreRectangle.Y + highScoreRectangle.Height, highScoreRectangle.Width + 1, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X, highScoreRectangle.Y, lineWidth, highScoreRectangle.Height), Color.Black);

            //Help Rectangle
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X, helpRectangle.Y, helpRectangle.Width, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X + helpRectangle.Width, helpRectangle.Y, lineWidth, helpRectangle.Height), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X, helpRectangle.Y + helpRectangle.Height, helpRectangle.Width + 1, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X, helpRectangle.Y, lineWidth, helpRectangle.Height), Color.Black);

            //Exit Rectangle
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X, exitRectangle.Y, exitRectangle.Width, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X + exitRectangle.Width, exitRectangle.Y, lineWidth, exitRectangle.Height), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X, exitRectangle.Y + exitRectangle.Height, exitRectangle.Width + 1, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X, exitRectangle.Y, lineWidth, exitRectangle.Height), Color.Black);

            //use WumpusGame.MotorwerkFont.MeasureString() to measure the string and place in middle of rectangle
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "New Game", new Vector2(newGameRectangle.X + (newGameRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("New Game").X) / 2,
                                    newGameRectangle.Y + (newGameRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("New Game").Y) / 2), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "High Scores", new Vector2(highScoreRectangle.X + (highScoreRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("High Scores").X) / 2,
                                    highScoreRectangle.Y + (highScoreRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("High Scores").Y) / 2), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Help", new Vector2(helpRectangle.X + (helpRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("Help").X) / 2,
                                    helpRectangle.Y + (helpRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("Help").Y) / 2), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Exit", new Vector2(exitRectangle.X + (exitRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("Exit").X) / 2,
                                    exitRectangle.Y + (exitRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("Exit").Y) / 2), Color.Black);
        }
        public static void DrawShop() 
        {
            WumpusGame.SpriteBatch.Draw(shopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Press left to go back", new Vector2(0, 560), Color.White);
        }
        public static void DrawTrivia() 
        {
            WumpusGame.SpriteBatch.Draw(shopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
            string question = strings[0];
            string answer1 = "A: " + strings[1];
            string answer2 = "B: " + strings[2];
            string answer3 = "C: " + strings[3];
            string answer4 = "D: " + strings[4];
            int correctAnswer = int.Parse(strings[5]);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.SmallMotorwerkFont, question, new Vector2((screenWidth -WumpusGame.SmallMotorwerkFont.MeasureString(question).X) / 2, (screenHeight - WumpusGame.SmallMotorwerkFont.MeasureString(question).Y) / 12), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer1, new Vector2((screenWidth - WumpusGame.MotorwerkFont.MeasureString(answer1).X) / 2, (screenHeight - WumpusGame.MotorwerkFont.MeasureString(answer1).Y) * 4 / 16 + 30), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer2, new Vector2((screenWidth - WumpusGame.MotorwerkFont.MeasureString(answer2).X) / 2, (screenHeight - WumpusGame.MotorwerkFont.MeasureString(answer2).Y) * 6 / 16 + 30), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer3, new Vector2((screenWidth - WumpusGame.MotorwerkFont.MeasureString(answer3).X) / 2, (screenHeight - WumpusGame.MotorwerkFont.MeasureString(answer3).Y) * 8 / 16 + 30), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer4, new Vector2((screenWidth - WumpusGame.MotorwerkFont.MeasureString(answer4).X) / 2, (screenHeight - WumpusGame.MotorwerkFont.MeasureString(answer4).Y) * 10 / 16 + 30), Color.White);
            if (GameControl.triviaWin)
            {
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Correct", new Vector2((screenWidth - WumpusGame.MotorwerkFont.MeasureString("Correct").X) / 2, 560), Color.White);
            }
            if (GameControl.triviaLose)
            {
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Incorrect", new Vector2((screenWidth - WumpusGame.MotorwerkFont.MeasureString("Incorrect").X) / 2, 560), Color.White);
            }
        }
        public static void DrawTriviaLose() { }
        public static void DrawTriviaWin() { }

        //Draws the outline of a rectangle with a certain texture and a certain width on the border
        private static void DrawRectangleOutline(Rectangle rectangle, Texture2D texture, int lineWidth)
        {
            WumpusGame.SpriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height), Color.Black);
            //Add one extra pixel at the end to fill in the corner
            WumpusGame.SpriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + 1, lineWidth), Color.Black);
            WumpusGame.SpriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height), Color.Black);
        }
    }
}
