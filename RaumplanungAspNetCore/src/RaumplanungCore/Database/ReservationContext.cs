using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaumplanungCore.Models;
using RaumplanungCore.Models.Roles;

namespace RaumplanungCore.Database
{
    public class ReservationContext : IdentityDbContext<Teacher>
    {
        public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
        {
            
        }
       

        public DbSet<RoleAdmin> Admins { set; get; }
        public DbSet<Room> Rooms { set; get; }
        public DbSet<Teacher> Teachers { set; get; }
        public DbSet<Reservation> Reservations { set; get; }
    }
}
