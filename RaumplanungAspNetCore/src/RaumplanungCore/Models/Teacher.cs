using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RaumplanungCore.Models
{

/*
 * Asp.net idenity tutorial
 * http://tektutorialshub.com/asp-net-identity-tutorial-basics/
 */
    public class Teacher :IdentityUser
    {
        public ICollection<Reservation> Reservations { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Anrede { get; set; }

        public Teacher()
        {
           if(Reservations == null)
                Reservations = new List<Reservation>();
        }
    }
}
