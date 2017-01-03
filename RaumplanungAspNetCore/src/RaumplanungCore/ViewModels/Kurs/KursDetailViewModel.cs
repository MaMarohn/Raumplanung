using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaumplanungCore.Models;

namespace RaumplanungCore.ViewModels.Kurs
{
    public class KursDetailViewModel
    {
        public string[] BlockStrings { get; set; }
        public string[] RoomStrings { get; set; }
        public string[] DayStrings { get; set; }
        public Course Course { get; set; }
        public int AmountOfAll { get; set; }
    }
}
