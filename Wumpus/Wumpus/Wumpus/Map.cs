 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
	public class Map
	{
		private int currentRoom;
		private int osamaRoom;
		private int helicopter1;
		private int helocopter2;
		private int oil1;
		private int oil2;

		private string warnings;

		// Variables for assinging random room and for showing hazards.
		private int[] RoomNumber = new int[30];
		Random rdn = new Random();
		bool[] NearHazard = new bool[3];

		//adjacent rooms get from GameControl
		int[] AdjcentRooms = new int[6];

		public int[] AdjRooms
		{
			get { return AdjcentRooms; }
			set { AdjcentRooms = value; }
		}

		public int CurrentRoom
		{
			get { return currentRoom; }
			set { currentRoom = value; }
		}
		public int OsamaRoom
		{
			get { return osamaRoom; }
			set { osamaRoom = value; }
		}
		public int Helicopter1
		{
			get { return helicopter1; }
			set { helicopter1 = value; }
		}
		public int Helicopter2
		{
			get { return helocopter2; }
			set { helocopter2 = value; }
		}
		public int OIL1
		{
			get { return oil1; }
			set { oil1 = value; }
		}
		public int OIL2
		{
			get { return oil2; }
			set { oil2 = value; }
		}
		
		public string Warnings
		{
			get { return warnings; }
			set { warnings = value; }
		}

		public Map()
		{		
			// constructor
			for (int i = 1; i <= 30; i++)
			{
				RoomNumber[i - 1] = i;
			}
			
			Shuffle();
			CheckForHazards();
			Warnings = warnings;
			
		}
		public bool ArrowShoot(int AShot)
		{
			//AShot = room shot into
			//return if arrow hit the wumpus

			return AShot == osamaRoom;
		}
		public void Shuffle()
		{
			// randomizing the rooms
			for (int index = 0; index < RoomNumber.Length; index++)
			{
				int rdnRoom = rdn.Next(1, 30);
				int TempRoom = RoomNumber[index];
				RoomNumber[index] = RoomNumber[rdnRoom];
				RoomNumber[rdnRoom] = TempRoom;

                
			}

			currentRoom = RoomNumber[0];
			OsamaRoom = RoomNumber[1];
			helicopter1 = RoomNumber[2];
			helocopter2 = RoomNumber[3];
			oil1 = RoomNumber[4];
			oil2 = RoomNumber[5];
		}
		
		public void CheckForHazards()
		{
			// checking adjecent rooms for hazards
			NearHazard[0] = false;
			NearHazard[1] = false;
			NearHazard[2] = false;
			
			// need the adjecent room from cave

			// int room = int.Parse(Room Moved To);
			// int[] AdjectentRooms = cave.getAdjRooms(Room Moved To);
			// aMap.AdjRooms = AdjectentRooms;

			for (int i = 0; i < AdjcentRooms.Length; i++)
			{
				if (osamaRoom == AdjcentRooms[i])
				{
					NearHazard[0] = true;
					break;
				}
			}
			for (int i = 0; i < AdjcentRooms.Length; i++)
			{
				if (helicopter1 == AdjcentRooms[i])
				{
					NearHazard[1] = true;
					break;
				}
			}
			for (int i = 0; i < AdjcentRooms.Length; i++)
			{
				if (helocopter2 == AdjcentRooms[i])
				{
					NearHazard[1] = true;
					break;
				}
			}
			for (int i = 0; i < AdjcentRooms.Length; i++)
			{
				if (oil1 == AdjcentRooms[i])
				{
					NearHazard[2] = true;
					break;
				}
			}
			for (int i = 0; i < AdjcentRooms.Length; i++)
			{
				if (oil2 == AdjcentRooms[i])
				{
					NearHazard[2] = true;
					break;
				}
			}
			Warnings = "";

			if (NearHazard[0] == true) warnings += "I hear Osama ";
			if (NearHazard[1] == true) warnings += "  I hear the helicopters coming";
			if (NearHazard[2] == true) warnings += "    I Smell oil";
		}
		public int BatCarryOff()
		{
            return rdn.Next(1, 31);
        }
	}
}

