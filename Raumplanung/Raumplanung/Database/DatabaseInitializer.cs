using System;
using System.Collections.Generic;
using System.Linq;

namespace Raumplanung.Database
{
    class DatabaseInitializer
    {
        public void FillDatabase(ReservationContext context)
        {
            if (!context.Room.Any())
            {
                for (var i = 1; i <= 12; i++)
                    context.Room.Add(new Room("Raum" + i));               
            }

            context.SaveChanges();
        }

        public void FillDatabaseWithTestData(ReservationContext context)
        {
            if (!context.Teacher.Any())
            {
                for (var i = 1; i <= 12; i++)
                    context.Teacher.Add(new Teacher("Teacher" + i));
            }

            context.SaveChanges();


            if (context.Reservation.Any()) return;
            {
                List<Room> r = new List<Room>(context.Room);
                List<Teacher> t = new List<Teacher>(context.Teacher);

                for (var i = 0; i < 12; i++)
                {
                    DateTime d = new DateTime(2016, 12, i + 1);
                    Reservation res = new Reservation(r[i].RoomID, t[i].TeacherID, d);
                    context.Reservation.Add(res);
                    r[i].Reservation.Add(res);
                    t[i].Reservation.Add(res);
                    context.SaveChanges();
                }
                
            }
        }
    }
}
