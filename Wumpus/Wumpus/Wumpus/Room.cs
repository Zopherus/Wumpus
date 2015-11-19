using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
	class Room
	{
        //first index is top room, goes clockwise
		public int[] AdjRooms { get; private set; }

        //is true if that room in that position is connected to this room
        public bool[] ConnectedRooms { get; private set; }

        //room number
        public int RoomNumber { get; private set; }

        public Room()
        {
            AdjRooms = new int[6];
            ConnectedRooms = new bool[6];
        }
	}
}
