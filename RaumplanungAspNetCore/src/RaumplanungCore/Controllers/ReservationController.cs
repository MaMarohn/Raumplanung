using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using RaumplanungCore.ViewModels;
using Microsoft.AspNetCore.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RaumplanungCore.Controllers
{
    [Authorize]
    [Authorize(Policy = "ConfirmedEmailOnly")]
    public class ReservationController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly UserManager<Teacher> _userManager;
        const int AmountOfBlocks = 7;
        readonly string[] _dayStrings = { "Mo", "Di", "Mi", "Do", "Fr" };
        readonly string[] _blockStartArray = { "08:30", "10:15", "12:00", "14:15", "16:00", "17:45", "19:30" };
        readonly string[] _blockEndArray = { "10:00", "11:45", "13:30", "15:45", "17:30", "19:15", "21:00" };

        public ReservationController(ReservationContext context, UserManager<Teacher> userManager)
        {
            _databaseHandler = new DatabaseHandler(context);
            _userManager = userManager;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            string teacherId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            List<Reservation> reservations = _databaseHandler.GetReservationsFromTeacher(teacherId);
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
                 
            for (int j = 0; j < _dayStrings.Length; j++)
            {
                int[] days = {j+1};
                for (int i = 0; i < AmountOfBlocks ; i++)
                {
                    CalendarEvent dailyEvent = new CalendarEvent((_dayStrings[j] + (i + 1)), _blockStartArray[i], _blockEndArray[i], days, FindReservationByDate(start, i));
                    eventList.Add(dailyEvent);
                }
                start = start.AddDays(1);
            }                        
            return eventList;            
        }

        [HttpGet("reservation/OnClick/{starts}")]
        public IActionResult OnClick(string starts)
        { 
            DateTime start = DateTime.Parse(starts);
            int blockId = CalculateBlock(starts);
            List<Reservation> reservationsInBlock = _databaseHandler.GetReservationsOnDateInBlock(start, blockId);
            List<RaumbelegungModel> raumbelegung = new List<RaumbelegungModel>();

            foreach (var room in _databaseHandler.GetAllRooms())
            {
                Reservation rr;
                raumbelegung.Add((rr = reservationsInBlock.Find(r => r.RoomId == room.RoomId)) != null
                    ? new RaumbelegungModel(room, true, _databaseHandler.GetTeacher(rr.TeacherId), start, blockId)
                    : new RaumbelegungModel(room, false, null, start, blockId));
            }                     
            return View("block", raumbelegung);
        }

        public IActionResult Tauschen(string teacherId)
        {
            return View();
        }


        [HttpGet("reservation/Reservieren/{id}")]
       public IActionResult Reservieren(string id)
        {
            string[] splitted = id.Split(';');
            DateTime date = DateTime.Parse(splitted[0]);
            int block = Convert.ToInt32(splitted[1]);
            string teacherId = splitted[2];
            int roomId = Convert.ToInt32(splitted[3]);
            bool succes =_databaseHandler.AddReservation(date, block, teacherId, roomId);
            return View("New");
        }
        
        private int CalculateBlock(string start)
        {
            string onlyTime = start.Split(' ')[1];
            //int blockId = 1;
            int blockId = 0;
            foreach (var startTime in _blockStartArray)
            {
                if (onlyTime.Equals(startTime))
                {
                    return blockId;
                }
                blockId++;
            }
            return 1;
        }

        private string FindReservationByDate(DateTime date, int blockNr)
        {
            List<Room> block = _databaseHandler.GetFreeRoomsOnDateAndBlock(date , blockNr);
            if (block.Count == 0)
            {
                return "red";
            }
            if (block.Count < _databaseHandler.GetAllRooms().Count)
            {
                return "orange";
            }
            else
            {
                return "green";
            }
        }
    }
}
