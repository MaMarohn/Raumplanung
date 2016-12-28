using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using Raumplanung.Database;
using RaumplanungCore.Database;
using Xunit;

namespace DatabaseTest
{
    public class DatabaseTest
    {
        //https://msdn.microsoft.com/en-us/library/dn314429%28v=vs.113%29.aspx
        private readonly DatabaseHandler _databaseHandler;

        public DatabaseTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReservationContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Reservation;" +
                                        "Trusted_Connection=True;MultipleActiveResultSets=true");
            _databaseHandler = new DatabaseHandler(new ReservationContext(optionsBuilder.Options));



            
            //http://dotnetliberty.com/index.php/2016/02/22/moq-on-net-core/
        }


        [Fact]
        public void Test_GetAllRooms()
        {
            int c = _databaseHandler.GetAllRooms().Count;
            Assert.True(c > 0);
        }

        [Fact]
        public void Test_GetAllTeachers()
        {
            int c = _databaseHandler.GetAllTeachers().Count;
            Assert.True(c > 0);
        }

        [Fact]
        public void Test_GetAllReservations()
        {
            int c = _databaseHandler.GetAllReservations().Count;
            Assert.True(c > 0);
        }

        [Fact]
        public void Test_GetReservationsOnDate()
        {
            int c = _databaseHandler.GetAllReservations().Count;
            Assert.True(c > 0);
        }
    }
}
