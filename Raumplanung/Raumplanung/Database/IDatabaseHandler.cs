using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Entities;

namespace Raumplanung.Database
{
    interface IDatabaseHandler
    {
        List<Room> GetAllRooms();
        List<Teacher> GetAllTeachers();
        List<Teacher> GetAllTeachersOrderedByReservations();
        List<Reservation> GetAllReservations();
    }
}
