using System;
using RaumplanungCore.Models;

namespace RaumplanungCore.ViewModels
{
    public class RaumbelegungModel
    {
        public Room Room { get; set; }
        public bool Reserved { get; set; }
        public Teacher Teacher { get; set; }
        public DateTime Date { get; set; }
        public int Block { get; set; }

        public int BlockIncremented()
        {
            return Block + 1;
        }

        public RaumbelegungModel(Room r, bool res, Teacher t, DateTime d, int b)
        {
            Room = r;
            Reserved = res;
            Teacher = t;
            Date = d;
            Block = b;
            
        }

    }
}
