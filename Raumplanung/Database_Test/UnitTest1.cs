using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raumplanung.Database;

namespace Database_Test
{
    [TestClass]
    public class UnitTest1
    {

        private readonly DatabaseHandler _databaseHandler = new DatabaseHandler();

        [TestMethod()]
        public void Test_Add_Methods()
        {
            _databaseHandler.AddNewTeacher("Test_Teacher");
            _databaseHandler.AddNewRoom("Test_Room");
            Assert.IsNotNull(_databaseHandler.GetTeacherByName("Test_Teacher"));
            Assert.IsNotNull(_databaseHandler.GetRoomByName("Test_Room"));
        }

        [TestMethod()]
        public void Test_Remove_Methods()
        {
            _databaseHandler.RemoveTeacher("Test_Teacher");
            _databaseHandler.RemoveRoom("Test_Room");
            Assert.IsNull(_databaseHandler.GetTeacherByName("Test_Teacher"));
            Assert.IsNull(_databaseHandler.GetRoomByName("Test_Room"));
        }
    }
}
