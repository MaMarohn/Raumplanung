using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using RaumplanungCore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RaumplanungCore.ViewModels.Reservation;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RaumplanungCore.Controllers
{
    [Authorize]
    [Authorize(Policy = "ConfirmedEmailOnly")]
    public class ReservationController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly UserManager<Teacher> _userManager;
        private readonly ReservationContext _reservationContext;                

        public ReservationController(ReservationContext context, UserManager<Teacher> userManager)
        {
            _reservationContext = context;
            _databaseHandler = new DatabaseHandler(context);
            _userManager = userManager;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            string teacherId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            List<Reservation> reservations = _databaseHandler.GetReservationsFromTeacher(teacherId);
            List<Course> courses = _databaseHandler.GetAllCourses();
            int count;
            if (reservations != null)
            {
                count = reservations.Count;
                foreach(Reservation r in reservations)
                {
                    r.Room = _databaseHandler.GetRoom(r.RoomId);
                }
            }
            else
            {
                //sonst gibts null exception
                reservations = new List<Reservation>();
            }          
           
            return View("Index", new ReservationAndCourse(reservations, courses));
        }

       //[HttpGet("reservation/delete/{reservationId}")]
        [HttpGet]
        public IActionResult Delete(int reservationId)
        {
            _databaseHandler.DeleteReservation(reservationId);
            return Index();
        }


        [HttpGet]
        public IActionResult DeleteCourse(int coursesid)
        {
            _databaseHandler.DeleteCourse(coursesid);
            return Index();
        }

        [HttpGet]
        public IActionResult Tauschen(int reservationId)
        {
            TauschViewModel t = new TauschViewModel
            {
                Reservation = _databaseHandler.GetReservation(reservationId),
                ToTeacherid = _userManager.GetUserAsync(User).Result.Id, // glaube es muss andersrum mit from und toteacher // jup hab ich mich vertan, is umgedreht
                FromTeacherid = _databaseHandler.GetReservation(reservationId).TeacherId,
                Reservationid = reservationId
            };
            return View(t);
        }
        [HttpPost]
        public IActionResult Tauschen(TauschViewModel t)
        {
            int id = t.Reservationid;
            int id2 = t.OfferReservation;
            string t1 = t.FromTeacherid;
            string t2 = t.ToTeacherid;
           
            _databaseHandler.AddReservationSuggestion(t.FromTeacherid, t.Reservationid, t.ToTeacherid,
                t.OfferReservation, t.message);
            
            return Index();
        }

        [HttpGet]
        public IActionResult Anfragen()
        {
            AnfragenViewModel anfragenViewModel=new AnfragenViewModel();
            anfragenViewModel.IncomingExchangeReservations=_databaseHandler.GetExchangeReservationByTeacherFromId(_userManager.GetUserId(User));
            anfragenViewModel.OutgoingExchangeReservations=_databaseHandler.GetExchangeReservationByTeacherToId(_userManager.GetUserId(User));
            return View(anfragenViewModel);
        }

        //[HttpGet]
        public IActionResult EditOutgoing(int exchangeid)
        {
            ExchangeReservation exchangeReservation=_databaseHandler.GetExchangeReservationById(exchangeid);
            if (_userManager.GetUserAsync(User).Result.Id == exchangeReservation.TeacherTo)
            {
                _databaseHandler.DeleteExchangeReservationById(exchangeid);
            }
            //get by id :  ExchangeReservation exchangeReservation=_databaseHandler.
            // check is logged in user is same as touser
                //delete
            return RedirectToAction("Anfragen");
        }

        //[HttpGet]
        public IActionResult EditIncoming(int exchangeid,bool accept)
        {
            
            ExchangeReservation exchangeReservation=_databaseHandler.GetExchangeReservationById(exchangeid);
            if (_userManager.GetUserAsync(User).Result.Id == exchangeReservation.TeacherFrom)
            {
                exchangeReservation.ExchangeAccepted = accept;
                if (accept)
                {
                    _databaseHandler.ExchangeReservation(exchangeReservation.TeacherFrom,
                        exchangeReservation.ReservationFromId, exchangeReservation.TeacherTo,
                        exchangeReservation.ReservationOfferId);
                }
            }

            exchangeReservation.ExchangeStatus = true;
            _reservationContext.Entry(exchangeReservation).State = EntityState.Modified;
            _reservationContext.SaveChanges();
            return RedirectToAction("Anfragen");
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
                 
            for (int j = 0; j < Data.DayStrings.Length; j++)
            {
                int[] days = {j+1};
                
                for (int i = 0; i < Data.AmountOfBlocks ; i++)
                {
                    CalendarEvent dailyEvent = new CalendarEvent((Data.DayStrings[j] + (i + 1)), Data.BlockStartArray[i], Data.BlockEndArray[i], days, GetColorFromDateAndBlock(start, i));
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
                    ? new RaumbelegungModel(room, true, _databaseHandler.GetTeacher(rr.TeacherId), start, blockId,rr.ReservationId)
                    : new RaumbelegungModel(room, false, null, start, blockId,-1));
            }                     
            return View("block", raumbelegung);
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
            foreach (var startTime in Data.BlockStartArray)
            {
                if (onlyTime.Equals(startTime))
                {
                    return blockId;
                }
                blockId++;
            }
            return 1;
        }

        private string GetColorFromDateAndBlock(DateTime date, int blockNr)
        {
            List<Room> block = _databaseHandler.GetFreeRoomsOnDateAndBlock(date , blockNr);
            if (DateTime.Now >= date && (DateTime.Now >= date || DateTime.Now.Hour >= TimeSpan.Parse(Data.BlockStartArray[blockNr]).Hours))
            {
                return "gray";
            }
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
