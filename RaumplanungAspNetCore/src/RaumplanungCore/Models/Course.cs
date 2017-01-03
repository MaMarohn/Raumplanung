﻿using System;
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
        public List<BlockNrAndRoomAndWeekday> BlockAndRoomAndWeekDay { set; get; }

        public String GetRoomsAsString()
        {
            String roomsString = "";
            foreach (var b in BlockAndRoomAndWeekDay)
            {
                roomsString += b.Room+";";
            }
            return roomsString;
        }

        public String GetBlockAsString()
        {
            String blocksString = "";
            foreach (var b in BlockAndRoomAndWeekDay)
            {
                blocksString += b.Block + ";";
            }
            return blocksString;
        }

        public String GetWeekDayAsString()
        {
            String dayString = "";
            foreach (var b in BlockAndRoomAndWeekDay)
            {
                dayString += b.WeekDay + ";";
            }
            return dayString;
        }



    }

    //EF Core supportet leider die persistierung von List<int> ... nicht
    public class BlockNrAndRoomAndWeekday
    {
        public BlockNrAndRoomAndWeekday(int block, int room , int weekDay)
        {
            Block = block;
            Room = room;
            WeekDay = weekDay;
        }
        public BlockNrAndRoomAndWeekday() { }

        public int Id { set; get; }
        public int Block { set; get; }
        public int Room { set; get; }
        public int WeekDay { set; get; }
    }
}
