using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
