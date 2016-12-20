using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Reservation> Reservations { set; get; }

        public Room()
        {
            if (Reservations == null)
                Reservations = new List<Reservation>();
        }
    }
}
