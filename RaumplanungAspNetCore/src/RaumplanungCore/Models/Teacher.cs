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
        //public override TKey Id { get; set; }

        //public int TeacherId { get; set; }
        //public string Name { get; set; }

        //public override string Id { get; set; }

        //public override ICollection<IdentityUserClaim<string>> Claims { get; }
        //public override ICollection<IdentityUserLogin<string>> Logins { get; }
        //public override ICollection<IdentityUserRole<string>> Roles { get; }

        //public override string UserName { get; set; }
        public ICollection<Reservation> Reservations { get; set; }



        

        public Teacher()
        {
           if(Reservations == null)
                Reservations = new List<Reservation>();
        }
    }
}
