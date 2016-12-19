using System.Data.Entity;

namespace Raumplanung.Database
{
    public class MyDataContextDbInitializer : DropCreateDatabaseIfModelChanges<ReservationContext>
    {
        protected override void Seed(ReservationContext context)
        {
            context.Room.Add(new Room("Raum_Test"));
        }
    }
}
