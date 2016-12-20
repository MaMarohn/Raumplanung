using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
