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
    class Helicopter
    {
        private Rectangle position;
        private int counter;

        public Helicopter() { }

        public Helicopter(Rectangle position)
        {
            this.position = position;
        }

        public Rectangle SourceRectangle
        {
            get { return new Rectangle(140 * ((int)(counter/3)%3), 0, 140, 165); }
        } 

        public Rectangle Position
        {
            get 
            { 
                counter++;
                return position;
            }
        }

        public int Counter
        {
            get { return counter; }
        }
    }
}
