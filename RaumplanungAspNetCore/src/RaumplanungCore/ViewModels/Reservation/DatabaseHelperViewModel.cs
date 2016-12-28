using System;
using System.Collections.Generic;
using Raumplanung.Database;
using RaumplanungCore.Database;


namespace RaumplanungCore.ViewModels.Reservation
{
    public class DatabaseHelperViewModel
    {
        private readonly DatabaseHandler _databaseHandler;

        public DatabaseHelperViewModel(ReservationContext context)
        {
            _databaseHandler = new DatabaseHandler(context);
        }

        public bool TeacherHasAlreadyReservationAtDateAndBlock(string teacherId, DateTime dateTime, int block)
        {
            List<Models.Reservation> reservations = _databaseHandler.GetReservationsFromTeacher(teacherId);
            foreach (var reservation in reservations)
            {
                if ((reservation.Date.Value.Month == dateTime.Month && reservation.Date.Value.Day == dateTime.Day && reservation.Date.Value.Year == dateTime.Year) && reservation.Block == block)
                {
                    return true;
                }
            }
            return false;
        }

        public string GetCourseNameById(int courseId)
        {
            string result = "placeholder";
            // TODO: Muss später möglich sein: result =  _databaseHandler.GetCourseById(courseId).Name;
            return result;
        }

        public string GetRoomNameById(int roomId)
        {
            string result = "";
            result = _databaseHandler.GetRoom(roomId).Name;
            return result;
        }

    }
}
