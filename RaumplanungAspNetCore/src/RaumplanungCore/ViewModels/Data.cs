using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.ViewModels
{
    public class Data
    {
        public static string[] DayStrings = { "Mo", "Di", "Mi", "Do", "Fr" };
        public static string[] BlockStartArray = { "08:30", "10:15", "12:00", "14:15", "16:00", "17:45", "19:30" };
        public static string[] BlockEndArray = { "10:00", "11:45", "13:30", "15:45", "17:30", "19:15", "21:00" };
        public static string[] DayStringsLong = {"Montag", "Dienstag", "Mittwoch", "Donnerstag", "Freitag"};
        public static int AmountOfBlocks = 7;
    }
}
