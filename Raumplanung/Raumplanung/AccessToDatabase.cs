using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Database;

namespace Raumplanung
{
    class AccessToDatabase
    {
        /*
         * Es dient nur als ein Beispiel , wie man auf die Datenbank zugreifen kann.
         * Kann gelöscht werden!!!
         * 
         * 
         * 
         */

        public AccessToDatabase()
        {
            DatabaseHandler databaseHandler = new DatabaseHandler();
            databaseHandler.GetRoom(1); // liefert raum mit dem id 1
        }
    }
}
