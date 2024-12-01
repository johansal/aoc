using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day23
{
    public static class Program
    {
        public static void Main()
        {
            //             A 1    energy
            //             B 10   energy
            //             C 100  energy
            //             D 1000 energy
            //             #############
            //             #...........#
            //             ###A#C#B#C###
            //               #D#A#D#B#
            //               #2#4#6#8#   

            char[] corridor = { '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.' };
            Stack<char> aRoom = new();
            aRoom.Push('D');
            aRoom.Push('A');
            Stack<char> bRoom = new();
            bRoom.Push('A');
            bRoom.Push('C');
            Stack<char> cRoom = new();
            cRoom.Push('D');
            cRoom.Push('B');
            Stack<char> dRoom = new();
            dRoom.Push('B');
            dRoom.Push('C');
            List<Stack<char>> rooms = new();
            rooms.Add(aRoom);
            rooms.Add(bRoom);
            rooms.Add(cRoom);
            rooms.Add(dRoom);

            //Basic cycle
            //check if we can move any of the amphi pods from corridor to their room
            //  move all pods to their rooms
            //check if we can or need to move any amphipod to corridor
            //  do it for all possible pod and position combinations
            //repeat until either all amphipods are in correct rooms or no pod was moved last turn
            //check basic cycle 
            int lowest = int.MaxValue;
            Console.WriteLine(OrderAmphiPods(ref lowest, corridor, rooms, 0));
        }
        public static void Print(char[] corridor, List<Stack<char>> rooms)
        {
            Console.WriteLine(new string(corridor));
            foreach (var room in rooms)
            {
                Console.WriteLine(new string(room.ToArray()));
            }
        }
        public static int OrderAmphiPods(ref int l, char[] corridor, List<Stack<char>> rooms, int totalEnergy)
        {
            //Console.WriteLine("corridor state:");
            //Print(corridor, rooms);
           
            var corridorMoves = ClearCorridor(ref corridor, ref rooms);
            totalEnergy += corridorMoves;
            bool allRoomsRdy = true;
            int minEnergy = int.MaxValue;
            for (var i = 0; i < rooms.Count; i++)
            {
                if (!RoomIsReady(i, rooms[i]))
                {
                    allRoomsRdy = false;
                    if (RoomShouldPop(i, rooms[i]))
                    {
                        //move pod to corridor
                        var entry = 2 * (i + 1);
                        int energy;
                        foreach (var target in FreeCorridorPositions(corridor, entry))
                        {
                            var tmp = 0;
                            char[] newCorridor = new char[corridor.Length];
                            Array.Copy(corridor, newCorridor, corridor.Length);
                            List<Stack<char>> newRooms = new();
                            foreach(var room in rooms) {
                                newRooms.Add(new Stack<char>(room));
                            }
                            newCorridor[target] = newRooms[i].Pop();
                            tmp += entry - target > 0 ? entry - target : target - entry;
                            tmp += 2 - newRooms[i].Count;
                            energy = OrderAmphiPods(ref l, newCorridor, newRooms, totalEnergy + (tmp * (int)Math.Pow(10, i + 1)));
                            if (energy < minEnergy) minEnergy = energy;
                        }
                    }
                }
            }
            if (allRoomsRdy) {
                if(totalEnergy < l) {
                Console.WriteLine(totalEnergy);
                l = totalEnergy;
                }
                return totalEnergy;
            }
            return minEnergy;
        }
        public static List<int> FreeCorridorPositions(char[] corridor, int entryPosition)
        {
            List<int> ret = new();
            for (var i = entryPosition + 1; i < corridor.Length; i++)
            {
                if (corridor[i] != '.') break;
                ret.Add(i);
            }
            for (var i = entryPosition - 1; i >= 0; i--)
            {
                if (corridor[i] != '.') break;
                ret.Add(i);
            }
            return ret;
        }
        public static bool RoomShouldPop(int roomIndex, Stack<char> room)
        {
            return room.Any(r => r - 'A' != roomIndex);
        }
        public static bool RoomIsReady(int roomIndex, Stack<char> room)
        {
            return room.Count == 2 && !room.Any(r => r - 'A' != roomIndex);
        }
        public static bool RoomIsClear(char c, Stack<char> room)
        {
            return room.Count < 2 && !room.Any(r => r != c);
        }
        public static bool CanMoveToRoomEntry(int position, int entryPosition, char[] corridor)
        {
            if (position < entryPosition)
            {
                for (var i = position + 1; i < entryPosition; i++)
                {
                    if (corridor[i] != '.') return false;
                }
            }
            else
            {
                for (var i = position - 1; i > entryPosition; i--)
                {
                    if (corridor[i] != '.') return false;
                }
            }
            return true;
        }
        public static int ClearCorridor(ref char[] corridor, ref List<Stack<char>> rooms)
        {
            int energyUsed = 0;
            bool podmoved = true;
            while (podmoved)
            {
                podmoved = false;
                for (var i = 0; i < corridor.Length; i++)
                {
                    //check if we can move pod from corridor to its room
                    if (corridor[i] == 'A' && RoomIsClear('A', rooms[0]) && CanMoveToRoomEntry(i, 2, corridor))
                    {
                        var tmp = 0;
                        podmoved = true;
                        rooms[0].Push(corridor[i]);
                        corridor[i] = '.';
                        tmp += i < 2 ? 2 - i : i - 2;
                        tmp += rooms[0].Count;
                        energyUsed += tmp;
                    }
                    else if (corridor[i] == 'B' && RoomIsClear('B', rooms[1]) && CanMoveToRoomEntry(i, 4, corridor))
                    {
                        var tmp = 0;
                        podmoved = true;
                        rooms[1].Push(corridor[i]);
                        corridor[i] = '.';
                        tmp += i < 4 ? 4 - i : i - 4;
                        tmp += rooms[1].Count;
                        energyUsed += tmp * 10;
                    }
                    else if (corridor[i] == 'C' && RoomIsClear('C', rooms[2]) && CanMoveToRoomEntry(i, 6, corridor))
                    {
                        var tmp = 0;
                        podmoved = true;
                        rooms[2].Push(corridor[i]);
                        corridor[i] = '.';
                        tmp += i < 6 ? 6 - i : i - 6;
                        tmp += rooms[2].Count;
                        energyUsed += tmp * 100;
                    }
                    else if (corridor[i] == 'D' && RoomIsClear('D', rooms[3]) && CanMoveToRoomEntry(i, 8, corridor))
                    {
                        var tmp = 0;
                        podmoved = true;
                        rooms[3].Push(corridor[i]);
                        corridor[i] = '.';
                        tmp += i < 8 ? 8 - i : i - 8;
                        tmp += rooms[3].Count;
                        energyUsed += tmp * 1000;
                    }
                }
            }
            return energyUsed;
        }
    }
}
