using System;
using Microsoft.AspNetCore.Mvc;
using Raumplanung.Database;
using RaumplanungCore.Database;

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
            return _databaseHandler.GetAllRooms()[0].Name;
        }

        public IActionResult Index()
        {
            return View("~/Views/Test/HomeView.cshtml");
        }
    }
}
