using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Wumpus
{
	class Cave
	{
        private static Hexagon[] hexagons = new Hexagon[30];
        private static bool[] fill = new bool[30];
        private static void FloodFill(int hexagonNumber)
        {
            if (fill[hexagonNumber])
                return;
            else
            {
                fill[hexagonNumber] = true;
                string binary = Convert.ToString(hexagons[hexagonNumber].doors, 2);
                int binaryLength = binary.Length;
                for (int counter = 0; counter < 6 - binaryLength; counter++)
                {
                    binary = "0" + binary;
                }
                for (int counter = 0; counter < 6; counter++)
                {
                    if (binary[counter] == '1')
                    {
                        switch (5 - counter)
                        {
                            case 0:
                                FloodFill(hexagons[hexagonNumber].DoorTop());
                                break;
                            case 1:
                                FloodFill(hexagons[hexagonNumber].DoorTopRight());
                                break;
                            case 2:
                                FloodFill(hexagons[hexagonNumber].DoorBottomRight());
                                break;
                            case 3:
                                FloodFill(hexagons[hexagonNumber].DoorBottom());
                                break;
                            case 4:
                                FloodFill(hexagons[hexagonNumber].DoorBottomLeft());
                                break;
                            case 5:
                                FloodFill(hexagons[hexagonNumber].DoorTopLeft());
                                break;
                        }
                    }
                }
            }
        }

        private static void CreateCave()
        {
            Random rnd = new Random();
            foreach (Hexagon hexagon in hexagons)
            {
                string binary = Convert.ToString(hexagon.doors, 2);
                int binaryLength = binary.Length;
                for (int counter = 0; counter < 6 - binaryLength; counter++)
                {
                    binary = "0" + binary;
                }
                if (hexagon.numberDoors >= 3)
                    continue;
                int numberDoors = 0;
                if (hexagon.numberDoors == 0)
                { numberDoors = rnd.Next(1, 4); }
                else
                { numberDoors = rnd.Next(0, 4 - hexagon.numberDoors); }
                for (int counter = 0; counter < numberDoors; counter++)
                {
                    int door = rnd.Next(0, 6);
                    if (binary[5 - door] == '0')
                    {
                        hexagon.doors += (int)Math.Pow(2, door);
                        hexagon.numberDoors++;
                        switch (door)
                        {
                            case 0:
                                if (hexagons[hexagon.DoorTop()].numberDoors == 3 || Convert.ToString(hexagons[hexagon.DoorTop()].doors, 2)[5 - door] == '1')
                                {
                                    continue;
                                }
                                else
                                {
                                    hexagons[hexagon.DoorTop()].numberDoors++;
                                    hexagons[hexagon.DoorTop()].doors += 8;
                                }
                                break;
                            case 1:
                                if (hexagons[hexagon.DoorTopRight()].numberDoors == 3 || Convert.ToString(hexagons[hexagon.DoorTopRight()].doors, 2)[5 - door] == '1')
                                {
                                    continue;
                                }
                                else
                                {
                                    hexagons[hexagon.DoorTopRight()].numberDoors++;
                                    hexagons[hexagon.DoorTopRight()].doors += 16;
                                }
                                break;
                            case 2:
                                if (hexagons[hexagon.DoorBottomRight()].numberDoors == 3 || (int)Convert.ToString(hexagons[hexagon.DoorBottomRight()].doors, 2)[5 - door] == '1')
                                {
                                    continue;
                                }
                                else
                                {
                                    hexagons[hexagon.DoorBottomRight()].numberDoors++;
                                    hexagons[hexagon.DoorBottomRight()].doors += 32;
                                }
                                break;
                            case 3:
                                if (hexagons[hexagon.DoorBottom()].numberDoors == 3 || Convert.ToString(hexagons[hexagon.DoorBottom()].doors, 2)[5 - door] == '1')
                                {
                                    continue;
                                }
                                else
                                {
                                    hexagons[hexagon.DoorBottom()].numberDoors++;
                                    hexagons[hexagon.DoorBottom()].doors += 1;
                                }
                                break;
                            case 4:
                                if (hexagons[hexagon.DoorBottomLeft()].numberDoors == 3 || Convert.ToString(hexagons[hexagon.DoorBottomLeft()].doors, 2)[5 - door] == '1')
                                {
                                    continue;
                                }
                                else
                                {
                                    hexagons[hexagon.DoorBottomLeft()].numberDoors++;
                                    hexagons[hexagon.DoorBottomLeft()].doors += 2;
                                }
                                break;
                            case 5:
                                if (hexagons[hexagon.DoorTopLeft()].numberDoors == 3 || Convert.ToString(hexagons[hexagon.DoorTopLeft()].doors, 2)[5 - door] == '1')
                                {
                                    continue;
                                }
                                else
                                {
                                    hexagons[hexagon.DoorTopLeft()].numberDoors++;
                                    hexagons[hexagon.DoorTopLeft()].doors += 4;
                                }
                                break;
                        }
                    }
                }
            }
        }

        private static void Shuffle(Hexagon[] array)
        {
            int n = array.Length;
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(random.NextDouble() * (n - i));
                int t = array[r].number;
                array[r] = array[i];
                array[i].number = t;
            }
        }
        public static void GenerateCave()
        {
            bool test = true;
            for (int counter = 0; counter < 30; counter++)
			{
				hexagons[counter] = new Hexagon(counter);
			}
			do
			{
				CreateCave();
				FloodFill(0);
				foreach (bool value in fill)
				{
					if (!value)
						test = false;
				}
			}
			while (!test);	
            Shuffle(hexagons);
        }
        
		private List <int[]> caveMatrix = new List<int[]>();
		public static List<Room> Matrix = new List<Room>();
		private int[][] caveConn = new int[][]
		{
			//inner array is the actual room number
			//outer arrays or the {x,x,x} is the connected rooms

			/*new int[] {2, 7, 6},//1 
			new int[] { 26, 7, 9},//2
			new int[] { 28, 9, 26},//3
			new int[] { 27, 9, 4}, //4
			new int[] { 11, 30, 29}, //5
			new int[] { 7, 11, 12}, //6
			new int[] { 1, 2, 8}, //7
			new int[] { 15, 2, 9}, //8
			new int[] { 3, 9, 17}, //9
			new int[] { 17, 11, 4}, //10
			new int[] { 6, 5, 17}, //11
			new int[] {18, 13, 6}, //12
			new int[] {19, 18, 14}, //13
			new int[] {8, 20, 21}, //14
			new int[] {29, 8, 10}, //15
			new int[] {23, 10, 21}, //16
			new int[] { 23, 11, 10}, //17
			new int[] {24, 12, 19}, //18
			new int[] {25, 14, 18}, //19
			new int[] {25, 26, 27},//20
			new int[] { 27, 20, 14}, //21
			new int[] {29, 21, 23}, //22
			new int[] {29, 18, 16}, //23
			new int[] {30, 25, 19}, //24
			new int[] {30, 1, 26}, //25
			new int[] {1, 2, 27}, //26
			new int[] { 3, 22, 20}, //27
			new int[] {3, 4, 5}, //28
			new int[] {22, 23, 24}, //29
			new int[] {1, 25, 5}, //30*/
		};
		private int[][] caveAdj = new int[][]
		{
			//inner array is the actual room number
			//outer arrays or the {x,x,x,x,x,x} is the connected rooms

			/*new int[] {2, 7, 6,25,26,30},//1
			new int[] { 26, 7, 9,1,8,3},//2
			new int[] { 2,9,4,28,27,26},//3
			new int[] { 3,9,10,11,5,28}, //4
			new int[] { 11, 30, 29,28,4,6}, //5
			new int[] { 1,7,12,11,5,30}, //6
			new int[] { 1,2,8,12,13,6}, //7
			new int[] { 13,14,15,2,7,9}, //8
			new int[] { 2,3,4,10,8,15}, //9
			new int[] { 9,4,11,15,16,16}, //10
			new int[] { 4,5,6,10,12,17}, //11
			new int[] {11,6,7,13,17,18}, //12
			new int[] {12,7,8,14,18,19}, //13
			new int[] {13,8,15,19,20,21}, //14
			new int[] {8,9,10,14,21,16}, //15
			new int[] {15,10,17,21,22,23}, //16
			new int[] { 10,11,12,16,23,18}, //17
			new int[] { 17,12,13,23,24,19}, //18
			new int[] {18,12,14,24,25,20}, //19
			new int[] {19,14,21,25,26,27},//20
			new int[] {14,15,16,20,22,27}, //21
			new int[] {21,16,23,27,28,29}, //22
			new int[] {16,17,18,22,29,24}, //23
			new int[] {13,18,19,29,30,25}, //24
			new int[] {24,19,20,30,1,26}, //25
			new int[] {1,2,3,25,20,27}, //26
			new int[] { 20,21,23,26,3,28}, //27
			new int[] {27,22,29,3,4,5}, //28
			new int[] {22,23,24,28,5,30}, //29
			new int[] {29,24,25,5,6,1}, //30*/
		};


		public Cave()
		{
			for (int i = 0; i < 30; i++)
			{
				Room newRoom = new Room();
				newRoom.AdjRooms = caveAdj[i];
				newRoom.ConnectedRooms = caveConn[i];
				newRoom.RoomNumber = i+1;
				Matrix.Add(newRoom);
			}
		}

		public Room getCurrentRoom(int currentRoom)
		{
			return Matrix[currentRoom];
		}

		public bool[] getConnectedRooms(int currentRoom)
		{
			return Matrix[currentRoom].ConnectedRooms;
		}

		public int[] getAdjRooms(int currentRoom)
		{
			if (currentRoom == 1) 
			{
				return Matrix[0].AdjRooms;
			}
			else if (currentRoom > 1 && currentRoom <= 29)
			{
				return Matrix[currentRoom - 1].AdjRooms;
			}
			else
			{
				return Matrix[29].AdjRooms;
			}
		}

		public int[][] getMap()
		{
			int[][] Map = {  new int[] {1, 2, 3, 4,5,6},
							 new int[] {7,8,9,10,11,12},
							 new int[]{13,14,15,16,17,18},
							 new int[]{19,20,21,22,23,24}, 
							 new int[]{25,26,27,28,29,30},
													};

			return Map;
		}
	}
}
