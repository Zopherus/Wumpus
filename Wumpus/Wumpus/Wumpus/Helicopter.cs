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
    static class Helicopter
    {
        private static Rectangle position  = new Rectangle(310, 120, 200, 200);
        public static int Counter { get; private set; }

        public static Rectangle SourceRectangle
        {
            get
            { 
                // The SourceRectangle will be called every frame where relevant
                Counter++;
                return new Rectangle(140 * ((int)(Counter/3)%3), 0, 140, 165); 
            }
        }

        public static Rectangle Position
        {
            get { return position; }
        }
    }
}
