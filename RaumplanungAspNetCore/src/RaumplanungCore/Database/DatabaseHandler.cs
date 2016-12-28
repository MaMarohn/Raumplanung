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
    public class DatabaseHandler : IDatabaseHandler
    {
        private readonly ReservationContext _reservationContext;
        private const int Week = 7;

        public DatabaseHandler(ReservationContext _reservation)
        {
            this._reservationContext = _reservation;
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

                var resAmBlock = reservations.ToList().Where(r => r.Block == blockIndex);
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

        public bool CheckIfReservationsExistsOnDateInBlock(DateTime date, int block, int roomId)
        {
            var dateTime = new DateTime(date.Year, date.Month, date.Day);
            var reservations = _reservationContext.Reservations.ToList()
                .Where(r => r.Date == dateTime && r.Block == block && r.RoomId == roomId);

            return reservations.Any();
        }

        public bool AddReservationSuggestion(string teacherFrom, int reservationFrom, string teacherTo, int reservationoffer,string message)
        {
            //not tested

            if (_reservationContext.Teachers.Find(teacherFrom) == null ||
                _reservationContext.Teachers.Find(teacherTo) == null)
            {
                //Teachers couldnt be found
                return false;
            }

            Reservation reservationF = _reservationContext.Reservations.Find(reservationFrom);
            Reservation reservationT = _reservationContext.Reservations.Find(reservationoffer);

            if (reservationF == null || reservationT == null)
            {
                //Reservations couldnt be found
                return false;
            }

            ExchangeReservation exchangeReservation = new ExchangeReservation
            {
                ReservationFrom = reservationF,
                ReservationOffer = reservationT,
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

        public List<ExchangeReservation> GetAllExchangeReservations()
        {
            return new List<ExchangeReservation>(_reservationContext.ExchangeReservations);
        }

        public List<ExchangeReservation> GetExchangeReservationByTeacherFromId(string id)
        {
            return new List<ExchangeReservation>(_reservationContext.ExchangeReservations.ToList().Where(r => r.TeacherFrom == id));
        }

        public List<ExchangeReservation> GetExchangeReservationByTeacherToId(string id)
        {
            return new List<ExchangeReservation>(_reservationContext.ExchangeReservations.ToList().Where(r => r.TeacherTo == id));
        }

        public List<ExchangeReservation> GetExchangeReservationByTeacherId(string id)
        {
            return new List<ExchangeReservation>(_reservationContext.ExchangeReservations.ToList().Where(rr => rr.TeacherFrom == id));
        }


        public bool AddCourse(DateTime startDate,DateTime endTime, int block, string teacherId , int room , string nameOfCourse)
        {
            DateTime dateTimeStart = GetCorrectDatetime(startDate);
            DateTime dateTimeEnd = GetCorrectDatetime(endTime);

            Course course = new Course
            {
                StartDate = dateTimeStart,
                EndDate = dateTimeEnd,
                Block = block,
                RoomId = room,
                Name = nameOfCourse,
                TeacherId = teacherId                
            };
            _reservationContext.Courses.Add(course);

            DateTime dateStart = dateTimeStart;
            DateTime dateEnd = dateTimeEnd;
            while (dateStart <= dateEnd)
            {
                
                _reservationContext.Add(new Reservation
                {
                    Block = course.Block,
                    TeacherId = course.TeacherId,
                    RoomId = course.RoomId,
                    Date = dateStart,
                    CourseId = course.CourseId
                });
                dateStart = dateStart.AddDays(7);
                _reservationContext.SaveChanges();
            }

            _reservationContext.SaveChanges();

            return true;
        }


        public List<Course> GetCoursesOnDateInBlock(DateTime date, int blockId)
        {
            return new List<Course>(_reservationContext.Courses.ToList().Where
                (c => c.Block == blockId && c.StartDate == GetCorrectDatetime(date)));
        }

       /* public List<CourseExceptions> GetAllCourseExceptionsesFromCourse(int courseId)
        {
           return new List<CourseExceptions>(_reservationContext.CourseExceptionses.ToList().Where
                (c => c.CourseId == courseId));
            
        }*/

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

        private List<Course> CheckWhichCourseTakesPlace(DateTime date , int blockNr)
        {
            date = GetCorrectDatetime(date);
            List<Course> courses = GetAllCoursesInBlock(blockNr);
            List<Course> coursesThatTakesPlace = new List<Course>();

            foreach (var course in courses)
            {
                DateTime startdate = new DateTime
                    (course.StartDate.Year , course.StartDate.Month , course.StartDate.Day);
                bool b = CheckIfCourseTakesPlaceOnDate(date, startdate);
                if (b)
                {
                    coursesThatTakesPlace.Add(course);
                }
            }

            return coursesThatTakesPlace;
        }

        private Boolean CheckIfCourseTakesPlaceOnDate(DateTime pdate, DateTime pcourseTime)
        {
            DateTime dateTime = GetCorrectDatetime(pdate);
            DateTime courseTime = GetCorrectDatetime(pcourseTime);

            while (dateTime <= courseTime)
            {
                if (dateTime == courseTime)
                    return true;
                courseTime.AddDays(Week);
            }
            return false;
        }

        public List<Course> GetAllCoursesInBlock(int blockNr)
        {
            return new List<Course>(_reservationContext.Courses.ToList().Where(r => r.Block == blockNr));
        }

        public List<Course> GetAllCourses()
        {
            return new List<Course>(_reservationContext.Courses);
        }

        public bool ExchangeReservation(string pTeacherFrom, int pReservationFrom , string pTeacherTo , int pReservationTo)
        {
            //not tested
            if (_reservationContext.Teachers.Find(pTeacherFrom) == null ||
                _reservationContext.Teachers.Find(pTeacherTo) == null)
            {
                //Teachers couldnt be found
                return false;
            }

            Reservation reservationFrom = _reservationContext.Reservations.Find(pReservationFrom);
            Reservation reservationTo = _reservationContext.Reservations.Find(pReservationTo);

            if ( reservationFrom == null || reservationTo == null)
            {
                //Reservations couldnt be found
                return false;
            }

            //Es wird getauscht
            reservationFrom.TeacherId = pTeacherTo;
            reservationTo.TeacherId = pTeacherFrom;
            _reservationContext.Entry(reservationFrom).State = EntityState.Modified;
            _reservationContext.Entry(reservationTo).State = EntityState.Modified;
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

