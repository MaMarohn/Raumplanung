using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaumplanungCore.Models;

namespace Raumplanung.Database
{
    interface IDatabaseHandler
    {
        List<Room> GetAllRooms();
        List<Room> GetAllFreeRooms(DateTime date, int block);
        List<Reservation> GetAllReservations();
        List<Reservation> GetReservationsWithDate(DateTime date);
        List<Reservation> GetReservationsFromTeacher(string teacherId);
        List<Reservation> GetReservationsFromRoom(int roomId);
        List<Teacher> GetAllTeachers();
        bool DeleteReservation(int reservationId);

        bool AddReservation(DateTime date, int block, int teacherId, int roomId);
        /*

        List<Room> GetFreeRooms();
        List<Room> GetRoomByDate(DateTime date);
        List<Teacher> GetAllTeachersOrderedByReservations();
        Teacher GetTeacher(int id);
        Teacher GetTeacherByName(String name);
        Room GetRoom(int id);
        Room GetRoomByName(String name);

        Boolean AddNewTeacher(String name);
        Boolean AddNewRoom(String name);
        Boolean AddNewReservation(Room r , Teacher t , DateTime d);

        Boolean RemoveRoom(String name);
        Boolean RemoveTeacher(String name);
        Boolean RemoveReservation(int id);*/

    }
}
