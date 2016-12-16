using System;
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

        public void FillDatabase(ReservationContext context)
        {
            if (!context.Rooms.Any())
            {
                for (var i = 1; i <= 12; i++)
                {
                    context.Rooms.Add(new Room("Raum" + i));
                }
                context.SaveChanges();
            } 
        }

        public void FillDatabaseWithTestData(ReservationContext context)
        {
            if (!context.Teachers.Any())
            {
                Random random = new Random();
                for (var index = 0; index < 20; index++)
                {
                    context.Teachers.Add(new Teacher("Teacher" + index + 1, random.Next(1, 20)));
                }
                context.SaveChanges();
            }
        }
    }
}
