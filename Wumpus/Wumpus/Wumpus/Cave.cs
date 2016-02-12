using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

namespace Wumpus
{
    class Cave
    {
        public const int numRows = 5;
        public const int numColumns = 6;
        public static Room[] Rooms = new Room[30];
        private static Random random = new Random();

        // Given the position of a room, these are the changes in position for the rooms adjacent to the room
        // Different values are needed if in even or odd column
        // First array is for even columns, second array for odd columns
        // Within each array, starts at room above and moves clockwise
        private static Point[,] directions = {{ new Point(0, -1), new Point(1, -1), new Point(1, 0),
                                            new Point(0, 1), new Point(-1, 0), new Point(-1, -1) },
                                            {new Point(0, -1), new Point(1, 0), new Point(1, -1),
                                            new Point(0, 1), new Point(-1, 1), new Point(-1, 0)} };

        public static void InitializeMap()
        {
            do
            {
                // Initialize the room objects with numbers and positions
                for (int counter = 0; counter < Rooms.Length; counter++)
                {
                    Point position = new Point(counter % numColumns, counter % numRows);
                    Room room = new Room(counter, position);
                    Rooms[counter] = room;
                }
                // Initialize the adjacent and connected rooms
                for (int counter = 0; counter < Rooms.Length; counter++)
                {
                    InitializeAdjacentRooms(Rooms[counter]);
                    ChooseConnectedRooms(Rooms[counter]);
                }
                RandomizeRoomNumbers();
            }
            while (!CheckMap());
        }

        private static void RandomizeRoomNumbers()
        {
            //create an array with ints 0 through how many rooms there are
            int[] roomNumbers = Enumerable.Range(0, Rooms.Length).ToArray();
            
            //randomize the array using Fisher-Yates shuffle
            int position = roomNumbers.Length;
            while (position > 1)
            {
                //creates a random int from 0 to position, not including position to swap to
                //decreases position by 1
                int swapPosition = random.Next(position--);
                int temp = roomNumbers[position];
                roomNumbers[position] = roomNumbers[swapPosition];
                roomNumbers[swapPosition] = temp;
            }

            //set the room numbers of the room equal to the randomized array
            for (int counter = 0; counter < Rooms.Length; counter++)
            {
                Rooms[counter].RoomNumber = roomNumbers[counter];
            }
        }

        private static void InitializeAdjacentRooms(Room room)
        {
            //even or odd column
            int columnParity = room.Position.X % 2;
            for (int counter = 0; counter < room.AdjRooms.Length; counter++)
            {
                room.AdjRooms[counter] = FindRoom(new Point(room.Position.X + directions[columnParity, counter].X,
                                                    room.Position.Y + directions[columnParity, counter].Y));
            }
        }

        //returns the room with the position in the Rooms array
        private static Room FindRoom(Point position)
        {
            //Since the map wraps around on itself, use mod to restrict to correct range
            position.X = mod(position.X, numColumns);
            position.Y = mod(position.Y, numRows);
            foreach (Room room in Rooms)
            {
                if (room != null && room.Position.X == position.X && room.Position.Y == position.Y)
                    return room;
            }
            return null;
        }

        private static void ChooseConnectedRooms(Room room)
        {
            //OTHER ROOMS CAN ADD PATHWAYS ONTO THIS ROOM TO MAKE THE NUMBER OF PATHS GO OVER 3 CAUSING IN AN ERROR
            //INFINITE LOOP IF EVERY ROOM AROUND THIS ROOM ALREADY HAS 3 PATHWAYS
            int numberConnectedRooms = room.ConnectedRooms.Count(c => c);

            int minToAdd = 0;
            //If the room already has a connected path, don't need to add anymore
            if (numberConnectedRooms == 0)
                minToAdd = 1;

            //Since random.Next is exclusive for upper bound, the max number of paths is 3
            int maxToAdd = 4;
            foreach (Room AdjRoom in room.AdjRooms)
            {
                //If an adjacent room is already full on paths, decrease the max by one
                if (AdjRoom.ConnectedRooms.Count(c => c) == 3)
                    maxToAdd--;
            }

            //Ensure that the max is greater than or equal to the min
            maxToAdd = (maxToAdd - numberConnectedRooms > minToAdd) ? maxToAdd - numberConnectedRooms : minToAdd;


            //Ensure that the room cannot have more than 3 paths and at least one path
            int numberPathsToAdd = random.Next(minToAdd, maxToAdd);
            //Use while loop to add that many number of paths
            int counter = 0;
            while (counter < numberPathsToAdd)
            {
                //The position that the hallway will try to be put
                int number = random.Next(0, room.ConnectedRooms.Length);

                //The number of paths that the adjacent room has, has to be kept at or below 3
                int numberConnectedRoomsAdjRoom = room.AdjRooms[number].ConnectedRooms.Count(c => c);
                //Only works if there is not already a path there and that adjacent room has less than 3 paths
                if (!room.ConnectedRooms[number] && numberConnectedRoomsAdjRoom < 3)
                {
                    room.ConnectedRooms[number] = true;

                    //Open the pathway in the adjacent room 
                    //0 corresponds with 3, 1 with 4, and 2 with 5
                    room.AdjRooms[number].ConnectedRooms[(number + 3) % 6] = true;

                    //only increase counter if success is found
                    counter++;
                }
            }
        }

        public static bool CheckMap()
        {
            //True if that room has been visited, false otherwise
            bool[] VisitedRooms = new bool[Rooms.Length];
            FloodFill(VisitedRooms, Rooms[0]);
            foreach (bool value in VisitedRooms)
            {
                if (!value)
                    return false;
            }
            return true;
        }

        public static void FloodFill(bool[] VisitedRooms, Room room)
        {
            VisitedRooms[room.RoomNumber] = true;
            for (int counter = 0; counter < room.ConnectedRooms.Length; counter++)
            {
                //If room is connected in that direction and that room has not yet been visited
                if (room.ConnectedRooms[counter] && !VisitedRooms[room.AdjRooms[counter].RoomNumber])
                {
                    FloodFill(VisitedRooms, room.AdjRooms[counter]);
                }
            }
        }

        //returns the mod of x in base m, % is the remainder function not mod function
        private static int mod(int number, int mod)
        {
            return (number % mod + mod) % mod;
        }
    }
}
