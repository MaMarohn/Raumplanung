using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RaumplanungCore.Models;

namespace RaumplanungCore.ViewModels
{
    public class ReservationAndCourse
    {
        public List<Models.Reservation> Reservations { get; set; }
        public List<Course> Courses { get; set; }

        public ReservationAndCourse(List<Models.Reservation> reservations, List<Course> courses)
        {
            Reservations = reservations;
            Courses = courses;
        }
    }
}
