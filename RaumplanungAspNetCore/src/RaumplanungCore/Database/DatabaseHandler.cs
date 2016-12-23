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
[assembly: InternalsVisibleTo("DatabaseTest")]
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

        public List<Block> GetFreeRoomsOnDate(DateTime date)
        {
            const int countBloecke = 8;
            var bloecke = new List<Block>(); // Wir haben genau 8 bloecke
            var dateTime = new DateTime(date.Year, date.Month, date.Day);
            var rooms = GetAllRooms();
            var reservations = _reservationContext.Reservations.ToList().Where(r => r.Date == dateTime);
            if (!reservations.Any())
            {
                for(int i = 0; i < countBloecke; i++)
                {
                    bloecke.Add(new Block()
                    {
                        FreeRooms = rooms,
                        BlockId = i+1                      
                    });
                }
                return bloecke;
            }

            for (int blockIndex = 0; blockIndex < countBloecke; blockIndex++)
            {
                bloecke.Add(new Block()
                {
                    BlockId = blockIndex + 1
                });

                var resAmBlock = reservations.ToList().Where(r => r.Block == blockIndex + 1);
                if (resAmBlock.Count() == 0)
                {
                    //An diesem Block gibt es keine Reservierungen
                    
                    bloecke[blockIndex].FreeRooms = rooms;
                }
                else
                {
                    foreach (var room in rooms)
                    {
                        if (resAmBlock.ToList().Where(r => r.RoomId == room.RoomId).Count() == 0)
                        {
                            bloecke[blockIndex].FreeRooms.Add(room);
                        }
                    }                 
                }
            }
            return bloecke;
        }

        List<Room> IDatabaseHandler.GetFreeRoomsOnDateAndBlock(DateTime date, int block)
        {
            
            var freeRooms = new List<Room>();
            var dateTime = new DateTime(date.Year, date.Month, date.Day);
            var rooms = GetAllRooms();
            var reservations = _reservationContext.Reservations.ToList().Where(r => r.Date == dateTime && r.Block == block);
            if (!reservations.Any())
                return rooms;

            foreach (var room in rooms)
            {
                if (reservations.ToList().Where(r => r.RoomId == room.RoomId).Count() == 0)
                {
                    freeRooms.Add(room);
                }
            }             
            return freeRooms;
        }

        public List<Reservation> GetAllReservations()
        {
            return new List<Reservation>(_reservationContext.Reservations);
        }

        public List<Reservation> GetReservationsWithDate(DateTime date)
        {

            var dateTime = new DateTime(date.Year , date.Month , date.Day);

            var reservations = from t in _reservationContext.Reservations
                                               where t.Date.Equals(dateTime)
                                               select t;

            return new List<Reservation>(reservations);
        }

        public List<Reservation> GetReservationsFromTeacher(string teacherId)
        {
            var t = new List<Teacher>(_reservationContext.Teachers.Include(r => r.Reservations).Where(te => te.Id == teacherId));

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
