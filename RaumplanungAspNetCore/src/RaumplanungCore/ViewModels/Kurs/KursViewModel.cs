using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Server.Kestrel.Filter.Internal;
using RaumplanungCore.Models;

namespace RaumplanungCore.ViewModels.Kurs
{
    public class KursViewModel
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public List<string> Days { get; set; }
        public List<DayAndRooms> Roomlist { get; set; }
    }

    public struct BlockandDay
    {
        public DateTime Day { get; set; }
        public int Block { get; set; }
    }

    public struct DayAndRooms
    {
        public DateTime Date { get; set; }
        public int block { get; set; }
        public List<Room> Rooms { get; set; }
        public Room ChosenRoom { get; set; }
        
    }

    public struct DateandRoom
    {
        public int block { get; set; }
        public int weekday { get; set; }
        public Room room { get; set; }

    }
}
