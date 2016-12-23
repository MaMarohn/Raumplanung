﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using RaumplanungCore.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RaumplanungCore.Controllers
{
    [Authorize]
    [Authorize(Policy = "ConfirmedEmailOnly")]
    public class ReservationController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;

        public ReservationController(ReservationContext context)
        {
            _databaseHandler = new DatabaseHandler(context);
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {

            List<Block> blocks = _databaseHandler.GetFreeRoomsOnDate(new DateTime(2016, 12,20));
            //List<Reservation> reservations = _databaseHandler.GetAllReservations();
            //HIER:var userId = User.Identity.GetUserId(); oder ähnliches
            List<Reservation> reservations = _databaseHandler.GetReservationsFromTeacher("0b5b8029-45f1-4314-aa08-b23f25f6af03");
            int count;
            if (reservations != null)
            {
                count = reservations.Count;
            }
            else
            {
                //sonst gibts null exception
                reservations = new List<Reservation>();
            }

            return View("Index", reservations);
        }

        // GET: /<controller>/Detail/reservationId
        //[HttpGet("reservation/detail/{reservationId}")]
        public IActionResult Detail(int reservationId)
        {
            ViewData["ReservationId"] = reservationId;
            return View();
        }

        [HttpGet("reservation/delete/{reservationId}")]
        public IActionResult Delete(int reservationId)
        {
            _databaseHandler.DeleteReservation(reservationId);
            List<Room> rooms = _databaseHandler.GetAllRooms();
            return View("Index", rooms);
        }

        [HttpGet("reservation/New")]
        public IActionResult New()
        {
            return View();
        }

        //[HttpGet("reservation/LoadEvents/{start, end}")]
        public IActionResult LoadEvents(DateTime start, DateTime end)
        {
            var eventList = GetEvents(start, end);
            var rows = eventList.ToArray();
            
            return Json(rows);
        }

        private List<CalendarEvent> GetEvents(DateTime start, DateTime end)
        {            
            List<CalendarEvent> eventList = new List<CalendarEvent>();                  
            int amountOfBlocks = 7;
            string[] dayStrings = {"Mo", "Di", "Mi", "Do", "Fr"};
            string[] blockStartArray = {"08:30", "10:15", "12:00", "14:15", "16:00", "17:45", "19:30"};
            string[] blockEndArray = {"10:00", "11:45", "13:30", "15:45", "17:30", "19:15", "21:00"};            
            for (int j = 0; j < dayStrings.Length; j++)
            {
                int[] days = {j+1};
                for (int i = 0; i < amountOfBlocks ; i++)
                {
                    CalendarEvent dailyEvent = new CalendarEvent((dayStrings[j] + (i + 1)), blockStartArray[i], blockEndArray[i], days, FindReservationByDate(start, i));
                    eventList.Add(dailyEvent);
                }
                start = start.AddDays(1);
            }                        
            return eventList;            
        }

        [HttpGet("reservation/OnClick/{start}")]
        public IActionResult OnClick(DateTime start)
        {
            List<Block> bloecke = _databaseHandler.GetFreeRoomsOnDate(start);
            int blockId = calculateBlock(start);
            //List<Reservation> reservations = bloecke[blockId].FreeRooms
            return View("block");
        }

        private int calculateBlock(DateTime start)
        {
            return 1;
        }

        private string FindReservationByDate(DateTime date, int blockNr)
        {
            List<Block> bloecke = _databaseHandler.GetFreeRoomsOnDate(date);
            if (bloecke[blockNr].FreeRooms.Count == 0)
            {
                return "red";
            }
            if (bloecke[blockNr].FreeRooms.Count < _databaseHandler.GetAllRooms().Count)
            {
                return "yellow";
            }
            else
            {
                return "green";
            }
        }
    }
}
