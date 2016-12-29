using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaumplanungCore.ViewModels.Kurs
{
    public class KursViewModel
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public List<BlockandDay> Days;
        

    }

    public struct BlockandDay
    {
        private DateTime Day { get; set; }
        private int Block { get; set; }
    }
}
