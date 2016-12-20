using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaumplanungCore.Models;

/*
 * https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro
 */

namespace RaumplanungCore.Database
{
    public static class DbInitializer
    {
        public static void Initialize(ReservationContext context)
        {
            context.Database.EnsureCreated();

            
            if (context.Rooms.Any())
            {
                return; // DB has been seeded
            }

            var rooms = new Room[]
            {
                new Room {Name = "Room1"},
                new Room {Name = "Room3"},
            };
            foreach (Room s in rooms)
            {
                context.Rooms.Add(s);
            }
            context.SaveChanges();
        }
    }
}
