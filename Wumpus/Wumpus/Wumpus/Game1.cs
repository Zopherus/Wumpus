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
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	///

	public class Game1 : Microsoft.Xna.Framework.Game
	{
        const int spriteSheetHeight = 32;
        const int spriteSheetWidth = 32;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string[] strings;

        bool value = false;
        int time = 0;
        int triviaWinCounter = 0;

        public static Texture2D hexagonTexture;
        public static Texture2D blackTexture;
        public static Texture2D whiteTexture;
        public static Texture2D moneyCurrencyTexture;
        public static Texture2D bushTexture;
        public static Texture2D osamaTexture;
        public static Texture2D groundTexture;
        public static Texture2D helicopterTexture;
        public static Texture2D wallpaperTexture;
        public static Texture2D bottomHUDTexture;
        public static Texture2D oilSpillTexture;
        public static Texture2D shopBackgroundTexture;
        public static Texture2D bulletsTexture;
        public static Texture2D mapTexture;
        public static Texture2D deathTexture;
        List<Texture2D> textures = new List<Texture2D>();

        KeyboardState keyboard;
        KeyboardState oldKeyboard;
        MouseState oldMouse;
        MouseState mouse;

        Point mousePoint;
        SpriteFont spritefont;
        SpriteFont smallspritefont;

        Rectangle newGameRectangle;
        Rectangle highScoreRectangle;
        Rectangle helpRectangle;
        Rectangle exitRectangle;

        const int lineWidth = 1;
        public const int screenHeight = 600;
        public const int screenWidth = 800;
        const int hexagonSize = 30; 

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
            GraphicsDevice graphicsDevice = this.GraphicsDevice;
			//graphics.IsFullScreen = true;
			graphics.PreferredBackBufferHeight = screenHeight;
			graphics.PreferredBackBufferWidth = screenWidth;
			Content.RootDirectory = "Content";
			this.IsMouseVisible = true;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		/// 

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

            //Cave.GenerateCave();
            
            //Menu Rectangles
            newGameRectangle = new Rectangle(75, 325, screenWidth / 3, screenHeight / 6);
            highScoreRectangle = new Rectangle(450, 325, screenWidth / 3, screenHeight / 6);
            helpRectangle = new Rectangle(75, 450, screenWidth / 3, screenHeight / 6);
            exitRectangle = new Rectangle(450, 450, screenWidth / 3, screenHeight / 6);

            GameControl.Initialize();
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			hexagonTexture = Content.Load<Texture2D>("Sprites/MapHexagon.fw");
			spritefont = Content.Load<SpriteFont>("Spritefonts/SpriteFont1");
            smallspritefont = Content.Load<SpriteFont>("Spritefonts/SmallSpriteFont");
			blackTexture = Content.Load<Texture2D>("Sprites/black.fw");
            whiteTexture = Content.Load<Texture2D>("Sprites/White");
			moneyCurrencyTexture = Content.Load<Texture2D>("Sprites/MoneyCurrency");
			bushTexture = Content.Load<Texture2D>("Sprites/Bush");
			osamaTexture = Content.Load<Texture2D>("Sprites/Osama");
			groundTexture = Content.Load<Texture2D>("Sprites/ground");
            wallpaperTexture = Content.Load<Texture2D>("Sprites/Wallpaper");
			helicopterTexture = Content.Load<Texture2D>("Sprites/Helicopter");
            bottomHUDTexture = Content.Load<Texture2D>("Sprites/HUD");
            shopBackgroundTexture = Content.Load<Texture2D>("Sprites/ShopBackground");
            oilSpillTexture = Content.Load<Texture2D>("Sprites/OilSpill");
            bulletsTexture = Content.Load<Texture2D>("Sprites/Bullets");
            mapTexture = Content.Load<Texture2D>("Sprites/Map");
            deathTexture = Content.Load<Texture2D>("Sprites/death");
            textures.Add(Content.Load<Texture2D>("Sprites/untitled"));
            textures.Add(Content.Load<Texture2D>("Sprites/untitled1"));
            textures.Add(Content.Load<Texture2D>("Sprites/untitled2"));
            textures.Add(Content.Load<Texture2D>("Sprites/untitled3"));
            textures.Add(Content.Load<Texture2D>("Sprites/untitled4"));
            textures.Add(Content.Load<Texture2D>("Sprites/untitled5"));
            textures.Add(Content.Load<Texture2D>("Sprites/untitled6"));
            
			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			keyboard = Keyboard.GetState();
			mouse = Mouse.GetState();
			mousePoint = new Point(mouse.X, mouse.Y);
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();
			if (keyboard.IsKeyDown(Keys.Tab) && oldKeyboard.IsKeyUp(Keys.Tab))
				graphics.ToggleFullScreen();
			// TODO: Add your update logic here
            if (GameControl.menu)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    //new game
                    if (newGameRectangle.Contains(mousePoint))
                    {
                        GameControl.resetGameState();
                        GameControl.cave = true;
                        GameControl.bat = true;
                    }
                    //high scores
                    if (highScoreRectangle.Contains(mousePoint))
                    {
                        GameControl.resetGameState();
                        GameControl.highscore = true;
                    }
                    if (helpRectangle.Contains(mousePoint))
                    {
                        GameControl.resetGameState();
                        GameControl.help = true;
                    }
                    //exit
                    if (exitRectangle.Contains(mousePoint))
                    { this.Exit(); }
                }
            }
			if (GameControl.cave)
			{
				if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
				{
					if (mouse.X > 40 && mouse.X < 190 && mouse.Y > 475 && mouse.Y < 565)
					{
                        GameControl.resetGameState();
                        GameControl.map = true;
					}
				}

				bool isMoving = false;
				if (keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D))
				{
					GameControl.Player.Position = new Rectangle(GameControl.Player.Position.X + 5, GameControl.Player.Position.Y, GameControl.Player.Position.Width, GameControl.Player.Position.Height);
					GameControl.Player.Direction = 2;
					isMoving = true;
				}
				if (keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W))
				{
					GameControl.Player.Position = new Rectangle(GameControl.Player.Position.X, GameControl.Player.Position.Y - 5, GameControl.Player.Position.Width, GameControl.Player.Position.Height);
					GameControl.Player.Direction = 3;
					isMoving = true;
				}
				if (keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A))
				{
					GameControl.Player.Position = new Rectangle(GameControl.Player.Position.X - 5, GameControl.Player.Position.Y, GameControl.Player.Position.Width, GameControl.Player.Position.Height);
					GameControl.Player.Direction = 1;
					isMoving = true;
				}
				if (keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S))
				{
					GameControl.Player.Position = new Rectangle(GameControl.Player.Position.X, GameControl.Player.Position.Y + 5, GameControl.Player.Position.Width, GameControl.Player.Position.Height);
					GameControl.Player.Direction = 0;
					isMoving = true;
				}
				if (isMoving)
					GameControl.Player.Counter++;
			}
			if (GameControl.trivia && !GameControl.triviaWin && !GameControl.triviaLose)
			{
				if (keyboard.IsKeyDown(Keys.A))
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
				if (keyboard.IsKeyDown(Keys.B))
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
				if (keyboard.IsKeyDown(Keys.C))
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
				if (keyboard.IsKeyDown(Keys.D))
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
            if (GameControl.triviaLose || GameControl.triviaWin)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    GameControl.resetGameState();
                    GameControl.cave = true;
                }
            }
            if (GameControl.help)
            {
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    GameControl.resetGameState();
                    GameControl.menu = true;
                }
            }
            if (GameControl.highscore)
            {
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    GameControl.resetGameState();
                    GameControl.menu = true;
                }
            }
            if (GameControl.shop)
            {
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    GameControl.resetGameState();
                    GameControl.cave = true;
                }
            }
            if (GameControl.map)
            {
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    GameControl.resetGameState();
                    GameControl.cave = true;
                }
            }
            if (GameControl.lose)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed)
                {
                    GameControl.resetGameState();
                    GameControl.menu = true;
                }
            }
			oldKeyboard = Keyboard.GetState();
			oldMouse = Mouse.GetState();
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
            spriteBatch.Begin();
			GraphicsDevice.Clear(Color.White);
			if (GameControl.trivia)
			{
                spriteBatch.Draw(shopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
				string question = strings[0];
				string answer1 = "A: " + strings[1];
				string answer2 = "B: " + strings[2];
				string answer3 = "C: " + strings[3];
                string answer4 = "D: " + strings[4];
                int correctAnswer = int.Parse(strings[5]);
				spriteBatch.DrawString(smallspritefont, question ,new Vector2((screenWidth-smallspritefont.MeasureString(question).X)/2,(screenHeight-smallspritefont.MeasureString(question).Y)/12),Color.White);
				spriteBatch.DrawString(spritefont, answer1, new Vector2((screenWidth - spritefont.MeasureString(answer1).X) / 2, (screenHeight - spritefont.MeasureString(answer1).Y) * 4 / 16 + 30), Color.White);
				spriteBatch.DrawString(spritefont, answer2, new Vector2((screenWidth - spritefont.MeasureString(answer2).X) / 2, (screenHeight - spritefont.MeasureString(answer2).Y) * 6 / 16 + 30), Color.White);
				spriteBatch.DrawString(spritefont, answer3, new Vector2((screenWidth - spritefont.MeasureString(answer3).X) / 2, (screenHeight - spritefont.MeasureString(answer3).Y) * 8 / 16 + 30), Color.White);
				spriteBatch.DrawString(spritefont, answer4, new Vector2((screenWidth - spritefont.MeasureString(answer4).X) / 2, (screenHeight - spritefont.MeasureString(answer4).Y) *10 / 16 + 30), Color.White);
                if (GameControl.triviaWin)
                {
                    spriteBatch.DrawString(spritefont, "Correct", new Vector2((screenWidth-spritefont.MeasureString("Correct").X) /2, 560),Color.White);
                }
                if (GameControl.triviaLose)
                {
                    spriteBatch.DrawString(spritefont, "Incorrect", new Vector2((screenWidth - spritefont.MeasureString("Incorrect").X) / 2, 560), Color.White);
                }
			}
			if (GameControl.cave)
			{
                spriteBatch.DrawString(spritefont, new Point(GameControl.Player.Position.X, GameControl.Player.Position.Y).ToString(), new Vector2(0, 0), Color.Black);
                spriteBatch.Draw(bottomHUDTexture, new Rectangle(0, 450, 800, 150), Color.White);
                spriteBatch.Draw(groundTexture, new Rectangle(0, 0, 800, 450), Color.White);
                spriteBatch.DrawString(spritefont, GameControl.DisplayHazards(), new Vector2(0, 420), Color.White);
                spriteBatch.DrawString(spritefont, GameControl.Player.Gold.ToString(), new Vector2(660 - spritefont.MeasureString("0").X * (int)Math.Log10((double)GameControl.Player.Gold+1), 465), Color.Black);
                spriteBatch.DrawString(spritefont, GameControl.Player.Arrows.ToString(), new Vector2(660 - spritefont.MeasureString("0").X * (int)Math.Log10((double)GameControl.Player.Arrows+1), 525), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(0, 450, 800, 1), Color.White);
				spriteBatch.Draw(bushTexture, GameControl.Player.Position, new Rectangle(32 * GameControl.Player.CounterHolder, 32 * GameControl.Player.Direction, spriteSheetWidth, spriteSheetHeight), Color.White);
				spriteBatch.Draw(moneyCurrencyTexture, new Rectangle(675, 445, 100, 100), Color.White);
                spriteBatch.Draw(bulletsTexture, new Rectangle(675, 500, 100, 100), new Rectangle(0, 0, 45, 41), Color.White);
                int[] AdjRooms = Cave.Matrix[GameControl.Player.CurrentRoom-1].ConnectedRooms;
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
                        spriteBatch.Draw(textures[i], new Rectangle(0, 0, 800, 450), Color.White);
                }
				//map button
				spriteBatch.Draw(blackTexture, new Rectangle(40, 475, 150, 1), Color.Black);
				spriteBatch.Draw(blackTexture, new Rectangle(190, 475, 1, 90), Color.Black);
				spriteBatch.Draw(blackTexture, new Rectangle(40, 475, 1, 90), Color.Black);
				spriteBatch.Draw(blackTexture, new Rectangle(40, 565, 151, 1), Color.Black);
				spriteBatch.DrawString(spritefont, "Map", new Vector2(115 - (spritefont.MeasureString("Map")).X / 2, 520 - (spritefont.MeasureString("Map")).Y / 2), Color.Black);

				if (GameControl.GameMap.Helicopter1 == GameControl.Player.CurrentRoom || GameControl.GameMap.Helicopter2 == GameControl.Player.CurrentRoom)
				{
                    if (!value)
                    {
                        value = true;
                        time = (int)gameTime.TotalGameTime.Seconds;
                    }
                    spriteBatch.Draw(helicopterTexture,GameControl.helicopter.Position , GameControl.helicopter.SourceRectangle, Color.White);
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
                    spriteBatch.Draw(osamaTexture, new Rectangle(375, 200, 50,50), new Rectangle(0,0,spriteSheetWidth,spriteSheetHeight), Color.White);
                    if (gameTime.TotalGameTime.Seconds - time > 3)
                    {
                        int counter = 0;
                        if (counter < 5 && triviaWinCounter < 3 && counter - triviaWinCounter < 3)
                        {
                            strings = GameControl.newTrivia();
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
			if (GameControl.menu)
			{
                //50 gap in between each rectangle
                //adding 1 to fill in the corner of rectangle
                spriteBatch.Draw(wallpaperTexture,new Rectangle(0,0,800,600),Color.White);

                //New Game Rectangle
                spriteBatch.Draw(blackTexture, new Rectangle(newGameRectangle.X, newGameRectangle.Y, newGameRectangle.Width, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(newGameRectangle.X + newGameRectangle.Width, newGameRectangle.Y, lineWidth, newGameRectangle.Height), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(newGameRectangle.X, newGameRectangle.Y + newGameRectangle.Height, newGameRectangle.Width + 1, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(newGameRectangle.X, newGameRectangle.Y, lineWidth, newGameRectangle.Height), Color.Black);

                //High Score Rectangle
                spriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X, highScoreRectangle.Y, highScoreRectangle.Width, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X + highScoreRectangle.Width, highScoreRectangle.Y, lineWidth, highScoreRectangle.Height), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X, highScoreRectangle.Y + highScoreRectangle.Height, highScoreRectangle.Width + 1, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(highScoreRectangle.X, highScoreRectangle.Y, lineWidth, highScoreRectangle.Height), Color.Black);

                //Help Rectangle
                spriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X, helpRectangle.Y, helpRectangle.Width, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X + helpRectangle.Width, helpRectangle.Y, lineWidth, helpRectangle.Height), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X, helpRectangle.Y + helpRectangle.Height, helpRectangle.Width + 1, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(helpRectangle.X, helpRectangle.Y, lineWidth, helpRectangle.Height), Color.Black);

                //Exit Rectangle
                spriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X, exitRectangle.Y, exitRectangle.Width, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X + exitRectangle.Width, exitRectangle.Y, lineWidth, exitRectangle.Height), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X, exitRectangle.Y + exitRectangle.Height, exitRectangle.Width + 1, lineWidth), Color.Black);
                spriteBatch.Draw(blackTexture, new Rectangle(exitRectangle.X, exitRectangle.Y, lineWidth, exitRectangle.Height), Color.Black);

                //use spritefont.MeasureString() to measure the string and place in middle of rectangle
                spriteBatch.DrawString(spritefont, "New Game", new Vector2(newGameRectangle.X + (newGameRectangle.Width - spritefont.MeasureString("New Game").X) / 2,
                                        newGameRectangle.Y + (newGameRectangle.Height - spritefont.MeasureString("New Game").Y) / 2), Color.Black);
                spriteBatch.DrawString(spritefont, "High Scores", new Vector2(highScoreRectangle.X + (highScoreRectangle.Width - spritefont.MeasureString("High Scores").X) / 2,
                                        highScoreRectangle.Y + (highScoreRectangle.Height - spritefont.MeasureString("High Scores").Y) / 2), Color.Black);
                spriteBatch.DrawString(spritefont, "Help", new Vector2(helpRectangle.X + (helpRectangle.Width - spritefont.MeasureString("Help").X) / 2,
                                        helpRectangle.Y + (helpRectangle.Height - spritefont.MeasureString("Help").Y) / 2), Color.Black);
                spriteBatch.DrawString(spritefont, "Exit", new Vector2(exitRectangle.X + (exitRectangle.Width - spritefont.MeasureString("Exit").X) / 2,
                                        exitRectangle.Y + (exitRectangle.Height - spritefont.MeasureString("Exit").Y) / 2), Color.Black);
			}
			if (GameControl.map)
			{
                spriteBatch.Draw(mapTexture, new Rectangle(0, 0, 800, 600), Color.White);
                spriteBatch.DrawString(spritefont, "Current Room: " + GameControl.Player.CurrentRoom.ToString(), new Vector2(0, 0), Color.White);
                int[][] array = GameControl.GameCave.getMap();
                for (int i = 0; i < 5; i++)
                {
                    foreach (int number in array[i])
                    {
                        if (number % 2 == 1)
                        {
                            spriteBatch.DrawString(spritefont, number.ToString(), new Vector2(200 + 75 * ((number-1) % 6) , 87 + 88 * i), Color.White);
                        }
                        else
                        {
                            spriteBatch.DrawString(spritefont, number.ToString(), new Vector2(200 + 75 * ((number-1) % 6), 120 + 88 * i), Color.White);
                        }
                    }
                }
                spriteBatch.DrawString(spritefont, "Press left to go back", new Vector2(0, 560), Color.White);
			}
            if (GameControl.highscore)
            {
                spriteBatch.Draw(shopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
                List<StoreScore> highscore = Highscore.GetScore();
                spriteBatch.DrawString(spritefont, "High Scores: ", new Vector2(0, 0), Color.White);
                int number = (highscore.Count > 10) ? 10 : highscore.Count;
                for (int i = 0; i < number; i++)
                {
                    spriteBatch.DrawString(spritefont, (i+1).ToString() + ". " + highscore[i].ToString(), new Vector2(0, 50*i + 50), Color.White);
                }
                spriteBatch.DrawString(spritefont, "Press left to go back to the menu", new Vector2(0, 560), Color.White);
            }
			if (GameControl.shop)
			{
                spriteBatch.Draw(shopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
                spriteBatch.DrawString(spritefont, "Press left to go back", new Vector2(0, 560), Color.White);
			}
            if (GameControl.help)
            {
                spriteBatch.Draw(shopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
                spriteBatch.DrawString(spritefont, "Help: ", new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(spritefont, "The goal of this game is to kill the wumpus", new Vector2(0, 20), Color.White);
                spriteBatch.DrawString(spritefont, "Find the wumpus in the cave and answer", new Vector2(0, 40), Color.White);
                spriteBatch.DrawString(spritefont, "trivia questions to defeat him", new Vector2(0, 60), Color.White);
                spriteBatch.DrawString(spritefont, "Use the arrow keys to move around" , new Vector2(0,80), Color.White);
                spriteBatch.DrawString(spritefont, "Watch out for the Helicopter that will ", new Vector2(0, 100), Color.White);
                spriteBatch.DrawString(spritefont, "move you to a random room", new Vector2(0, 1205), Color.White);

                spriteBatch.DrawString(spritefont, "Press left to go back to the menu", new Vector2(0, 560), Color.White);
            }
            if (GameControl.lose)
            {
                spriteBatch.Draw(shopBackgroundTexture, new Rectangle(0, 0, 800, 600), Color.White);
                spriteBatch.DrawString(spritefont, "Lose", new Vector2(400, 0), Color.White);
                spriteBatch.DrawString(spritefont, "Score: " + GameControl.Player.calculateScore().ToString(), new Vector2(350, 0), Color.White);
            }
            spriteBatch.End();
			// TODO: Add your drawing code here
			base.Draw(gameTime);
		}
	}
}
