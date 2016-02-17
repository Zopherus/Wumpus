using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wumpus
{
    public static class Map
    {
        //The room in which each of these is contained
        public static Room OsamaRoom { get; private set; }
        public static Room Helicopter1 { get; private set; }
        public static Room Helicopter2 { get; private set; }
        public static Room Oil1 { get; private set; }
        public static Room Oil2 { get; private set; }

        public static Room[] Hazards { get; private set; }

        public static string Warnings 
        { 
            get 
            {
                CheckForHazards();
                return Warnings; 
            }
        }

        private static Random random = new Random();

        public static void InitializeMap()
        {
            Hazards = new Room[]{OsamaRoom, Helicopter1, Helicopter2, Oil1, Oil2};
            ShuffleHazards();
        }

        private static void ShuffleHazards()
        {
            Room[] hazardRooms = new Room[5];
            int counter = 0;
            while (counter < hazardRooms.Length)
            {
                Room nextRoom = Cave.Rooms[random.Next(Cave.Rooms.Length)];
                if (!hazardRooms.Contains(nextRoom))
                {
                    hazardRooms[counter] = nextRoom;
                    counter++;
                }
            }

            OsamaRoom = hazardRooms[0];
            Helicopter1 = hazardRooms[1];
            Helicopter2 = hazardRooms[2];
            Oil1 = hazardRooms[3];
            Oil2 = hazardRooms[4];
        }

        private static void CheckForHazards()
        {
            /*foreach (Room room in WumpusGame.Player.CurrentRoom)
            {
                if (room == OsamaRoom)
                    warnings += "Osama is near";
                if (room == Helicopter1 || room == Helicopter2)
                    warnings += "You hear the beat of a helicopter";
                if (room == Oil1 || room == Oil2)
                    warnings += "You smell oil";
            }*/
        }
    }
}

