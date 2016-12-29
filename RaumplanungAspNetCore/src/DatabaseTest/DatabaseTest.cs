using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Resource;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Raumplanung.Database;
using RaumplanungCore.Controllers;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using Xunit;

namespace DatabaseTest {

    public class DatabaseTest
    {
        private readonly SqliteConnection _connection;
        private readonly ReservationContext _reservationContext;
        private readonly DatabaseHandler _databaseHandler;
        private List<Teacher> _testTeachers;
        private List<Room> _testRooms;
        private List<Reservation> _testReservations;

        public DatabaseTest()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<ReservationContext>()
                    .UseSqlite(_connection)
                    .Options;

            _reservationContext = new ReservationContext(options);
            _reservationContext.Database.EnsureCreated();

            _databaseHandler = new DatabaseHandler(_reservationContext);

            _testRooms = new List<Room> {new Room {Name = "D14"}, new Room {Name = "D15"}, new Room {Name = "D20"}};
            _testTeachers = new List<Teacher> { new Teacher{ Vorname = "Frank"} , new Teacher { Vorname = "Peter" } , new Teacher{ Vorname = "Lisa"}, new Teacher { Vorname = "Martin" } };
            _testReservations = new List<Reservation> { new Reservation {} , new Reservation {}};
        }

        [Fact]
        public void TestGetAllRooms()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.SaveChanges();

            Assert.Equal(1 , _databaseHandler.GetAllRooms().Count);
            _reservationContext.Rooms.Local.Clear();

        }

        [Fact]
        public void TestGetAllTeachers()
        {
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();

            Assert.Equal(1, _databaseHandler.GetAllTeachers().Count);
            _reservationContext.Teachers.Local.Clear();
        }

        [Fact]
        public void TestGetAllReservations()
        {
            AddReservation(0,0,1, new DateTime(2017, 1, 1));

            Assert.Equal(1, _databaseHandler.GetAllReservations().Count);

            Clear();
           
        }

        [Fact]
        public void TestAddRoom()
        {
            _reservationContext.Rooms.Add(new Room {Name = "D20"});
            _reservationContext.SaveChanges();
            Assert.Equal(1 , _reservationContext.Rooms.Count());
            Clear();
        }

        [Fact]
        public void TestGetReservationsOnDate()
        {

            AddReservation(0, 0, 1, new DateTime(2017, 1, 1));

            Assert.Equal(1, _databaseHandler.GetReservationsOnDate(new DateTime(2017, 1, 1)).Count);
            Clear();
        }

        [Fact]
        public void TestGetReservationsFromTeacher()
        {

            AddReservation(0, 0, 1, new DateTime(2017, 1, 1));
            Assert.Equal(1, _databaseHandler.GetReservationsFromTeacher(_testTeachers[0].Id).Count);
            Clear();
        }

        [Fact]
        public void TestGetReservationsFromRoom()
        {
            AddReservation(0, 0, 1, new DateTime(2017, 1, 1));

            Assert.Equal(1, _databaseHandler.GetReservationsFromRoom(_testRooms[0].RoomId).Count);
            Clear();
        }

        [Fact]
        public void TestGetReservationsOnDateInBlock()
        {
            AddReservation(0, 0, 1, new DateTime(2017, 1, 1));
            
            Assert.Equal(1, _databaseHandler.GetReservationsOnDateInBlock(new DateTime(2017, 1, 1) , 1).Count);
            Clear();
        }

        [Fact]
        public void DeleteReservation()
        {
            AddReservation(0,0,1,new DateTime(2017,1,1));
            int id = _reservationContext.Reservations.First().ReservationId;
            _databaseHandler.DeleteReservation(id);
            Assert.Equal(0 , _reservationContext.Reservations.Count());
            Clear();
        }

        [Fact]
        public void TestAddReservation()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();
            _databaseHandler.AddReservation(new DateTime(2017, 1, 1), 1, _testTeachers[0].Id, _testRooms[0].RoomId);
            Assert.Equal(1, _reservationContext.Reservations.Count());
            Clear();
        }


        [Fact]
        public void TestGetTeacher()
        {
            _reservationContext.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();
            Teacher t = _databaseHandler.GetTeacher(_reservationContext.Teachers.First().Id);
            Assert.NotNull(t);
            Clear();
        }

        [Fact]
        public void TestGetRoom()
        {
            _reservationContext.Add(_testRooms[0]);
            _reservationContext.SaveChanges();
            Room r = _databaseHandler.GetRoom(_reservationContext.Rooms.First().RoomId);
            Assert.NotNull(r);
            Clear();
        }

        [Fact]
        public void TestGetReservation()
        {
            AddReservation(0,0,0,new DateTime(2017,1,1));
            _reservationContext.SaveChanges();
            Reservation r = _databaseHandler.GetReservation(_reservationContext.Reservations.First().ReservationId);
            Assert.NotNull(r);
            Clear();
        }

        [Fact]
        public void TestGetFreeRoomsOnDate()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Rooms.Add(_testRooms[1]);
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1));
            _reservationContext.SaveChanges();
            List<Block> b = _databaseHandler.GetFreeRoomsOnDate(new DateTime(2017, 1, 1));
            Assert.Equal(1 , b[0].FreeRooms.Count);
            Assert.Equal(2, b[1].FreeRooms.Count);
            Clear();
        }

        [Fact]
        public void TestGetFreeRoomsOnDateAndBlock()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Rooms.Add(_testRooms[1]);
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1));
            _reservationContext.SaveChanges();
            Assert.Equal(1, _databaseHandler.GetFreeRoomsOnDateAndBlock(new DateTime(2017, 1, 1) , 0).Count);
            Clear();
        }

        [Fact]
        public void TestCheckIfReservationsExistsOnDateInBlock()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1));
            _reservationContext.SaveChanges();
            Assert.Equal(true, _databaseHandler.CheckIfReservationsExistsOnDateInBlock(new DateTime(2017, 1, 1), 0, _testRooms[0].RoomId));
            Clear();
        }

        [Fact]
        public void TestExchangeReservation()
        {
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1), 0);
            AddReservation(1, 1, 0, new DateTime(2017, 1, 1), 1);
            AddExchangeReservation(0, 1);

            String teacherFrom = _testReservations[0].TeacherId;
            String teacherTo = _testReservations[1].TeacherId;

            _databaseHandler.ExchangeReservation(_testReservations[0].TeacherId, _testReservations[0].ReservationId,
                _testReservations[1].TeacherId, _testReservations[1].ReservationId);

            Assert.Equal(teacherFrom, _testReservations[1].TeacherId);
            Assert.Equal(teacherTo, _testReservations[0].TeacherId);
            Clear();
        }    

        [Fact]
        public void TestAddCourse()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();

            string tId = _reservationContext.Teachers.First().Id;
            int roomId = _reservationContext.Rooms.First().RoomId;

            _databaseHandler.AddCourse(new DateTime(2017, 1, 1), new DateTime(2017, 1, 30), 0,
                tId , roomId , "Informatik");
            
            Assert.Equal(5 , _reservationContext.Reservations.Count());

            Clear();
        }

        [Fact]
        public void TestGetAllCourses()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();

            Course c = new Course
            {
                Block = 1,
                RoomId = _testRooms[0].RoomId,
                TeacherId = _testTeachers[0].Id
            };
            _reservationContext.Courses.Add(c);
            _reservationContext.SaveChanges(); 
             
            Assert.Equal(1, _reservationContext.Courses.Count());

            Clear();
        } 

        [Fact]
        public void TestDeleteCourse()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();

            Course c = new Course
            {
                Block = 1,
                RoomId = _testRooms[0].RoomId,
                TeacherId = _testTeachers[0].Id
            };
            _reservationContext.Courses.Add(c);
            _reservationContext.SaveChanges();
            Assert.Equal(true , _databaseHandler.DeleteCourse(_reservationContext.Courses.First().CourseId));
            Clear();
        }

        [Fact]
        public void TestGetCourseById()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();

            Course c = new Course
            {
                Block = 1,
                RoomId = _testRooms[0].RoomId,
                TeacherId = _testTeachers[0].Id
            };
            _reservationContext.Courses.Add(c);
            _reservationContext.SaveChanges();
            Assert.NotNull(_databaseHandler.GetCourseById(_reservationContext.Courses.First().CourseId));
            Clear();
        }

        [Fact]
        public void TestGetCoursesFromTeacher()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.SaveChanges();

            Course c = new Course
            {
                Block = 1,
                RoomId = _testRooms[0].RoomId,
                TeacherId = _testTeachers[0].Id
            };
            _reservationContext.Courses.Add(c);
            _reservationContext.SaveChanges();
            Assert.NotNull(_databaseHandler.GetCoursesFromTeacher(_reservationContext.Teachers.First().Id));
            Clear();
        }

        [Fact]
        public void TestGetExchangeReservationById()
        {

            AddReservation(0,0,0,new DateTime(2017,1,1) , 0);
            AddReservation(1,1,0,new DateTime(2017,1,1) , 1);
            AddExchangeReservation(0, 1);

            int id = _reservationContext.ExchangeReservations.First().EchangeReservationId;

            Assert.NotNull(_databaseHandler.GetExchangeReservationById(id));

            Clear();
        }

        [Fact]
        public void TestDeleteExchangeReservationById()
        {
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1), 0);
            AddReservation(1, 1, 0, new DateTime(2017, 1, 1), 1);
            AddExchangeReservation(0, 1);

            int id = _reservationContext.ExchangeReservations.First().EchangeReservationId;
            _databaseHandler.DeleteExchangeReservationById(id);
            Assert.Equal(0 , _reservationContext.ExchangeReservations.Count());
            Clear();
        }

        [Fact]
        public void TestDeleteExchangeReservationByObject()
        {
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1), 0);
            AddReservation(1, 1, 0, new DateTime(2017, 1, 1), 1);
            AddExchangeReservation(0, 1);

            var er = _reservationContext.ExchangeReservations.First();
            _databaseHandler.DeleteExchangeReservationByObject(er);
            Assert.Equal(0, _reservationContext.ExchangeReservations.Count());
            Clear();
        }

        [Fact]
        public void TestGetExchangeReservationByTeacherFromId()
        {
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1), 0);
            AddReservation(1, 1, 0, new DateTime(2017, 1, 1), 1);
            AddExchangeReservation(0, 1);

            Assert.Equal(0,_databaseHandler.GetExchangeReservationByTeacherFromId("-5").Count);
            Assert.Equal(1,_databaseHandler.GetExchangeReservationByTeacherFromId(_testTeachers[0].Id).Count);
            Clear();
        }

        [Fact]
        public void TestGetExchangeReservationByTeacherToId()
        {
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1), 0);
            AddReservation(1, 1, 0, new DateTime(2017, 1, 1), 1);
            AddExchangeReservation(0, 1);

            Assert.Equal(0, _databaseHandler.GetExchangeReservationByTeacherToId("-5").Count);
            Assert.Equal(0, _databaseHandler.GetExchangeReservationByTeacherToId(_testTeachers[0].Id).Count);
            Assert.Equal(1, _databaseHandler.GetExchangeReservationByTeacherToId(_testTeachers[1].Id).Count);
            Clear();
        }

        [Fact]
        public void TestAddReservationSuggestion()
        {
            AddReservation(0,0,0,new DateTime(2017,1,1) , 0);
            AddReservation(1,1,0, new DateTime(2017, 1, 1), 1);
            _databaseHandler.AddReservationSuggestion(_testReservations[0].TeacherId, _testReservations[0].ReservationId,
                _testReservations[1].TeacherId, _testReservations[1].ReservationId, "TestMessage");

            Assert.Equal(1, _reservationContext.ExchangeReservations.Count());
        }

        public void Dispose()
        {
            _connection.Close();
        }

        /*
         * 
         * ´Keine Tests
         * 
         */
        private void AddExchangeReservation(int resIndexFrom, int resIndexTo)
        {
            _reservationContext.ExchangeReservations.Add(new ExchangeReservation()
            {
                TeacherFrom = _testReservations[resIndexFrom].TeacherId,
                TeacherTo = _testReservations[resIndexTo].TeacherId,
                ReservationFromId = _testReservations[resIndexFrom].ReservationId,
                ReservationOfferId = _testReservations[resIndexTo].ReservationId
            });
            _reservationContext.SaveChanges();
        }

        private void AddReservation(int roomIndex , int teacherIndex , int block , DateTime date , int resId = 0)
        {
            _reservationContext.Rooms.Add(_testRooms[roomIndex]);
            _reservationContext.Teachers.Add(_testTeachers[teacherIndex]);
            _reservationContext.SaveChanges();

            _testReservations[resId].RoomId = _testRooms[roomIndex].RoomId;
            _testReservations[resId].TeacherId = _testTeachers[teacherIndex].Id;
            _testReservations[resId].Date = date;
            _testReservations[resId].Block = block;

            _reservationContext.Reservations.Add(_testReservations[resId]);
            _reservationContext.SaveChanges();
        }

        private void Clear()
        {
            _reservationContext.Reservations.Local.Clear();
            _reservationContext.Teachers.Local.Clear();
            _reservationContext.Rooms.Local.Clear();
            _reservationContext.Courses.Local.Clear();
            _reservationContext.ExchangeReservations.Local.Clear();
        }
    }
}

