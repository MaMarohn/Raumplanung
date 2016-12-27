using System.Collections.Generic;

namespace RaumplanungCore.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; }

        public ICollection<Reservation> Reservations { set; get; }

        public Room()
        {
            if (Reservations == null)
                Reservations = new List<Reservation>();
        }
    }
}
