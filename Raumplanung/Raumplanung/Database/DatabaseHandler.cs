using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Entities;

namespace Raumplanung.Database
{
    class DatabaseHandler : IDatabaseHandler
    {

        private ReservationContext _reservationContext = new ReservationContext();

        public List<Room> GetAllRooms()
        {
            return new List<Room>(_reservationContext.Rooms);
        }

        public List<Teacher> GetAllTeachers()
        {
            return new List<Teacher>(_reservationContext.Teachers);
        }

        public List<Reservation> GetAllReservations()
        {
            return new List<Reservation>(_reservationContext.Reservations);
        }
    }
}
