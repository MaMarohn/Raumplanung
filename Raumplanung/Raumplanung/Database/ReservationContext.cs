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
        public DbSet<Room> Rooms { set; get; }
        public DbSet<Teacher> Teachers { set; get; }
        public DbSet<Reservation> Reservations { set; get; }

        public ReservationContext() : base("ReservationService")
        {
            DatabaseInitializer databaseInitializer = new DatabaseInitializer();
            databaseInitializer.fillDatabase(this);
            databaseInitializer.fillDatabaseWithTestData(this);
        }
    }
}
