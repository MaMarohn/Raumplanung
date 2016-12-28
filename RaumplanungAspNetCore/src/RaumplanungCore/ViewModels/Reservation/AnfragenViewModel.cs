using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RaumplanungCore.Models;

namespace RaumplanungCore.ViewModels.Reservation
{
    public class AnfragenViewModel
    {

        public List<ExchangeReservation> OutgoingExchangeReservations { get; set; }
        public List<ExchangeReservation> IncomingExchangeReservations { get; set; }

    }
}
