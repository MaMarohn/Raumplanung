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

        public Block GetFreeRoomsOnDate(DateTime date, int blockNr)
        {
            var b = new Block
            {
                Date = date,
                BlockId = blockNr
            };

            //alle Reservation des Tages
            var reservations = _reservationContext.Reservations.ToList().Where(r => r.Date == date);
            //alle Reservationen des Blocks
            var block = reservations.ToList().Where(r => r.Block == blockNr);

            var rooms = GetAllRooms();
            foreach (var r in rooms)
            {
                /*if (block.ToList().Contains())
                {
                    //dieses Raum wurde nicht reserviert
                    b.FreeRooms.Add(r);
                }*/
            }      
            return b;

        }

        public List<Reservation> GetAllReservations()
        {
            return new List<Reservation>(_reservationContext.Reservations);
        }

        public List<Reservation> GetReservationsWithDate(DateTime date)
        {
            /*
             * To Do date and block
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

        public bool AddReservation(DateTime date, int block, string teacherId, int roomId)
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

        public Teacher GetTeacher(string teacherId)
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
