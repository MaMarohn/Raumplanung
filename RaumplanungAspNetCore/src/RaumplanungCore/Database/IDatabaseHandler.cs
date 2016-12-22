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
        List<Room> GetAllFreeRoomsOnDate(DateTime date, int block);
        List<Reservation> GetAllReservations();
        List<Reservation> GetReservationsWithDate(DateTime date);
        List<Reservation> GetReservationsFromTeacher(string teacherId);
        List<Reservation> GetReservationsFromRoom(int roomId);
        List<Teacher> GetAllTeachers();
        bool DeleteReservation(int reservationId);
        bool AddReservation(DateTime date, int block, string teacherId, int roomId);
        Teacher GetTeacher(string teacherId);
        Room GetRoom(int roomId);
        Reservation GetReservation(int reservationId);
    }
}
