using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public partial class Course
    {
        public int CourseId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string Name { set; get; }
        //public int Block { set; get; }
        public string TeacherId { set; get; }
        //public int RoomId { set; get; }
        public List<BlockNrAndRoom> BlockAndRoom { set; get; }

        public String GetRoomsAsString()
        {
            String roomsString = "";
            foreach (var b in BlockAndRoom)
            {
                roomsString += b.Room+" ";
            }
            return roomsString;
        }

        public String GetBlockAsString()
        {
            String blocksString = "";
            foreach (var b in BlockAndRoom)
            {
                blocksString += b.Block + " ";
            }
            return blocksString;
        }

    }

    //EF Core supportet leider die persistierung von List<int> ... nicht
    public class BlockNrAndRoom
    {
        public BlockNrAndRoom(int block, int room)
        {
            Block = block;
            Room = room;
        }
        public BlockNrAndRoom() { }

        public int Id { set; get; }
        public int Block { set; get; }
        public int Room { set; get; }
    }
}
