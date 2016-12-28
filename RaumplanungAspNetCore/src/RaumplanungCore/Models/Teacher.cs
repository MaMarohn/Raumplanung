using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RaumplanungCore.Models
{

/*
 * Asp.net idenity tutorial
 * http://tektutorialshub.com/asp-net-identity-tutorial-basics/
 */
    public class Teacher :IdentityUser
    {
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<ExchangeReservation> ExchangeReservations { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Anrede { get; set; }

        public Teacher()
        {
           if(Reservations == null)
                Reservations = new List<Reservation>();
            if (ExchangeReservations == null)
                ExchangeReservations = new List<ExchangeReservation>();
        }
    }
}
