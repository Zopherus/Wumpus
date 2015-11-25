using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Wumpus
{
	public class Room
	{
        //first index is top room, goes clockwise
		public Room[] AdjRooms { get; private set; }

        //is true if that room in that position is connected to this room
        public bool[] ConnectedRooms { get; private set; }

        //room number, goes from 0 to 29
        private int roomNumber;

        public int RoomNumber
        {
            get { return roomNumber; }
            set { if (value >= 0 && value <= Cave.Rooms.Length) roomNumber = value; }
        }

        public Point Position { get; private set; }

        public Room()
        {
            AdjRooms = new Room[6];
            ConnectedRooms = new bool[6];
        }

        public Room(int RoomNumber, Point Position)
        {
            this.RoomNumber = RoomNumber;
            this.Position = Position;
            AdjRooms = new Room[6];
            ConnectedRooms = new bool[6];
        }
	}
}
