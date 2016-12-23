using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using RaumplanungCore.ViewModels;
using RaumplanungCore.Models.Roles;
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
        int amountOfBlocks = 7;
        string[] dayStrings = { "Mo", "Di", "Mi", "Do", "Fr" };
        string[] blockStartArray = { "08:30", "10:15", "12:00", "14:15", "16:00", "17:45", "19:30" };
        string[] blockEndArray = { "10:00", "11:45", "13:30", "15:45", "17:30", "19:15", "21:00" };

        public ReservationController(ReservationContext context, UserManager<Teacher> userManager)
        {
            _databaseHandler = new DatabaseHandler(context);
            _userManager = userManager;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            //HIER:var userId = User.Identity.GetUserId(); oder ähnliches
            //List<Reservation> reservations = _databaseHandler.GetReservationsFromTeacher("0b5b8029-45f1-4314-aa08-b23f25f6af03");
            string teacherId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            List<Reservation> reservations = _databaseHandler.GetReservationsFromTeacher(teacherId);//_userManager.FindByNameAsync(User.Identity.Name).Result.Id);
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

        [HttpGet("reservation/OnClick/{starts}")]
        public IActionResult OnClick(string starts)
        {
            DateTime start = DateTime.Parse(starts);
            int blockId = calculateBlock(starts);
            /*
             * TO-DO
             * GetReservationsOnDateAndBlock()
             * SortierteListe nach RaumNR
             */
            List<Reservation> reservations = _databaseHandler.GetReservationsWithDate(start);
            List<Reservation> reservationsInBlock = new List<Reservation>();
            foreach (var reservation in reservations)
            {
                if (reservation.Block == blockId)
                {
                    reservationsInBlock.Add(reservation);
                }
            }
            List<RaumbelegungModel> raumbelegung = new List<RaumbelegungModel>();
            foreach (var room in _databaseHandler.GetAllRooms())
            {
                bool found = false;
                foreach (var reservation in reservationsInBlock)
                {                
                    if (reservation.RoomId == room.RoomId)
                    {
                        Teacher teacher = _databaseHandler.GetTeacher(reservation.TeacherId);
                        raumbelegung.Add(new RaumbelegungModel(room, true, teacher, start, blockId));
                        found = true;
                    }
                }
                if (!found)
                {
                    raumbelegung.Add(new RaumbelegungModel(room, false, null, start, blockId));
                }
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
        
        private int calculateBlock(string start)
        {
            string onlyTime = start.Split(' ')[1];
            int blockId = 1;
            foreach (var startTime in blockStartArray)
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
