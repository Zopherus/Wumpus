using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
	class Room
	{
		private int[] adjRooms;
		private int[] connectedRooms;
		private int currentRoom;

		public int[] AdjRoom
		{
			get { return adjRooms; }
			set { adjRooms = value; }
		}
		public int[] ConnectedRooms
		{
			get { return connectedRooms; }
			set { connectedRooms = value; }
		}
		public int CurrentRoom
		{
			get { return currentRoom; }
			set { currentRoom = value; }
		}
	}
}
