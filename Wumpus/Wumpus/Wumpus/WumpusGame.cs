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
    public enum GameState { Cave, Help, Highscore, Lose, Map, Menu, Shop, Trivia, TriviaLose, TriviaWin };
    public enum Direction { Down, Left, Right, Up };
	public enum TriviaState { NotAnswered, Correct, Incorrect };

	public class WumpusGame : Game
	{   
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        
		
        public static GameState GameState;
		//Only used when in the Trivia GameState
		public static TriviaState TriviaState;

        public static Texture2D BlackTexture { get; private set;  }
        public static Texture2D WhiteTexture { get; private set;  }
        public static Texture2D MoneyCurrencyTexture { get; private set;  }
        public static Texture2D BushTexture { get; private set;  }
        public static Texture2D OsamaTexture { get; private set;  }
        public static Texture2D GroundTexture { get; private set;  }
        public static Texture2D HelicopterTexture { get; private set;  }
        public static Texture2D WallpaperTexture { get; private set;  }
        public static Texture2D BottomHUDTexture { get; private set;  }
        public static Texture2D OilSpillTexture { get; private set;  }
        public static Texture2D ShopBackgroundTexture { get; private set;  }
        public static Texture2D BulletsTexture { get; private set;  }
        public static Texture2D MapTexture { get; private set;  }
        public static Texture2D DeathTexture { get; private set;  }
        public static Texture2D DefaultRoomBoundary { get; private set; }
        public static List<Texture2D> TreeBoundaryTextures = new List<Texture2D>();

        public static KeyboardState KeyboardState { get; private set; }
        public static KeyboardState OldKeyboardState { get; private set; }
        public static MouseState OldMouseState { get; private set; }
        public static MouseState MouseState { get; private set; }

        public static SpriteFont MotorwerkFont { get; private set; }
        public static SpriteFont SmallMotorwerkFont { get; private set; }

		public static Player Player = new Player();



        public const int lineWidth = 1;
        public const int ScreenHeight = 600;
        public const int ScreenWidth = 800;
        public const int hexagonSize = 30;

		public WumpusGame()
		{
			Graphics = new GraphicsDeviceManager(this);
            GraphicsDevice graphicsDevice = this.GraphicsDevice;
			Graphics.PreferredBackBufferHeight = ScreenHeight;
			Graphics.PreferredBackBufferWidth = ScreenWidth;
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

            Cave.InitializeMap();
            Map.InitializeMap();
            TriviaList.InitializeTriviaList();
            GameState = GameState.Menu;
            Player = new Player(new Rectangle(375, 180, Player.rectangleSize, Player.rectangleSize), 3, 0);
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			MotorwerkFont = Content.Load<SpriteFont>("Spritefonts/MotorwerkFont");
            SmallMotorwerkFont = Content.Load<SpriteFont>("Spritefonts/SmallMotorwerkFont");
			BlackTexture = Content.Load<Texture2D>("Sprites/Black");
            WhiteTexture = Content.Load<Texture2D>("Sprites/White");
			MoneyCurrencyTexture = Content.Load<Texture2D>("Sprites/MoneyCurrency");
			BushTexture = Content.Load<Texture2D>("Sprites/Bush");
			OsamaTexture = Content.Load<Texture2D>("Sprites/Osama");
			GroundTexture = Content.Load<Texture2D>("Sprites/Ground");
            WallpaperTexture = Content.Load<Texture2D>("Sprites/Wallpaper");
			HelicopterTexture = Content.Load<Texture2D>("Sprites/Helicopter");
            BottomHUDTexture = Content.Load<Texture2D>("Sprites/HUD");
            ShopBackgroundTexture = Content.Load<Texture2D>("Sprites/ShopBackground");
            OilSpillTexture = Content.Load<Texture2D>("Sprites/OilSpill");
            BulletsTexture = Content.Load<Texture2D>("Sprites/Bullets");
            MapTexture = Content.Load<Texture2D>("Sprites/Map");
            DeathTexture = Content.Load<Texture2D>("Sprites/Death");
            DefaultRoomBoundary = Content.Load<Texture2D>("Sprites/HexagonTreeBoundaries/DefaultBoundary");
            TreeBoundaryTextures.Add(Content.Load<Texture2D>("Sprites/HexagonTreeBoundaries/TopBoundary"));
            TreeBoundaryTextures.Add(Content.Load<Texture2D>("Sprites/HexagonTreeBoundaries/TopRightBoundary"));
            TreeBoundaryTextures.Add(Content.Load<Texture2D>("Sprites/HexagonTreeBoundaries/BottomRightBoundary"));
            TreeBoundaryTextures.Add(Content.Load<Texture2D>("Sprites/HexagonTreeBoundaries/BottomBoundary"));
            TreeBoundaryTextures.Add(Content.Load<Texture2D>("Sprites/HexagonTreeBoundaries/BottomLeftBoundary"));
            TreeBoundaryTextures.Add(Content.Load<Texture2D>("Sprites/HexagonTreeBoundaries/TopLeftBoundary"));
            
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
            OldKeyboardState = KeyboardState;
            OldMouseState = MouseState;
			KeyboardState = Keyboard.GetState();
			MouseState = Mouse.GetState();
            if (KeyboardState.IsKeyDown(Keys.Escape))
                this.Exit();
			if (KeyboardState.IsKeyDown(Keys.Tab) && OldKeyboardState.IsKeyUp(Keys.Tab))
				Graphics.ToggleFullScreen();
            //Run the according update method depending on the GameState
            switch(GameState)
            {
                case GameState.Cave:
                    UpdateStates.UpdateCave(gameTime);
                    break;
                case GameState.Help:
                    UpdateStates.UpdateHelp();
                    break;
                case GameState.Highscore:
                    UpdateStates.UpdateHighscore();
                    break;
                case GameState.Lose:
                    UpdateStates.UpdateLose();
                    break;
                case GameState.Map:
                    UpdateStates.UpdateMap();
                    break;
                case GameState.Menu:
                    UpdateStates.UpdateMenu();
                    break;
                case GameState.Shop:
                    UpdateStates.UpdateShop();
                    break;
                case GameState.Trivia:
                    UpdateStates.UpdateTrivia();
                    break;
            }
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
            SpriteBatch.Begin();
			GraphicsDevice.Clear(Color.White);
            //Run the according update method depending on the GameState
            switch (GameState)
            {
                case GameState.Cave:
                    DrawStates.DrawCave();
                    break;
                case GameState.Help:
                    DrawStates.DrawHelp();
                    break;
                case GameState.Highscore:
                    DrawStates.DrawHighscore();
                    break;
                case GameState.Lose:
                    DrawStates.DrawLose();
                    break;
                case GameState.Map:
                    DrawStates.DrawMap();
                    break;
                case GameState.Menu:
                    DrawStates.DrawMenu();
                    break;
                case GameState.Shop:
                    DrawStates.DrawShop();
                    break;
                case GameState.Trivia:
                    DrawStates.DrawTrivia();
                    break;
            }
            SpriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
