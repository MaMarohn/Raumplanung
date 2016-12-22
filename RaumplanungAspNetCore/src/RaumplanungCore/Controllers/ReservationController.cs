using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;

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
            List<Reservation> reservations = _databaseHandler.GetAllReservations();
            int count = reservations.Count;            
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
            //_databasHandler.deleteReservation(reservationId);
            List<Room> rooms = _databaseHandler.GetAllRooms();
            return View("Index", rooms);
        }

        [HttpGet("reservation/New")]
        public IActionResult New()
        {
            return View();
        }
    }
}
