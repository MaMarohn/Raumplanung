using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public class ExchangeReservation
    {
        [Key]
        public int EchangeReservationId { set; get; }
        public string TeacherFrom { set; get; }
        public string TeacherTo { set; get; }
        public Reservation ReservationFrom { set; get; }
        public Reservation ReservationTo { set; get; }
        public bool? ExchangeStatus { set; get; }
        public bool? ExchangeAccepted { set; get; }
        public string Message { set; get; }
        

    }
}
