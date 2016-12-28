using System;
using System.Collections.Generic;
using Raumplanung.Database;
using RaumplanungCore.Database;


namespace RaumplanungCore.ViewModels.Reservation
{
    public class NewModel
    {
        private readonly DatabaseHandler _databaseHandler;

        public NewModel(ReservationContext context)
        {
            _databaseHandler = new DatabaseHandler(context);
        }

        public bool teacherHasAlreadyReservationAtDateAndBlock(string teacherId, DateTime dateTime, int block)
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

    }
}
