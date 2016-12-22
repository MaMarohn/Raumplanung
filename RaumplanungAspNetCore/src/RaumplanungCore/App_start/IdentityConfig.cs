using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RaumplanungCore.Database;
using RaumplanungCore.Models;

namespace RaumplanungCore.App_start
{
    public class ReservationUserStore : UserStore<Teacher>
    {

        public ReservationUserStore(ReservationContext context) : base(context)
        {

        }

    }
}




