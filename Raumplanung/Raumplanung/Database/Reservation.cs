using System;

namespace Raumplanung
{
    partial class Reservation
    {

        public Reservation()
        { 
        }

        public Reservation(int teacherId , int roomId , DateTime d)
        {
            this.Teacher = teacherId;
            this.Room = roomId;
            this.Date = d;
        }
    }
}
