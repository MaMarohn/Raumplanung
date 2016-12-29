using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RaumplanungCore.Models;

namespace RaumplanungCore.ViewModels.Kurs
{
    public class KursViewModel
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public List<BlockandDay> Days { get; set; }
        public List<List<Room>> Rooms { get; set; }


    }

    public struct BlockandDay
    {
        public DateTime Day { get; set; }
        public int Block { get; set; }
    }
}
