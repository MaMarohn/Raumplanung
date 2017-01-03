using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RaumplanungCore.Models;
using RaumplanungCore.Models.Roles;
using static Microsoft.EntityFrameworkCore.Storage.Internal.InMemoryTransactionManager;

namespace RaumplanungCore.Database
{
    public class ReservationContext : IdentityDbContext<Teacher>
    {
        public ReservationContext(DbContextOptions options) : base(options)
        {
            
        }
       
        public DbSet<RoleAdmin> Admins { set; get; }
        public DbSet<Room> Rooms { set; get; }
        public DbSet<Teacher> Teachers { set; get; }
        public DbSet<Reservation> Reservations { set; get; }
        public DbSet<ExchangeReservation> ExchangeReservations { set; get; }
        public DbSet<Course> Courses { set; get; }

        public DbSet<BlockNrAndRoomAndWeekday> BlockNrAndRoomAndWeekdays { set; get; }
    }
}
