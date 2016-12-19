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
