using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Moq;
using Raumplanung.Database;
using RaumplanungCore.Controllers;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using Xunit;

namespace DatabaseTest

/*Lösung https://www.thecodingforums.com/threads/method-write-does-not-have-an-implementation.85980/
 *             //http://dotnetliberty.com/index.php/2016/02/22/moq-on-net-core/
        //http://stackoverflow.com/questions/33490696/how-can-i-reset-an-ef7-inmemory-provider-between-unit-tests
    }
    */
{
    public class DatabaseTest
    {
        
        public DatabaseTest()
        {

        }

        public void InitContext()
        {

        }


        [Fact]
        public void Test_GetAllRooms()
        {

            DbContextOptionsBuilder<ReservationContext> optionsBuilder = new DbContextOptionsBuilder<ReservationContext>();
            //optionsBuilder.
            ReservationContext res = new ReservationContext(optionsBuilder.Options);
            DatabaseHandler databaseHandler = new DatabaseHandler(res);



            /*List<Room> rooms = new List<Room> {new Room {Name = "d1"}, new Room {Name = "d2"} };

            Mock<IDatabaseHandler> mock = new Mock<IDatabaseHandler>();
            mock.Setup(r => r.GetAllRooms()).Returns(rooms);

            Assert.Equal(2 , mock.Object.GetAllRooms().Count);*/
        }

        [Fact]
        public void Test_GetAllTeachers()
        {
            //int c = _databaseHandler.GetAllTeachers().Count;
            //Assert.True(c > 0);
        }

        [Fact]
        public void Test_AddRoom()
        {
            /*var mockSet = new Mock<DbSet<Room>>();
            mockSet.Setup(r => r.Add());
            Mock.

            var mockContext = new Mock<ReservationContext>();
            mockContext.Setup(m => m.Rooms).Returns(mockSet.Object);

            var service = new BlogService(mockContext.Object);
            service.A
            //service.AddBlog("ADO.NET Blog", "http://blogs.msdn.com/adonet");

            mockSet.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());*/
        }

        [Fact]
        public void Test_GetAllReservations()
        {
            //int c = _databaseHandler.GetAllReservations().Count;
           // Assert.True(c > 0);
        }

        [Fact]
        public void Test_GetReservationsOnDate()
        {
            //int c = _databaseHandler.GetAllReservations().Count;
           // Assert.True(c > 0);
        }
    }
}
