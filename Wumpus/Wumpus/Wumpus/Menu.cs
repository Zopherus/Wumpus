using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Wumpus
{
    //The rectangles for the buttons on the menu screen
    class Menu
    {
        private static Rectangle newGameRectangle = new Rectangle(75, 325, WumpusGame.ScreenWidth / 3, WumpusGame.ScreenHeight / 6);
        private static Rectangle highScoreRectangle = new Rectangle(450, 325, WumpusGame.ScreenWidth / 3, WumpusGame.ScreenHeight / 6);
        private static Rectangle helpRectangle = new Rectangle(75, 450, WumpusGame.ScreenWidth / 3, WumpusGame.ScreenHeight / 6);
        private static Rectangle exitRectangle = new Rectangle(450, 450, WumpusGame.ScreenWidth / 3, WumpusGame.ScreenHeight / 6);

        public static Rectangle NewGameRectangle 
        {
            get { return newGameRectangle; }
        }

        public static Rectangle HighScoreRectangle
        {
            get { return highScoreRectangle; }
        }

        public static Rectangle HelpRectangle
        {
            get { return helpRectangle; }
        }

        public static Rectangle ExitRectangle
        {
            get { return exitRectangle; }
        }
    }
}
