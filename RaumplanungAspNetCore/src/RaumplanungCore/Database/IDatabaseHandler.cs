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

        List<Reservation> GetReservationsOnDate(DateTime date);
        List<Reservation> GetReservationsFromTeacher(string teacherId);
        List<Reservation> GetReservationsFromRoom(int roomId);
        List<Reservation> GetReservationsOnDateInBlock(DateTime date, int blockNr);
        
        bool DeleteReservation(int reservationId);
        bool AddReservation(DateTime date, int block, string teacherId, int roomId);
        Teacher GetTeacher(string teacherId);
        Room GetRoom(int roomId);
        Reservation GetReservation(int reservationId);
        List<Block> GetFreeRoomsOnDate(DateTime date);
        List<Room> GetFreeRoomsOnDateAndBlock(DateTime date, int block);
        bool CheckIfReservationsExistsOnDateInBlock(DateTime date, int block, int roomId);

        bool ExchangeReservation(string fromTeacher,int fromRoom , string toTeacher , int toRoom);
    }
}
