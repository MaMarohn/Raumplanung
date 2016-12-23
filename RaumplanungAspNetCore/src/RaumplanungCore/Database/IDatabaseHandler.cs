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
        List<Teacher> GetAllTeachers();
        List<Reservation> GetAllReservations();

        List<Reservation> GetReservationsWithDate(DateTime date);
        List<Reservation> GetReservationsFromTeacher(string teacherId);
        List<Reservation> GetReservationsFromRoom(int roomId);
        
        bool DeleteReservation(int reservationId);
        bool AddReservation(DateTime date, int block, string teacherId, int roomId);
        Teacher GetTeacher(string teacherId);
        Room GetRoom(int roomId);
        Reservation GetReservation(int reservationId);
        //List<Room> GetFreeRoomsOnDateAndBlock(DateTime date, int blockNr);
        List<Block> GetFreeRoomsOnDate(DateTime date);
        List<Room> GetFreeRoomsOnDateAndBlock(DateTime date, int block);
    }
}
