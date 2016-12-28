using System;

namespace RaumplanungCore.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int Block { get; set; }
        public DateTime? Date { get; set; }

        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int CourseId { get; set; }

    }
}
