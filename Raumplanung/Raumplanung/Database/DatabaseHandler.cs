using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Entities;

/*
 * For unit testing
 */
[assembly: InternalsVisibleTo("Database_Test")] 
namespace Raumplanung.Database
{
    class DatabaseHandler : IDatabaseHandler
    {
        private readonly ReservationContext _reservationContext;

        public DatabaseHandler()
        { 
            _reservationContext = new ReservationContext();
        }

        public List<Room> GetAllRooms()
        {
            return new List<Room>(_reservationContext.Rooms);
        }

        public List<Room> GetFreeRooms()
        {
            throw new NotImplementedException();
        }

        public List<Room> GetRoomByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Teacher> GetAllTeachers()
        {
            return new List<Teacher>(_reservationContext.Teachers);
        }

        public List<Teacher> GetAllTeachersOrderedByReservations()
        {
            throw new NotImplementedException();
        }

        public Teacher GetTeacher(int id)
        {
            return _reservationContext.Teachers.Find(id);
        }

        public Room GetRoom(int id)
        {
            return _reservationContext.Rooms.Find(id);
        }

        public List<Reservation> GetAllReservations()
        {
            return new List<Reservation>(_reservationContext.Reservations);
        }

        public bool AddNewTeacher(string name)
        {
            _reservationContext.Teachers.Add(new Teacher(name));
            _reservationContext.SaveChanges();
            return true;
        }

        public bool AddNewRoom(string name)
        {
            _reservationContext.Rooms.Add(new Room(name));
            _reservationContext.SaveChanges();
            return true;
        }

        public bool AddNewReservation()
        {
            throw new NotImplementedException();
            _reservationContext.SaveChanges();
        }

        public bool RemoveRoom(String name)
        {
            Room r = GetRoomByName(name);

            if (r != null)
            {
                _reservationContext.Rooms.Remove(r);
                _reservationContext.SaveChanges();
                return true;
            }
            Console.WriteLine("Room was not found");
            return false;
        }

        public bool RemoveTeacher(string name)
        {
            Teacher t = GetTeacherByName(name);

            if (t != null)
            {
                _reservationContext.Teachers.Remove(t);
                _reservationContext.SaveChanges();
                return true;
            }
            Console.WriteLine("Teacher was not found");
            return false;
        }

        public bool RemoveReservation(int id)
        {
            throw new NotImplementedException();
            _reservationContext.SaveChanges();
        }

        public Teacher GetTeacherByName(string name)
        {
            return Enumerable.FirstOrDefault(_reservationContext.Teachers, t => t.Name.Equals(name));
        }

        public Room GetRoomByName(string name)
        {
            return Enumerable.FirstOrDefault(_reservationContext.Rooms, t => t.Name.Equals(name));
        }

    }
}
