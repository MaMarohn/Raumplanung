using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RaumplanungCore.Database;
using RaumplanungCore.Models;

/*
 * For unit testing
 */
[assembly: InternalsVisibleTo("Database_Test")] 
namespace Raumplanung.Database
{
    class DatabaseHandler : IDatabaseHandler
    {
        private readonly ReservationContext _reservationContext;

        public DatabaseHandler(ReservationContext _reservation)
        {
            this._reservationContext = _reservation;
        }


        public List<Room> GetAllRooms()
        {
            return new List<Room>(_reservationContext.Rooms);
        }

        public List<Room> GetAllFreeRooms(DateTime date, int block)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> GetAllReservations()
        {
            return new List<Reservation>();
        }

        public List<Reservation> GetReservationsWithDate(DateTime date)
        {
            /*
             * To Do date
             */

            var reservations = from t in _reservationContext.Reservations
                                               where t.Date.Equals(date)
                                               select t;

            return new List<Reservation>(reservations);
        }

        public List<Reservation> GetReservationsFromTeacher(int teacherId)
        {
            var reservations = from t in _reservationContext.Reservations
                        where t.TeacherId.Equals(teacherId)
                        select t;

            return new List<Reservation>(reservations);
        }
    }
}
