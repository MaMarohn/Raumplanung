using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RaumplanungCore.Models.Roles
{
    public class RoleAdmin:IdentityRole
    {
        public string Description { set; get; }
    }
}
