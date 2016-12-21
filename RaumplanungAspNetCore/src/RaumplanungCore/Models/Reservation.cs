using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int Block { get; set; }
        public DateTime? Date { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
