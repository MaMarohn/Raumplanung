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
    public class DatabaseHandlerTest
    {
        /// <summary>Test-Stub für GetAllRooms()</summary>
        //[PexMethod]
        [TestMethod()]
        public void GetAllRoomsTest([PexAssumeUnderTest]DatabaseHandler target)
        {
            List<Room> result = target.GetAllRooms();
            //return result;
            Assert.IsNotNull(result);
        }
    }
}
