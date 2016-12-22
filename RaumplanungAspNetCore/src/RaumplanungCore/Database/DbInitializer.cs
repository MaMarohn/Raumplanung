using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

            const int countRoom = 12;
            if (context.Rooms.Count() < 12)
            {
                //Datenbank wird neu gefüllt
                foreach (var entity in context.Rooms)
                    context.Rooms.Remove(entity);
                context.SaveChanges();

                var rooms = new Room[countRoom];
                for (var c = 0; c < countRoom; c++)
                {
                    rooms[c] = new Room {Name = "Room" + c};
                }

                foreach (Room s in rooms)
                {
                    context.Rooms.Add(s);
                }
                context.SaveChanges();
            }

            const int countTeacher = 100;
            //Console.WriteLine(context.Rooms.Count());
            if (context.Teachers.Count() < countTeacher - 1)
            {
                //Datenbank wird neu gefüllt
                foreach (var entity in context.Teachers)
                    context.Teachers.Remove(entity);
                context.SaveChanges();

                var teachers = new Teacher[countTeacher];
                for (var c = 0; c < countTeacher; c++)
                {
                    teachers[c] = new Teacher {Name = "Teacher" + c};
                }

                foreach (Teacher s in teachers)
                {
                    context.Teachers.Add(s);
                }
            }
            context.SaveChanges();

            const int countReservation = 10;
            if (context.Reservations.Count() < countReservation)
            {
                //Datenbank wird neu gefüllt
                foreach (var entity in context.Reservations)
                    context.Reservations.Remove(entity);
                context.SaveChanges();

                List<Teacher> teachers = new List<Teacher>(context.Teachers);
                List<Room> rooms = new List<Room>(context.Rooms);

                for (int c = 0; c < rooms.Count; c++)
                {
                    Teacher t = teachers[c];
                    Room r = rooms[c];

                    Reservation reservation = new Reservation();
                    reservation.Date = new DateTime(2016,12,20);
                    reservation.Teacher = t;
                    reservation.Room = r;
                    
                    t.Reservations.Add(reservation);
                    r.Reservations.Add(reservation);

                    context.Entry(t).State = EntityState.Modified;
                    context.Entry(r).State = EntityState.Modified;

                    context.Reservations.Add(reservation);

                    context.SaveChanges();
                }
            }
            context.SaveChanges();
        }

    }
}
