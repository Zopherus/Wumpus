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
            WumpusGame.SpriteBatch.Draw(WumpusGame.BottomHUDTexture, new Rectangle(0, 450, 800, 150), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.GroundTexture, new Rectangle(0, 0, 800, 450), Color.White);
            //WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, GameControl.DisplayHazards(), new Vector2(0, 420), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, WumpusGame.Player.Gold.ToString(), new Vector2(660 - WumpusGame.MotorwerkFont.MeasureString("0").X * (int)Math.Log10((double)WumpusGame.Player.Gold + 1), 465), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, WumpusGame.Player.Arrows.ToString(), new Vector2(660 - WumpusGame.MotorwerkFont.MeasureString("0").X * (int)Math.Log10((double)WumpusGame.Player.Arrows + 1), 525), Color.Black);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BlackTexture, new Rectangle(0, 450, 800, 1), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BushTexture, WumpusGame.Player.Position, new Rectangle(32 * WumpusGame.Player.CharacterFrameCounter, Player.SpriteSheetHeight * (int)WumpusGame.Player.Direction, Player.SpriteSheetWidth, Player.SpriteSheetHeight), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.MoneyCurrencyTexture, new Rectangle(675, 445, 100, 100), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BulletsTexture, new Rectangle(675, 500, 100, 100), new Rectangle(0, 0, 45, 41), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.DefaultRoomBoundary, new Rectangle(0, 0, 800, 450), Color.White);
            WumpusGame.SpriteBatch.Draw(WumpusGame.BushTexture, WumpusGame.Player.Position, new Rectangle(32 * WumpusGame.Player.CharacterFrameCounter, Player.SpriteSheetHeight * (int)WumpusGame.Player.Direction, Player.SpriteSheetWidth, Player.SpriteSheetHeight), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, new Point(WumpusGame.Player.Position.X, WumpusGame.Player.Position.Y).ToString(), new Vector2(0, 0), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, WumpusGame.Player.CurrentRoom.RoomNumber.ToString(), new Vector2(300, 150), Color.Black);
            //WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, WumpusGame.MouseState.ToString(), new Vector2(0, 0), Color.Black);
            for (int counter = 0; counter < WumpusGame.Player.CurrentRoom.ConnectedRooms.Length; counter++)
            {
                if (!WumpusGame.Player.CurrentRoom.ConnectedRooms[counter])
                    WumpusGame.SpriteBatch.Draw(WumpusGame.TreeBoundaryTextures[counter], new Rectangle(0, 0, 800, 450), Color.White);
            }
            //map button
			DrawRectangleOutline(new Rectangle(40, 475, 150, 90), WumpusGame.BlackTexture, 1);

            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Map", new Vector2(115 - (WumpusGame.MotorwerkFont.MeasureString("Map")).X / 2, 520 - (WumpusGame.MotorwerkFont.MeasureString("Map")).Y / 2), Color.Black);

            if (Map.Helicopter1 == WumpusGame.Player.CurrentRoom || Map.Helicopter2 == WumpusGame.Player.CurrentRoom)
            {
				WumpusGame.SpriteBatch.Draw(WumpusGame.HelicopterTexture, Helicopter.Position, Helicopter.SourceRectangle, Color.White);
            }
            if (Map.OsamaRoom == WumpusGame.Player.CurrentRoom)
            {
                WumpusGame.SpriteBatch.Draw(WumpusGame.OsamaTexture, new Rectangle(375, 200, 50, 50), new Rectangle(0, 0, Player.SpriteSheetWidth, Player.SpriteSheetHeight), Color.White);
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
            List<Score> highscore = Highscore.GetScore();
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
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Score: " + WumpusGame.Player.calculateScore().ToString(), new Vector2(350, 0), Color.White);
        }

        public static void DrawMap() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.MapTexture, new Rectangle(0, 0, 800, 600), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Current Room: " + WumpusGame.Player.CurrentRoom.RoomNumber.ToString(), new Vector2(0, 0), Color.White);

            Room[] roomArray = Cave.Rooms;
            for (int y = 0; y < Cave.numRows; y++)
            {
                for (int x = 0; x < Cave.numColumns; x++)
                {
                    if (x % 2 == 0)
                    {
                        //Increase the number by one since the rooms are stored as zero-indexed
                        WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, (roomArray[x + Cave.numRows * y].RoomNumber + 1).ToString(), new Vector2(200 + 75 * x , 87 + 88 * y), Color.White);
                    }
                    else
                    {
                        WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, (roomArray[x + Cave.numRows * y].RoomNumber + 1).ToString(), new Vector2(200 + 75 * x, 120 + 88 * y), Color.White);
                    }
                }
            }
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Press left to go back", new Vector2(0, 560), Color.White);
        }
        public static void DrawMenu() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.WallpaperTexture, new Rectangle(0, 0, WumpusGame.ScreenWidth, WumpusGame.ScreenHeight), Color.White);

            DrawRectangleOutline(Menu.NewGameRectangle, WumpusGame.BlackTexture, 1);
            DrawRectangleOutline(Menu.HighScoreRectangle, WumpusGame.BlackTexture, 1);
            DrawRectangleOutline(Menu.HelpRectangle, WumpusGame.BlackTexture, 1);
            DrawRectangleOutline(Menu.ExitRectangle, WumpusGame.BlackTexture, 1);
            

            //use WumpusGame.MotorwerkFont.MeasureString() to measure the string and place in middle of rectangle
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "New Game", new Vector2(Menu.NewGameRectangle.X + (Menu.NewGameRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("New Game").X) / 2,
                                    Menu.NewGameRectangle.Y + (Menu.NewGameRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("New Game").Y) / 2), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "High Scores", new Vector2(Menu.HighScoreRectangle.X + (Menu.HighScoreRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("High Scores").X) / 2,
                                    Menu.HighScoreRectangle.Y + (Menu.HighScoreRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("High Scores").Y) / 2), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Help", new Vector2(Menu.HelpRectangle.X + (Menu.HelpRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("Help").X) / 2,
                                    Menu.HelpRectangle.Y + (Menu.HelpRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("Help").Y) / 2), Color.Black);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Exit", new Vector2(Menu.ExitRectangle.X + (Menu.ExitRectangle.Width - WumpusGame.MotorwerkFont.MeasureString("Exit").X) / 2,
                                    Menu.ExitRectangle.Y + (Menu.ExitRectangle.Height - WumpusGame.MotorwerkFont.MeasureString("Exit").Y) / 2), Color.Black);
        }

        public static void DrawShop() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.ShopBackgroundTexture, new Rectangle(0, 0, WumpusGame.ScreenWidth, WumpusGame.ScreenHeight), Color.White);
            WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Press left to go back", new Vector2(0, 560), Color.White);
        }

        public static void DrawTrivia() 
        {
            WumpusGame.SpriteBatch.Draw(WumpusGame.ShopBackgroundTexture, new Rectangle(0, 0, WumpusGame.ScreenWidth, WumpusGame.ScreenHeight), Color.White);
            try
            {
                string question = UpdateStates.CurrentTrivia.Question;
                string answer1 = "A: " + UpdateStates.CurrentTrivia.Answers[0];
                string answer2 = "B: " + UpdateStates.CurrentTrivia.Answers[1];
                string answer3 = "C: " + UpdateStates.CurrentTrivia.Answers[2];
                string answer4 = "D: " + UpdateStates.CurrentTrivia.Answers[3];
                WumpusGame.SpriteBatch.DrawString(WumpusGame.SmallMotorwerkFont, question, new Vector2((WumpusGame.ScreenWidth - WumpusGame.SmallMotorwerkFont.MeasureString(question).X) / 2, (WumpusGame.ScreenHeight - WumpusGame.SmallMotorwerkFont.MeasureString(question).Y) / 12), Color.White);
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer1, new Vector2((WumpusGame.ScreenWidth - WumpusGame.MotorwerkFont.MeasureString(answer1).X) / 2, (WumpusGame.ScreenHeight - WumpusGame.MotorwerkFont.MeasureString(answer1).Y) * 4 / 16 + 30), Color.White);
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer2, new Vector2((WumpusGame.ScreenWidth - WumpusGame.MotorwerkFont.MeasureString(answer2).X) / 2, (WumpusGame.ScreenHeight - WumpusGame.MotorwerkFont.MeasureString(answer2).Y) * 6 / 16 + 30), Color.White);
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer3, new Vector2((WumpusGame.ScreenWidth - WumpusGame.MotorwerkFont.MeasureString(answer3).X) / 2, (WumpusGame.ScreenHeight - WumpusGame.MotorwerkFont.MeasureString(answer3).Y) * 8 / 16 + 30), Color.White);
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, answer4, new Vector2((WumpusGame.ScreenWidth - WumpusGame.MotorwerkFont.MeasureString(answer4).X) / 2, (WumpusGame.ScreenHeight - WumpusGame.MotorwerkFont.MeasureString(answer4).Y) * 10 / 16 + 30), Color.White);
            }
            catch {}

            if (WumpusGame.TriviaState == TriviaState.Correct)
            {
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Correct", new Vector2((WumpusGame.ScreenWidth - WumpusGame.MotorwerkFont.MeasureString("Correct").X) / 2, 560), Color.White);
            }
            if (WumpusGame.TriviaState == TriviaState.Incorrect)
            {
                WumpusGame.SpriteBatch.DrawString(WumpusGame.MotorwerkFont, "Incorrect", new Vector2((WumpusGame.ScreenWidth - WumpusGame.MotorwerkFont.MeasureString("Incorrect").X) / 2, 560), Color.White);
            }
        }

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
