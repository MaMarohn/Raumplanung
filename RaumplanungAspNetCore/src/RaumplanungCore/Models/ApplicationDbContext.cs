﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace RaumplanungCore.Models
{
    /*public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=Reservation;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }*/
}
