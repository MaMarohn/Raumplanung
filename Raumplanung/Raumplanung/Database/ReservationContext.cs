using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Database;
using Raumplanung.Entities;

namespace Raumplanung
{
    class ReservationContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Teacher> Teachers { get; set;  }
        public DbSet<Reservation> Reservations { get; set;  }

        public ReservationContext() : base("ReservationService")
        {
            DatabaseInitializer databaseInitializer = new DatabaseInitializer();
            databaseInitializer.FillDatabase(this);
            databaseInitializer.FillDatabaseWithTestData(this);
        }
    }
}
