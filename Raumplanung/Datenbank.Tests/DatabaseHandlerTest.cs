// <copyright file="DatabaseHandlerTest.cs">Copyright ©  2016</copyright>
using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Raumplanung;
using Raumplanung.Database;

namespace Raumplanung.Database.Tests
{
    /// <summary>Diese Klasse enthält parametrisierte Komponententests für DatabaseHandler.</summary>
    [PexClass(typeof(DatabaseHandler))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class DatabaseHandlerTest
    {
        /// <summary>Test-Stub für AddNewReservation(Room, Teacher, DateTime)</summary>
        [TestMethod]
        internal bool AddNewReservationTest(
            [PexAssumeUnderTest]DatabaseHandler target,
            Room r,
            Teacher t,
            DateTime d
        )
        {
            bool result = target.AddNewReservation(r, t, d);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.AddNewReservationTest(DatabaseHandler, Room, Teacher, DateTime) hinzufügen
        }

        /// <summary>Test-Stub für AddNewRoom(String)</summary>
        [PexMethod]
        internal bool AddNewRoomTest([PexAssumeUnderTest]DatabaseHandler target, string name)
        {
            bool result = target.AddNewRoom(name);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.AddNewRoomTest(DatabaseHandler, String) hinzufügen
        }

        /// <summary>Test-Stub für AddNewTeacher(String)</summary>
        [PexMethod]
        internal bool AddNewTeacherTest([PexAssumeUnderTest]DatabaseHandler target, string name)
        {
            bool result = target.AddNewTeacher(name);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.AddNewTeacherTest(DatabaseHandler, String) hinzufügen
        }

        /// <summary>Test-Stub für .ctor()</summary>
        [PexMethod]
        internal DatabaseHandler ConstructorTest()
        {
            DatabaseHandler target = new DatabaseHandler();
            return target;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.ConstructorTest() hinzufügen
        }

        /// <summary>Test-Stub für GetAllReservations()</summary>
        [PexMethod]
        internal List<Reservation> GetAllReservationsTest([PexAssumeUnderTest]DatabaseHandler target)
        {
            List<Reservation> result = target.GetAllReservations();
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetAllReservationsTest(DatabaseHandler) hinzufügen
        }

        /// <summary>Test-Stub für GetAllRooms()</summary>
        [PexMethod]
        internal List<Room> GetAllRoomsTest([PexAssumeUnderTest]DatabaseHandler target)
        {
            List<Room> result = target.GetAllRooms();
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetAllRoomsTest(DatabaseHandler) hinzufügen
        }

        /// <summary>Test-Stub für GetAllTeachersOrderedByReservations()</summary>
        [PexMethod]
        internal List<Teacher> GetAllTeachersOrderedByReservationsTest([PexAssumeUnderTest]DatabaseHandler target)
        {
            List<Teacher> result = target.GetAllTeachersOrderedByReservations();
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetAllTeachersOrderedByReservationsTest(DatabaseHandler) hinzufügen
        }

        /// <summary>Test-Stub für GetAllTeachers()</summary>
        [PexMethod]
        internal List<Teacher> GetAllTeachersTest([PexAssumeUnderTest]DatabaseHandler target)
        {
            List<Teacher> result = target.GetAllTeachers();
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetAllTeachersTest(DatabaseHandler) hinzufügen
        }

        /// <summary>Test-Stub für GetFreeRooms()</summary>
        [PexMethod]
        internal List<Room> GetFreeRoomsTest([PexAssumeUnderTest]DatabaseHandler target)
        {
            List<Room> result = target.GetFreeRooms();
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetFreeRoomsTest(DatabaseHandler) hinzufügen
        }

        /// <summary>Test-Stub für GetRoomByDate(DateTime)</summary>
        [PexMethod]
        internal List<Room> GetRoomByDateTest([PexAssumeUnderTest]DatabaseHandler target, DateTime date)
        {
            List<Room> result = target.GetRoomByDate(date);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetRoomByDateTest(DatabaseHandler, DateTime) hinzufügen
        }

        /// <summary>Test-Stub für GetRoomByName(String)</summary>
        [PexMethod]
        internal Room GetRoomByNameTest([PexAssumeUnderTest]DatabaseHandler target, string name)
        {
            Room result = target.GetRoomByName(name);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetRoomByNameTest(DatabaseHandler, String) hinzufügen
        }

        /// <summary>Test-Stub für GetRoom(Int32)</summary>
        [PexMethod]
        internal Room GetRoomTest([PexAssumeUnderTest]DatabaseHandler target, int id)
        {
            Room result = target.GetRoom(id);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetRoomTest(DatabaseHandler, Int32) hinzufügen
        }

        /// <summary>Test-Stub für GetTeacherByName(String)</summary>
        [PexMethod]
        internal Teacher GetTeacherByNameTest([PexAssumeUnderTest]DatabaseHandler target, string name)
        {
            Teacher result = target.GetTeacherByName(name);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetTeacherByNameTest(DatabaseHandler, String) hinzufügen
        }

        /// <summary>Test-Stub für GetTeacher(Int32)</summary>
        [PexMethod]
        internal Teacher GetTeacherTest([PexAssumeUnderTest]DatabaseHandler target, int id)
        {
            Teacher result = target.GetTeacher(id);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.GetTeacherTest(DatabaseHandler, Int32) hinzufügen
        }

        /// <summary>Test-Stub für RemoveReservation(Int32)</summary>
        [PexMethod]
        internal bool RemoveReservationTest([PexAssumeUnderTest]DatabaseHandler target, int id)
        {
            bool result = target.RemoveReservation(id);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.RemoveReservationTest(DatabaseHandler, Int32) hinzufügen
        }

        /// <summary>Test-Stub für RemoveRoom(String)</summary>
        [PexMethod]
        internal bool RemoveRoomTest([PexAssumeUnderTest]DatabaseHandler target, string name)
        {
            bool result = target.RemoveRoom(name);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.RemoveRoomTest(DatabaseHandler, String) hinzufügen
        }

        /// <summary>Test-Stub für RemoveTeacher(String)</summary>
        [PexMethod]
        internal bool RemoveTeacherTest([PexAssumeUnderTest]DatabaseHandler target, string name)
        {
            bool result = target.RemoveTeacher(name);
            return result;
            // TODO: Assertionen zu Methode DatabaseHandlerTest.RemoveTeacherTest(DatabaseHandler, String) hinzufügen
        }
    }
}
