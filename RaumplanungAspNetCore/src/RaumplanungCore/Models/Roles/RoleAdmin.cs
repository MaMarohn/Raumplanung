using System;
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
