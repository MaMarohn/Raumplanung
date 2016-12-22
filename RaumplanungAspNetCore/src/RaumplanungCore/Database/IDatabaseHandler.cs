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
        List<Reservation> GetReservationsFromTeacher(int teacherId);
        List<Reservation> GetReservationsFromRoom(int roomId);
        List<Teacher> GetAllTeachers();
        bool DeleteReservation(int reservationId);
        bool AddReservation(DateTime date, int block, int teacherId, int roomId);
        Teacher GetTeacher(int teacherId);
        Room GetRoom(int roomId);
        Reservation GetReservation(int reservationId);
    }
}
