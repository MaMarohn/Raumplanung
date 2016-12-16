using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raumplanung.Entities;

namespace Raumplanung.Database
{
    interface IDatabaseHandler
    {
        List<Room> GetAllRooms();
        List<Teacher> GetAllTeachers();
        List<Reservation> GetAllReservations();

        List<Room> GetFreeRooms();
        List<Room> GetRoomByDate(DateTime date);
        List<Teacher> GetAllTeachersOrderedByReservations();
        Teacher GetTeacher(int id);
        Teacher GetTeacherByName(String name);
        Room GetRoom(int id);
        Room GetRoomByName(String name);

        Boolean AddNewTeacher(String name);
        Boolean AddNewRoom(String name);
        Boolean AddNewReservation();

        Boolean RemoveRoom(String name);
        Boolean RemoveTeacher(String name);
        Boolean RemoveReservation(int id);

    }
}
