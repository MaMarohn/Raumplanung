﻿using System;
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

            const int countRoom = 20;
            if (context.Rooms.Count() < countRoom)
            {
                //Datenbank wird neu gefüllt
                foreach (var entity in context.Rooms)
                    context.Rooms.Remove(entity);
                context.SaveChanges();

                var rooms = new Room[countRoom];
                for (var c = 0; c < countRoom; c++)
                {
                    rooms[c] = new Room {Name = "Raum D" + c};
                }

                foreach (Room s in rooms)
                {
                    context.Rooms.Add(s);
                }
                context.SaveChanges();
            }

            const int countTeacher = 150;
            if (context.Teachers.Count() < countTeacher)
            {
                //Datenbank wird neu gefüllt
                foreach (var entity in context.Teachers)
                    context.Teachers.Remove(entity);
                context.SaveChanges();

                var teachers = new Teacher[countTeacher];
                for (var c = 0; c < countTeacher; c++)
                {
                    teachers[c] = new Teacher { UserName = "Lehrer " + c };
                }

                foreach (Teacher s in teachers)
                {
                    context.Teachers.Add(s);
                }
            }
            context.SaveChanges();

            /*var countT = context.Teachers.Count();
            if (context.Reservations.Count() < countT)
            {
                //Datenbank wird neu gefüllt
                foreach (var entity in context.Reservations)
                    context.Reservations.Remove(entity);
                context.SaveChanges();

                
                var teachers = new List<Teacher>(context.Teachers);
                var rooms = new List<Room>(context.Rooms);
                Random random = new Random();

                for (int day = 23; day <= 31; day++)
                {
                    for (int blockNr = 1; blockNr <= 8; blockNr++)
                    {                                          
                        rooms.Shuffle();
                        teachers.Shuffle();
                        for (int x = 0; x < random.Next(rooms.Count) ; x++) {
                            var r = new Reservation
                            {
                                TeacherId = teachers[x].Id,
                                RoomId = rooms[x].RoomId,
                                Date = new DateTime(2016, 12, day),
                                Block = blockNr
                            };


                            teachers[x].Reservations.Add(r);
                            rooms[x].Reservations.Add(r);

                            context.Reservations.Add(r);
                        }
                    }
                    context.SaveChanges();
                }               
            }*/
            context.Rooms.Include(r => r.Reservations);
            context.Teachers.Include(r => r.Reservations);
            context.SaveChanges();
        }


        private static readonly Random rng = new Random();
        private static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}
