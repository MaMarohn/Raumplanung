using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public class Block
    {
        public Block()
        {
            FreeRooms = new List<Room>();
        }

        public int BlockId { set; get; }
        //public DateTime Date { set; get; }
        public List<Room> FreeRooms { set; get; }

    }
}
