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
	public class Hexagon
	{
		public int number;
		public int width;
		public int left;
		public int top;
		public int height;
        //Binary form of this number describes which doors are open
        //Top door is 1 and rotates clockwise
        public int doors;
        //Number of doors
        public int numberDoors;
		public Hexagon() { }
		public Hexagon(int n)
		{
			number = n;
		}
		public Hexagon( int num, int lef, int to, int wid, int heigh)
		{
			number = num;
			width = wid;
			left = lef;
			top = to;
			height = heigh;
		}

        public static int DoorTop(int hexagonNumber)
        {
			if (hexagonNumber < 7)
            {
                return hexagonNumber + 24;
            }
            else
            {
                return hexagonNumber - 6;
            }
        }

        public static int DoorTopRight(int hexagonNumber)
        {
            if (hexagonNumber % 2 == 1)
            {
                if (hexagonNumber < 6)
                {
                    return hexagonNumber + 25;
                }
                else
                {
                    return hexagonNumber - 5;
                }
            }
            else
            {
                if (hexagonNumber % 6 == 0)
                {
                    return hexagonNumber - 5;
                }
                else
                {
                    return hexagonNumber + 1;
                }
            }
        }

        public static int DoorBottomRight(int hexagonNumber)
        {
            if (hexagonNumber % 2 == 1)
            {
                return hexagonNumber + 1;
            }
            else
            {
                if (hexagonNumber == 30)
                {
                    return 1;
                }
                else
                    if (hexagonNumber > 25)
                    {
                        return hexagonNumber - 23;
                    }
                    else
                        if (hexagonNumber % 6 == 0)
                        {
                            return hexagonNumber + 1;
                        }
                        else
                        {
                            return hexagonNumber + 7;
                        }
            }
        }

        public static int DoorBottom(int hexagonNumber)
        {
            if (hexagonNumber > 24)
            {
                return hexagonNumber - 24;
            }
            else
            {
                return hexagonNumber + 6;
            }
        }

        public static int DoorBottomLeft(int hexagonNumber)
        {
            if (hexagonNumber % 2 == 1)
            {
                if (hexagonNumber % 6 == 1)
                {
                    return hexagonNumber + 5;
                }
                else
                {
                    return hexagonNumber - 1;
                }
            }
            else
            {
                if (hexagonNumber > 25)
                {
                    return hexagonNumber - 25;
                }
                else
                {
                    return hexagonNumber + 5;
                }
            }
        }

        public static int DoorTopLeft(int hexagonNumber)
        {
            if (hexagonNumber % 2 == 1)
            {
                if (hexagonNumber == 1)
                {
                    return 30;
                }
                else
                    if (hexagonNumber < 6)
                    {
                       return hexagonNumber + 23;
                    }
                    else
                        if (hexagonNumber % 6 == 1)
                        {
                            return hexagonNumber - 1;
                        }
                        else
                        {
                            return hexagonNumber - 7;
                        }
            }
            else
            {
                return hexagonNumber - 1;
            }
        }
	}
}
