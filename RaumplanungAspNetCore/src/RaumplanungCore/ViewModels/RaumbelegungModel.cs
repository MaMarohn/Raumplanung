using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaumplanungCore.Models;

namespace RaumplanungCore.ViewModels
{
    public class RaumbelegungModel
    {
        public Room room { get; set; }
        public bool reserved { get; set; }
        public string teacherId { get; set; }
        public DateTime date { get; set; }
        public int block { get; set; }

        public RaumbelegungModel(Room r, bool res, string t, DateTime d, int b)
        {
            room = r;
            reserved = res;
            teacherId = t;
            date = d;
            block = b;
        }

    }
}
