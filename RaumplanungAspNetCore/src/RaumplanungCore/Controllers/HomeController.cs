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
    public class HomeController : Controller
    {

        private readonly DatabaseHandler _databaseHandler;

        public HomeController(ReservationContext context)
        {
            _databaseHandler = new DatabaseHandler(context);
        }


        public String Index2()
        {

            List<Reservation> r = _databaseHandler.GetReservationsWithDate(new DateTime(2016, 12, 20));
            Console.WriteLine(r.Count);

            return _databaseHandler.GetAllRooms()[0].Name;
        }

        public IActionResult Index()
        {
            return View("~/Views/Test/HomeView.cshtml");
        }
    }
}
