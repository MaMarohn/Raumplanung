using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using RaumplanungCore.ViewModels;
using RaumplanungCore.ViewModels.Kurs;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RaumplanungCore.Controllers
{
    public class KursController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly ReservationContext _reservationContext;
        // GET: /<controller>/
        public KursController(ReservationContext context)
        {
            _reservationContext = context;
            _databaseHandler = new DatabaseHandler(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowRooms(KursViewModel kursViewModel)
        {
            List<List<Room>> freeRoomsPerDay=new List<List<Room>>(); 
            List<Room> Rooms = _databaseHandler.GetAllRooms();
            for (int i = 0; i < kursViewModel.Days.Count; i++)
            {
                
                freeRoomsPerDay.Add(Rooms);//liste mit allen räumen füllen
            }
            DateTime dateStart = kursViewModel.start;
            DateTime dateEnd = kursViewModel.end;
            while (dateStart <= dateEnd)  //für den ganzen zeitraum
            {
                foreach(BlockandDay day in kursViewModel.Days)  //für jede tag/block Komponente
                {
                    DateTime date = new DateTime();
                    if (dateStart.DayOfWeek <= day.Day.DayOfWeek)
                    {
                        date = dateStart.AddDays(day.Day.DayOfWeek - dateStart.DayOfWeek);
                    }
                    else
                    {
                        date = dateStart.AddDays(7-(dateStart.DayOfWeek - day.Day.DayOfWeek));
                    }

                    List<Room> availableRooms = _databaseHandler.GetFreeRoomsOnDateAndBlock(date, day.Block);

                    int i = kursViewModel.Days.BinarySearch(day); //momentaner index
                   
                        List<Room> okayRooms=new List<Room>();
                        foreach (var room in freeRoomsPerDay[i])  // jeder raum von den räumen des tages
                        {
                            if (availableRooms.Contains(room))   //wenn der raum in beiden existiert, wird er behalten
                            {
                                okayRooms.Add(room);
                            }
                        }
                        freeRoomsPerDay[i] = okayRooms;
                    
                }
               
                dateStart = dateStart.AddDays(7);
               
            }

            kursViewModel.Rooms = freeRoomsPerDay;
            

            return View(kursViewModel);
        }





        [HttpGet("kurs/check/{startStop}")]
        public IActionResult Check(string startStop)
        {
            string[] splittedStrings = startStop.Split(';');
            DateTime startDate = DateTime.Parse(splittedStrings[0]);
            DateTime stopDate = DateTime.Parse(splittedStrings[1]);

            if (startDate > stopDate)
            {
                ViewData["DateError"] = "Das Startdatum muss vor dem Enddatum liegen!";
                return View("NewCourse");
            }
            else
            {
                KursViewModel result = new KursViewModel
                {
                    start = startDate,
                    end = stopDate
                };
                return View("CourseDays", result);
            }
        }

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
                int[] days = { j + 1 };

                for (int i = 0; i < Data.AmountOfBlocks; i++)
                {
                    CalendarEvent dailyEvent = new CalendarEvent((Data.DayStrings[j] + (i + 1)), Data.BlockStartArray[i], Data.BlockEndArray[i], days, "blue");
                    eventList.Add(dailyEvent);
                }
                start = start.AddDays(1);
            }
            return eventList;
        }


    }
}
