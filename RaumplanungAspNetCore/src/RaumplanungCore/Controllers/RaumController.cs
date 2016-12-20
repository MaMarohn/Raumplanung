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
    public class RaumController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;

        public RaumController(ReservationContext context)
        {
            _databaseHandler = new DatabaseHandler(context);
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Room> rooms = _databaseHandler.GetAllRooms();
            return View("Index", rooms);
        }

        // GET: /<controller>/Detail/reservationId
        [HttpGet("Detail/{reservationId}")]
        public IActionResult Detail(int reservationId)
        {
            ViewData["ReservationId"] = reservationId;
            return View();
        }
    }
}
