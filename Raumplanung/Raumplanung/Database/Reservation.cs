using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
