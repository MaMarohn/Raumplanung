using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Database;

namespace Raumplanung
{
    partial class ReservationContext
    {
        public void FillDatabase()
        {
            DatabaseInitializer databaseInitializer = new DatabaseInitializer();
            databaseInitializer.FillDatabase(this);
            databaseInitializer.FillDatabaseWithTestData(this);
        }
    }
}
