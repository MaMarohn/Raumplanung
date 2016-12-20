using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { set; get; }

        public Teacher()
        {
            if(Reservations == null)
                Reservations = new List<Reservation>();
        }
    }
}
