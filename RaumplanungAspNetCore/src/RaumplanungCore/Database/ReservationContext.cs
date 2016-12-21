using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaumplanungCore.Models;

namespace RaumplanungCore.Database
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {
            
        }

        public DbSet<Room> Rooms { set; get; }
        public DbSet<Teacher> Teachers { set; get; }
        public DbSet<Reservation> Reservations { set; get; }
    }
}
