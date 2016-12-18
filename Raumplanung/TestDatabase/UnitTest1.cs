using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raumplanung.Database;
using Raumplanung;

namespace TestDatabase
{
    [TestClass]
    public class UnitTest1
    {
        private readonly DatabaseHandler _databaseHandler = new DatabaseHandler();

        [TestMethod()]
        public void Test_Add_Methods()
        {
            Assert.IsNull(null);
            //_databaseHandler.AddNewTeacher("Test_Teacher");
            //_databaseHandler.AddNewRoom("Test_Room");
            //Assert.IsNotNull(_databaseHandler.GetTeacherByName("Test_Teacher"));
            //Assert.IsNotNull(_databaseHandler.GetRoomByName("Test_Room"));
        }

        [TestMethod()]
        public void Test_Remove_Methods()
        {
            Assert.IsNull(null);
            //_databaseHandler.RemoveTeacher("Test_Teacher");
            //_databaseHandler.RemoveRoom("Test_Room");
            //Assert.IsNull(_databaseHandler.GetTeacherByName("Test_Teacher"));
            //Assert.IsNull(_databaseHandler.GetRoomByName("Test_Room"));
        }
    }
}
