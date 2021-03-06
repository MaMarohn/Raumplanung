﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RaumplanungCore.Models;
using RaumplanungCore.ViewModels.Kurs;

[assembly: InternalsVisibleTo("DatabaseTest")]
namespace Raumplanung.Database
{
    public interface IDatabaseHandler
    {
        List<Room> GetAllRooms();
        List<Teacher> GetAllTeachers();
        List<Reservation> GetAllReservations();

        List<Reservation> GetReservationsOnDate(DateTime date);
        List<Reservation> GetReservationsFromTeacher(string teacherId);
        List<Reservation> GetReservationsFromRoom(int roomId);
        List<Reservation> GetReservationsOnDateInBlock(DateTime date, int blockNr);
        List<Reservation> GetAllReservationsFromCourse(int courseId);
        
        bool DeleteReservation(int reservationId);
        bool AddReservation(DateTime date, int block, string teacherId, int roomId);
        bool AddRoom(string name);
        Teacher GetTeacher(string teacherId);
        Room GetRoom(int roomId);
        Reservation GetReservation(int reservationId);
        List<Block> GetFreeRoomsOnDate(DateTime date);
        List<Room> GetFreeRoomsOnDateAndBlock(DateTime date, int block);
        bool CheckIfReservationsExistsOnDateInBlock(DateTime date, int block, int roomId);

        bool ExchangeReservation(string fromTeacher,int fromRoom , string toTeacher , int toRoom);
        
        List<ExchangeReservation> GetExchangeReservationByTeacherFromId(string id);
        List<ExchangeReservation> GetExchangeReservationByTeacherToId(string id);

        bool AddCourse(List<DateandRoom> dateandRooms, DateTime startDate,DateTime endDate, string courseName, string teacherId);
        bool AddCourse(DateTime startDate, DateTime endTime, int block, string teacherId, int room, string nameOfCourse , int dayOfWeek);
        List<Course> GetAllCourses();
        bool DeleteCourse(int id);
        Course GetCourseById(int id);
        List<Course> GetCoursesFromTeacher(string teacherId);
        ExchangeReservation GetExchangeReservationById(int id);
        bool DeleteExchangeReservationById(int id);
        bool DeleteExchangeReservationByObject(ExchangeReservation exchangeReservation);
        bool AddReservationSuggestion(string teacherFrom, int reservationFrom, string teacherTo, int reservationoffer, string message);
        
    }
}
