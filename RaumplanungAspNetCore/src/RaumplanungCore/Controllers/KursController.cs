using System;
using System.Collections.Generic;
using System.Globalization;
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
            kursViewModel.Roomlist = new List<DayAndRooms>();
           
            List<Room> allRooms = _databaseHandler.GetAllRooms();
                      
            foreach (var day in kursViewModel.Days)
            {
                DateTime dayformatted = DateTime.ParseExact(day, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                DayAndRooms dayAndRooms=new DayAndRooms()
                {
                    Date = dayformatted,
                    Rooms = allRooms,
                    block = Array.FindIndex(Data.BlockStartArray,s=>s.Equals(dayformatted.Hour+":"+dayformatted.Minute))+1
                };
                kursViewModel.Roomlist.Add(dayAndRooms);
            }
            
            DateTime dateStart = kursViewModel.start;
            DateTime dateEnd = kursViewModel.end;
            while (dateStart <= dateEnd)  //für den ganzen zeitraum
            {
                for(var x=0; x<kursViewModel.Roomlist.Count;x++)  //für jede tag/block Komponente
                {
                    DayAndRooms day = kursViewModel.Roomlist[x];
                    DateTime date = new DateTime();
                    if (dateStart.DayOfWeek <= day.Date.DayOfWeek)
                    {
                        if (!(dateStart.AddDays(day.Date.DayOfWeek - dateStart.DayOfWeek) > dateEnd))
                        {
                            date = dateStart.AddDays(day.Date.DayOfWeek - dateStart.DayOfWeek);
                        }
                        
                    }
                    else
                    {
                        if (!(dateStart.AddDays(7 + (dateStart.DayOfWeek - day.Date.DayOfWeek))>dateEnd))
                        {
                            date = dateStart.AddDays(7 + (dateStart.DayOfWeek - day.Date.DayOfWeek));
                        }
                       
                    }
                    List<Room> resultrooms=new List<Room>();
                    List<Room> availableRooms = _databaseHandler.GetFreeRoomsOnDateAndBlock(date, day.block);
                    resultrooms = availableRooms.Intersect(day.Rooms).ToList();

                    kursViewModel.Roomlist[x].Rooms.Clear();
                    kursViewModel.Roomlist[x].Rooms.AddRange(resultrooms);


                }
               
                dateStart = dateStart.AddDays(7);
               
            }
           return View(kursViewModel);
        }

        [HttpPost]
        public IActionResult SubmitCourse(KursViewModel kursViewModel,List<string> rooms)
        {
            List<Room> Rooms = _databaseHandler.GetAllRooms();

            for (int x = 0; x < rooms.Count; x++)
            {
                var roomlistobject = kursViewModel.Roomlist[x];
                roomlistobject.ChosenRoom=Rooms.Find(r => r.Name.Equals(rooms[x]));
                kursViewModel.Roomlist[x] = roomlistobject;
            }
            DateTime d = new DateTime();
          
            //kurs erstellen
            



            return RedirectToAction("Reservation","Index");
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
