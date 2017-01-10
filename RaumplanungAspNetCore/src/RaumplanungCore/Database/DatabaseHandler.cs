using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using Microsoft.EntityFrameworkCore;
using RaumplanungCore.ViewModels.Kurs;


/*
 * For unit testing
 */
[assembly: InternalsVisibleTo("DatabaseTest")]
namespace Raumplanung.Database
{
    public class DatabaseHandler : IDatabaseHandler
    {
        private readonly ReservationContext _reservationContext;
        private const int Week = 7;

        public DatabaseHandler(ReservationContext reservation)
        {
            this._reservationContext = reservation;
        }


        public List<Room> GetAllRooms()
        {
            return new List<Room>(_reservationContext.Rooms.OrderBy(r => r.Name));
        }

        public List<Block> GetFreeRoomsOnDate(DateTime date)
        {
            const int countBloecke = 7;
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
                        BlockId = i                      
                    });
                }
                return bloecke;
            }

            for (int blockIndex = 0; blockIndex < countBloecke; blockIndex++)
            {
                bloecke.Add(new Block()
                {
                    BlockId = blockIndex
                });

                List<Reservation> list = reservations.ToList();
                var resAmBlock = list.Where(r => r.Block == blockIndex);
                if (!resAmBlock.Any())
                {
                    //An diesem Block gibt es keine Reservierungen                    
                    bloecke[blockIndex].FreeRooms = rooms;
                }
                else
                {
                    foreach (var room in rooms)
                    {
                        int count = resAmBlock.Count(r => r.RoomId == room.RoomId);
                        if (count == 0)
                        {
                            bloecke[blockIndex].FreeRooms.Add(room);
                        }
                    }                 
                }
            }
            return bloecke;
        }

        public bool CheckIfReservationsExistsOnDateInBlock(DateTime date, int block, int roomId)
        {
            var dateTime = new DateTime(date.Year, date.Month, date.Day);
            var reservations = _reservationContext.Reservations.ToList()
                .Where(r => r.Date == dateTime && r.Block == block && r.RoomId == roomId);

            return reservations.Any();
        }

        public bool AddReservationSuggestion(string teacherFrom, int reservationFrom, string teacherTo, int reservationoffer,string message)
        {

            if (_reservationContext.Teachers.Find(teacherFrom) == null ||
                _reservationContext.Teachers.Find(teacherTo) == null)
            {
                //Teachers couldnt be found
                return false;
            }
            
            ExchangeReservation exchangeReservation = new ExchangeReservation
            {
                ReservationFromId = reservationFrom,
                ReservationOfferId = reservationoffer,
                TeacherFrom = teacherFrom,
                TeacherTo = teacherTo,
                ExchangeAccepted = false,
                ExchangeStatus = false,
                Message = message,
                Seen = false
            };

            _reservationContext.ExchangeReservations.Add(exchangeReservation);
            _reservationContext.SaveChanges();

            return true;
        }

        public List<ExchangeReservation> GetExchangeReservationByTeacherFromId(string id)
        {
            return new List<ExchangeReservation>(_reservationContext.ExchangeReservations.ToList().Where(r => r.TeacherFrom == id));
        }

        public List<ExchangeReservation> GetExchangeReservationByTeacherToId(string id)
        {
            return new List<ExchangeReservation>(_reservationContext.ExchangeReservations.ToList().Where(r => r.TeacherTo == id));
        }

        public bool AddCourse(List<DateandRoom> dateandRooms , DateTime startDate,
            DateTime endDate , string courseName , string teacherId)
        {

            DateTime dateTimeStart = GetCorrectDatetime(startDate);
            DateTime dateTimeEnd = GetCorrectDatetime(endDate);

            var blockNrAndRooms  = dateandRooms.
                Select(d => new BlockNrAndRoomAndWeekday(d.block, d.room.RoomId, d.weekday)).ToList();

            var course = new Course
            {
                StartDate = dateTimeStart,
                EndDate = dateTimeEnd,
                BlockAndRoomAndWeekDay = blockNrAndRooms,
                Name = courseName,
                TeacherId = teacherId
            };
            var c = _reservationContext.Courses.Add(course);
            _reservationContext.SaveChanges();

            foreach (var d in blockNrAndRooms)
            {
                _reservationContext.BlockNrAndRoomAndWeekdays.Find(d.Id).CourseId = c.Entity.CourseId;              
            }
            _reservationContext.SaveChanges();

            foreach (var dateAndRoom in dateandRooms)
            {
                AddReservationsForCourse(startDate, endDate, dateAndRoom.block, teacherId, dateAndRoom.room.RoomId,
                    c.Entity.CourseId, dateAndRoom.weekday);
            }
            return true;
        }

        private bool AddReservationsForCourse(DateTime startDate, DateTime endTime, int block, string teacherId, int room,
            int courseId, int dayOfWeek)
        {
            DateTime dateTimeStart = GetCorrectDatetime(startDate);
            DateTime dateTimeEnd = GetCorrectDatetime(endTime);

            if ((int)dateTimeStart.DayOfWeek != dayOfWeek)
            {
                if ((int)startDate.DayOfWeek > dayOfWeek)
                {
                    int diff = (int)startDate.DayOfWeek - dayOfWeek;
                    dateTimeStart = new DateTime(dateTimeStart.Year,
                        dateTimeStart.Month, dateTimeStart.Day - diff);
                }
                else
                {
                    int diff = (int)startDate.DayOfWeek - dayOfWeek;
                    dateTimeStart = new DateTime(dateTimeStart.Year,
                        dateTimeStart.Month, dateTimeStart.Day - diff);
                }
            }

            while (dateTimeStart <= dateTimeEnd)
            {
                _reservationContext.Add(new Reservation
                {
                    Block = block,
                    TeacherId = teacherId,
                    RoomId = room,
                    Date = dateTimeStart,
                    CourseId = courseId
                });
                dateTimeStart = dateTimeStart.AddDays(7);
                _reservationContext.SaveChanges();
            }

            _reservationContext.SaveChanges();

            return true;
        }

        public bool AddCourse(DateTime startDate, DateTime endTime, int block, 
            string teacherId , int room , string nameOfCourse , int dayOfWeek)
        {

            DateTime dateTimeStart = GetCorrectDatetime(startDate);
            DateTime dateTimeEnd = GetCorrectDatetime(endTime);
            var blockNrAndRoom = new BlockNrAndRoomAndWeekday(block, room , dayOfWeek);

            Course course = new Course
            {
                StartDate = dateTimeStart,
                EndDate = dateTimeEnd,
                BlockAndRoomAndWeekDay = new List<BlockNrAndRoomAndWeekday> { blockNrAndRoom },              
                Name = nameOfCourse,
                TeacherId = teacherId
            };
            var c = _reservationContext.Courses.Add(course);
            _reservationContext.SaveChanges();

            _reservationContext.BlockNrAndRoomAndWeekdays.Find(blockNrAndRoom.Id).CourseId = c.Entity.CourseId;
            _reservationContext.SaveChanges();

            while (dateTimeStart <= dateTimeEnd)
            {                
                _reservationContext.Add(new Reservation
                {
                    Block = block,
                    TeacherId = course.TeacherId,
                    RoomId = room,
                    Date = dateTimeStart,
                    CourseId = c.Entity.CourseId
                });
                dateTimeStart = dateTimeStart.AddDays(7);
                _reservationContext.SaveChanges();
            }

            _reservationContext.SaveChanges();

            return true;
        }

        public List<ExchangeReservation> GetExchangeReservationByTeacherId(string id)
        {
            return new List<ExchangeReservation>(_reservationContext.ExchangeReservations.ToList().Where(rr => rr.TeacherFrom == id));
        }

        public List<Room> GetFreeRoomsOnDateAndBlock(DateTime date, int block)
        {
            var freeRooms = new List<Room>();
            var dateTime = new DateTime(date.Year, date.Month, date.Day);
            var rooms = GetAllRooms();
            var reservations = _reservationContext.Reservations.ToList().Where(r => r.Date == dateTime && r.Block == block);
            if (!reservations.Any())
                return rooms;

            foreach (var room in rooms)
            {
                int count = reservations.Count(r => r.RoomId == room.RoomId);
                if (count == 0)
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

        public List<Reservation> GetReservationsOnDate(DateTime date)
        {

            var dateTime = new DateTime(date.Year , date.Month , date.Day);

            var reservations = from t in _reservationContext.Reservations
                                               where t.Date.Equals(dateTime)
                                               select t;

            return new List<Reservation>(reservations = reservations.OrderBy(r => r.RoomId));
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

            return t.Count == 0 ? null : new List<Reservation>(t.First().Reservations);
        }


        public List<Reservation> GetReservationsOnDateInBlock(DateTime date, int blockNr)
        {
            var dateTime = new DateTime(date.Year, date.Month, date.Day);
            var reservations = _reservationContext.Reservations.ToList()
                .Where(r => r.Date == dateTime && r.Block == blockNr);
            return new List<Reservation>(reservations);
        }

        public List<Reservation> GetAllReservationsFromCourse(int courseId)
        {
            var reservations = _reservationContext.Reservations.ToList()
                .Where(r => r.CourseId == courseId);
            return new List<Reservation>(reservations);
        }

        public bool DeleteExchangeReservationByObject(ExchangeReservation exchangeReservation)
        {
            if (exchangeReservation == null) return false;
            _reservationContext.ExchangeReservations.Remove(exchangeReservation);
            _reservationContext.SaveChanges();
            return true;
        }

        public List<Course> GetAllCourses()
        {
            return _reservationContext.Courses.Include(r => r.BlockAndRoomAndWeekDay).ToList();
        }

        public bool DeleteCourse(int id)
        {
            Course course = _reservationContext.Courses.Find(id);
            if (course == null) return false;

            foreach (var c in _reservationContext.Reservations)
            {
                if (c.CourseId == id)
                    _reservationContext.Reservations.Remove(c);
            }

            foreach (var r in _reservationContext.BlockNrAndRoomAndWeekdays)
            {
                if (r.CourseId == course.CourseId)
                    _reservationContext.BlockNrAndRoomAndWeekdays.Remove(r);
            }
            _reservationContext.Courses.Remove(course);
            _reservationContext.SaveChanges();
            return true;
        }

        public Course GetCourseById(int id)
        {
            return _reservationContext.Courses.Include(r => r.BlockAndRoomAndWeekDay).Single(rr => rr.CourseId ==  id);
        }

        public List<Course> GetCoursesFromTeacher(string teacherId)
        {
            var courses = GetAllCourses();
            return new List<Course>(courses.ToList().Where(r => r.TeacherId == teacherId));
        }

        public ExchangeReservation GetExchangeReservationById(int id)
        {
            return _reservationContext.ExchangeReservations.Find(id);
        }

        public bool DeleteExchangeReservationById(int id)
        {
            var exchangeReservation = GetExchangeReservationById(id);
            if (exchangeReservation == null) return false;
            _reservationContext.ExchangeReservations.Remove(exchangeReservation);
            _reservationContext.SaveChanges();
            return true;
        }

        public bool ExchangeReservation(string pTeacherFrom, int pReservationFrom , string pTeacherTo , int pReservationTo)
        {
            if (_reservationContext.Teachers.Find(pTeacherFrom) == null ||
                _reservationContext.Teachers.Find(pTeacherTo) == null)
            {
                //Teachers couldnt be found
                return false;
            }

            Reservation reservationFrom = _reservationContext.Reservations.Find(pReservationFrom);
            Reservation reservationTo = null;
            if (pReservationTo != -1)
            {
                reservationTo = _reservationContext.Reservations.Find(pReservationTo);
            }
            
            if ( reservationFrom == null ||(pReservationTo!=-1 && reservationTo == null))
            {
                return false;
            }

            //Es wird getauscht
            reservationFrom.TeacherId = pTeacherTo;
            reservationFrom.CourseId = null;
            if (pReservationTo != -1)
            {
                reservationTo.TeacherId = pTeacherFrom;
                reservationTo.CourseId = null;
                _reservationContext.Entry(reservationTo).State = EntityState.Modified;
            }
           _reservationContext.Entry(reservationFrom).State = EntityState.Modified;           
           _reservationContext.SaveChanges();

            return true;
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
            DateTime dateTime = new DateTime(date.Year, date.Month, date.Day);

            if (CheckIfReservationsExistsOnDateInBlock(date , block , roomId))
            {
                return false;
            }

            _reservationContext.Reservations.Add(new Reservation
            {
                Date = dateTime,
                Block = block,
                TeacherId = teacherId,
                RoomId = roomId
            });

            _reservationContext.SaveChanges();
            return true;
        }

        public bool AddRoom(string name)
        {
            if (_reservationContext.Rooms.Count(r => r.Name == name) != 0) return false;
            _reservationContext.Rooms.Add(new Room {Name = name});
            _reservationContext.SaveChanges();
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

        private DateTime GetCorrectDatetime(DateTime time)
        {
            return new DateTime(time.Year , time.Month , time.Day);
        } 
      
    }
}



