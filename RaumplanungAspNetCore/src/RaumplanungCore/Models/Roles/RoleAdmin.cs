using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RaumplanungCore.Models.Roles
{
    public class RoleAdmin:IdentityRole
    {

        public RoleAdmin(String name, String description)
        {
            Description = description;
            Name = name;
        }

        public RoleAdmin()
        {
            
        }
        public string Description { set; get; }
    }
}
