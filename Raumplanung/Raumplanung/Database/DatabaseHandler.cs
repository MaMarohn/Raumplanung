using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



//For unit testing
[assembly: InternalsVisibleTo("TestDatabase")]
[assembly: InternalsVisibleTo("Database_Test")]
namespace Raumplanung.Database
{
    class DatabaseHandler
    {
        private readonly ReservationContext _reservationContext;

        public DatabaseHandler()
        { 
            _reservationContext = new ReservationContext();
            _reservationContext.FillDatabase();
        }

        public List<Room> GetAllRooms()
        {
            return new List<Room>(_reservationContext.Room);
        }

        public List<Room> GetFreeRooms()
        {
            return null;
            //return _reservationContext.Room.Where(room => room.Free == true).ToList();
        }

        public List<Room> GetRoomByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public List<Teacher> GetAllTeachers()
        {
            return new List<Teacher>(_reservationContext.Teacher);
        }

        public List<Teacher> GetAllTeachersOrderedByReservations()
        {
            //?
            _reservationContext.Teacher.SqlQuery("SELECT * FROM dbo.Teacher ORDER BY CountAssignedRooms Desc");

            throw new NotImplementedException();
        }

        public Teacher GetTeacher(int id)
        {
            return _reservationContext.Teacher.Find(id);
        }

        public Room GetRoom(int id)
        {
            return _reservationContext.Room.Find(id);
        }

        public List<Reservation> GetAllReservations()
        {
            return new List<Reservation>(_reservationContext.Reservation);
        }

        public bool AddNewTeacher(string name)
        {
            _reservationContext.Teacher.Add(new Teacher(name));
            _reservationContext.SaveChanges();
            return true;
        }

        public bool AddNewRoom(string name)
        {
            _reservationContext.Room.Add(new Room(name));
            _reservationContext.SaveChanges();
            return true;
        }

        public bool AddNewReservation(Room r, Teacher t, DateTime d)
        {
            _reservationContext.Reservation.Add(new Reservation(r.RoomID,t.TeacherID,d));
            _reservationContext.SaveChanges();
            return true;
        }

        public bool RemoveRoom(String name)
        {
            Room r = GetRoomByName(name);

            if (r != null)
            {
                _reservationContext.Room.Remove(r);
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
                _reservationContext.Teacher.Remove(t);
                _reservationContext.SaveChanges();
                return true;
            }
            Console.WriteLine("Teacher was not found");
            return false;
        }

        public bool RemoveReservation(int id)
        {
            Reservation r = _reservationContext.Reservation.Find(id);
            _reservationContext.Reservation.Remove(r);
            _reservationContext.SaveChanges();
            return true;
        }

        public Teacher GetTeacherByName(string name)
        {
            return Enumerable.FirstOrDefault(_reservationContext.Teacher, t => t.Name.Equals(name));
        }

        public Room GetRoomByName(string name)
        {
            return Enumerable.FirstOrDefault(_reservationContext.Room, t => t.Name.Equals(name));
        }

    }
}
