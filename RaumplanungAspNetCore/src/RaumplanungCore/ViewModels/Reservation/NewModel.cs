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

        public bool AllRoomsAvailable()
        {
            //_databaseHandler;
            return true;
        }

        public string FindReservationByDate(DateTime date)
        {
            List<Models.Reservation> reservations = _databaseHandler.GetReservationsOnDate(date);
            if (reservations.Count == 0)
            {
                return "green";
            }
            if (reservations.Count < _databaseHandler.GetAllRooms().Count)
            {
                return "yellow";
            }
            else
            {
                return "red";
            }
            
        }
    }
}
