using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using Microsoft.EntityFrameworkCore;


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

        public List<Room> GetAllFreeRoomsOnDate(DateTime date, int block)
        {
            /*var firstName = "John";
            var id = 12;
            var sql = @"Update [User] SET FirstName = {0} WHERE Id = {1}";
            ctx.Database.ExecuteSqlCommand(sql, firstName, id);*/

            //var sql = "Select Reser From Reservation WHERE block={0} AND Date={1}";
            //var result = _reservationContext.Reservations.FromSql(sql, block, date).ToList();

            //To-Do
            return null;

        }

        public List<Reservation> GetAllReservations()
        {
            return new List<Reservation>(_reservationContext.Reservations);
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

        public List<Reservation> GetReservationsFromTeacher(string teacherId)
        {
            List<Teacher> t = new List<Teacher>(_reservationContext.Teachers.Include(r => r.Reservations).Where(te => te.Id == teacherId));

            if (t.Count == 0)
                return null;

            return new List<Reservation>(t.First().Reservations);
        }

        public List<Reservation> GetReservationsFromRoom(int roomId)
        {
            List<Room> t = new List<Room>(_reservationContext.Rooms.Include(r => r.Reservations).Where(te => te.RoomId == roomId));

            if (t.Count == 0)
                return null;

            return new List<Reservation>(t.First().Reservations);
        }


        public List<Teacher> GetAllTeachers()
        {
            return new List<Teacher>(_reservationContext.Teachers);
        }

        public bool DeleteReservation(int reservationId)
        {
            Reservation r = _reservationContext.Reservations.Find(reservationId);

            if (r == null)
                return false;

            _reservationContext.Reservations.Remove(r);
            _reservationContext.SaveChanges();
            return true;
        }

        public bool AddReservation(DateTime date, int block, int teacherId, int roomId)
        {
            _reservationContext.Reservations.Add(new Reservation
            {
                Date = date,
                Block = block,
                TeacherId = teacherId,
                RoomId = roomId
            });
            return true;
        }

        public Teacher GetTeacher(int teacherId)
        {
            return _reservationContext.Teachers.Find(teacherId);
        }

        public Room GetRoom(int roomId)
        {
            return _reservationContext.Rooms.Find(roomId);
        }

        public Reservation GetReservation(int reservationId)
        {
            return _reservationContext.Reservations.Find(reservationId);
        }
    }
}
