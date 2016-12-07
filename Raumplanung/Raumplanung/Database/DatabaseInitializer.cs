using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Raumplanung.Database
{
    class DatabaseInitializer : DropCreateDatabaseAlways<ReservationContext>
    {
        protected override void Seed(ReservationContext context)
        {
            for (int i = 1; i <= 12; i++)
            {
                context.Rooms.Add(new Room("Raum" + i));
            }
            context.SaveChanges();

            base.Seed(context);
        }

        public void fillDatabase(ReservationContext context)
        {
            if (!context.Rooms.Any())
            {
                for (int i = 1; i <= 12; i++)
                {
                    context.Rooms.Add(new Room("Raum" + i));
                }
                context.SaveChanges();
            } 
        }
    }
}
