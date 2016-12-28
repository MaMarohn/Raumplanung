using System.ComponentModel.DataAnnotations;

namespace RaumplanungCore.ViewModels.Reservation
{
    public class TauschViewModel
    {
        [Required]
         public string FromTeacherid { get; set; }
        [Required]
        public string ToTeacherid { get; set; }
        
        public int OfferReservation  { get; set; }
        [Required]
        public Models.Reservation Reservation { get; set; }
        [Required]
        public int Reservationid { get; set; } 
        public string message { get; set; }

    }
}
