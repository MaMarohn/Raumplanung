using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using RaumplanungCore.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RaumplanungCore.Controllers
{
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

        [HttpGet("reservation/LoadEvents")]
        public IActionResult LoadEvents()
        {
            var eventList = GetEvents();
            var rows = eventList.ToArray();
            
            return Json(rows);
        }

        private List<CalendarEvent> GetEvents()
        {
            List<CalendarEvent> eventList = new List<CalendarEvent>();                  
            int amountOfBlocks = 3;
            string[] dayStrings = {"Mo", "Di", "Mi", "Do", "Fr"};
            string[] blockStartArray = {"08:30", "10:15", "12:00"};
            string[] blockEndArray = {"10:00", "11:45", "13:30"};
            for (int j = 0; j < dayStrings.Length; j++)
            {
                int[] days = {j+1};
                for (int i = 0; i < amountOfBlocks ; i++)
                {
                    CalendarEvent dailyEvent = new CalendarEvent((dayStrings[j] + (i + 1)), blockStartArray[i], blockEndArray[i], days, "green");
                    eventList.Add(dailyEvent);
                }
            }                        
            return eventList;            
        }

        private string FindReservationByDate(DateTime date)
        {
            List<Models.Reservation> reservations = _databaseHandler.GetReservationsWithDate(date);
            if (reservations.Count == 0)
            {
                return "green";
            }
            if (reservations.Count < _databaseHandler.GetAllRooms().Count)
            {
                return "yellow";
            }
            else
            {
                return "red";
            }

        }
    }
}
