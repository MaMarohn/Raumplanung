using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Entities;

namespace Raumplanung
{
    class ReservationContext : DbContext
    {

        public DbSet<Room> Rooms { set; get; }
        private DbSet<Teacher> _teachers;
        private DbSet<Reservation> _reservations;

        public ReservationContext() : base("ReservationService")
        {
            
        }
    }
}
