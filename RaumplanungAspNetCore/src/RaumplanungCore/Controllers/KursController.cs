using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Raumplanung.Database;
using RaumplanungCore.Database;
using RaumplanungCore.Models;
using RaumplanungCore.Services;
using RaumplanungCore.ViewModels;
using RaumplanungCore.ViewModels.Kurs;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RaumplanungCore.Controllers
{
    public class KursController : Controller
    {
        private readonly DatabaseHandler _databaseHandler;
        private readonly ReservationContext _reservationContext;
        private readonly UserManager<Teacher> _userManager;
        // GET: /<controller>/
        public KursController(ReservationContext context,UserManager<Teacher> UserManager )
        {
            _reservationContext = context;
            _databaseHandler = new DatabaseHandler(context);
            _userManager = UserManager;
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
            if (kursViewModel.Days!=null)
            {
                kursViewModel.Roomlist = new List<DayAndRooms>();
                List<DateTime> datelist = new List<DateTime>();
                List<Room> allRooms = _databaseHandler.GetAllRooms();

                foreach (var day in kursViewModel.Days)
                {
                    DateTime dayformatted = DateTime.ParseExact(day, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    DayAndRooms dayAndRooms = new DayAndRooms()
                    {
                        Date = dayformatted,
                        Rooms = allRooms,
                        block =
                            Array.FindIndex(Data.BlockStartArray,
                                s => s.Equals(dayformatted.Hour + ":" + dayformatted.Minute)) + 1
                    };
                    kursViewModel.Roomlist.Add(dayAndRooms);

                }

                DateTime dateStart = kursViewModel.start;
                DateTime dateEnd = kursViewModel.end;
                while (dateStart <= dateEnd) //für den ganzen zeitraum
                {
                    for (var x = 0; x < kursViewModel.Roomlist.Count; x++) //für jede tag/block Komponente
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
                            if (!(dateStart.AddDays(7 + (dateStart.DayOfWeek - day.Date.DayOfWeek)) > dateEnd))
                            {
                                date = dateStart.AddDays(7 + (dateStart.DayOfWeek - day.Date.DayOfWeek));
                            }

                        }
                        List<Room> resultrooms = new List<Room>();
                        List<Room> availableRooms = _databaseHandler.GetFreeRoomsOnDateAndBlock(date, day.block);
                        resultrooms = availableRooms.Intersect(day.Rooms).ToList();

                        var roomlistobject = kursViewModel.Roomlist[x];
                        roomlistobject.Rooms = resultrooms;
                        kursViewModel.Roomlist[x] = roomlistobject;





                        //kursViewModel.Roomlist[x].Rooms.Clear();
                        //kursViewModel.Roomlist[x].Rooms.AddRange(resultrooms);
                        datelist.Add(kursViewModel.Roomlist[x].Date);

                    }

                    dateStart = dateStart.AddDays(7);

                }


                HttpContext.Session.SetObjectAsJson("datelist", datelist);

                return View(kursViewModel);
            }
            else
            {
                ViewData["ErrorTagauswahl"] = "Es wurden keine Tage ausgewählt!";
                return View("CourseDays",kursViewModel);
            }
        }

        [HttpPost]
        public IActionResult SubmitCourse(KursViewModel kursViewModel)
        {
            List<Room> Rooms = _databaseHandler.GetAllRooms();
            List<DateTime> datelist = HttpContext.Session.GetObjectFromJson<List<DateTime>>("datelist");
            for (int x = 0; x < kursViewModel.rooms.Count; x++)
            {
                /*var roomlistobject = kursViewModel.Roomlist[x];
                roomlistobject.ChosenRoom=Rooms.Find(r => r.Name.Equals(kursViewModel.rooms[x]));
                kursViewModel.Roomlist[x] = roomlistobject;*/
            }
            List<DateandRoom> datenandRooms=new List<DateandRoom>();
            for (int x = 0; x < kursViewModel.rooms.Count; x++)
            {
                datenandRooms.Add(new DateandRoom
                {
                    block=Array.IndexOf(Data.BlockStartArray,datelist[x].ToString("HH:mm")),
                    room = Rooms.Find(r => r.Name.Equals(kursViewModel.rooms[x])),
                    weekday =(int) datelist[x].DayOfWeek
                   
                });
            }


            _databaseHandler.AddCourse(datenandRooms, kursViewModel.start, kursViewModel.end, kursViewModel.kursname,
                _userManager.GetUserId(User));





            return RedirectToAction("Index", "Reservation");
        }



        [HttpGet("kurs/check/{startStop}")]
        public IActionResult Check(string startStop)
        {
            ViewData["ErrorTagauswahl"] = "";
            string[] splittedStrings = startStop.Split(';');
            string name = splittedStrings[0];
            DateTime startDate = DateTime.Parse(splittedStrings[1]);
            DateTime stopDate = DateTime.Parse(splittedStrings[2]);
            ViewData["DateError"] = "";

            if (startDate > stopDate)
            {
                ViewData["DateError"] = "Das Startdatum muss vor dem Enddatum liegen!";
                return View("NewCourse");
            }
            else
            {
                KursViewModel result = new KursViewModel
                {
                    kursname = name,
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

        public IActionResult FreeDates(int id)
        {
            List<Reservation> reservations = _databaseHandler.GetAllReservationsFromCourse(id); // TODO: später: getReservationsFromCourse()
            return View(reservations);
        }

        public IActionResult Details(int id)
        {
            return View();
        }

    }
}
