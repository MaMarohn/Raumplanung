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
            _testReservations = new List<Reservation> { new Reservation {} , new Reservation {} , new Reservation {}};
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
            _databaseHandler.AddRoom("TestRaum");
            _reservationContext.SaveChanges();
            Assert.Equal(1 , _reservationContext.Rooms.Count());
            _reservationContext.Rooms.Local.Clear();
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

        public void TestExchangeReservation()
        {
            _reservationContext.Rooms.Add(_testRooms[0]);
            _reservationContext.Rooms.Add(_testRooms[1]);
            _reservationContext.Teachers.Add(_testTeachers[0]);
            _reservationContext.Teachers.Add(_testTeachers[1]);
            AddReservation(0, 0, 0, new DateTime(2017, 1, 1));
            AddReservation(1, 1, 0, new DateTime(2017, 5, 1));

            //_databaseHandler.ExchangeReservation(_te)
            _reservationContext.SaveChanges();
        }

        public void TestGetAllExchangeReservations()
        {
            
        }

        public void TestGetExchangeReservationByTeacherFromId()
        {
            
        }

        public void TestGetExchangeReservationByTeacherToId()
        {
            
        }

        public void TestAddCourse()
        {
            
        }

        public void TestGetCoursesOnDateInBlock()
        {
            
        }

        public void TestGetAllCourses()
        {
            
        }

        public void TestDeleteCourse()
        {
            
        }

        public void TestGetCourseById()
        {
            
        }

        public void TestGetCoursesFromTeacher()
        {
            
        }

        public void TestGetExchangeReservationById()
        {
            
        }

        public void TestDeleteExchangeReservationById()
        {
            
        }

        public void TestDeleteExchangeReservationByObject()
        {
            
        }

        public void TestGetAllCoursesInBlock()
        {
            
        }

        public void TestAddReservationSuggestion()
        {
            
        }


        private void AddReservation(int roomIndex , int teacherIndex , int block , DateTime date)
        {
            _reservationContext.Rooms.Add(_testRooms[roomIndex]);
            _reservationContext.Teachers.Add(_testTeachers[teacherIndex]);
            _reservationContext.SaveChanges();

            _testReservations[0].RoomId = _testRooms[roomIndex].RoomId;
            _testReservations[0].TeacherId = _testTeachers[teacherIndex].Id;
            _testReservations[0].Date = date;
            _testReservations[0].Block = block;

            _reservationContext.Reservations.Add(_testReservations[0]);
            _reservationContext.SaveChanges();
        }

        private void Clear()
        {
            _reservationContext.Reservations.Local.Clear();
            _reservationContext.Teachers.Local.Clear();
            _reservationContext.Rooms.Local.Clear();
        }


        public void Dispose()
        {
            _connection.Close();
        }
    }
}

