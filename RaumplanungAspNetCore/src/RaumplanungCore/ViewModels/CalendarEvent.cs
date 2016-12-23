using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace RaumplanungCore.ViewModels
{
    [DataContract(Name = "CalendarEvent")]
    public class CalendarEvent
    {

        [DataMember] private string title;
        [DataMember] private string start;
        [DataMember] private string end;
        [DataMember] private int[] dow;
        [DataMember] private string color;

        public CalendarEvent()
        {
            
        }

        public CalendarEvent(string title, string start, string end, int[] dow, string color)
        {
            this.title = title;
            this.start = start;
            this.end = end;
            this.dow = dow;
            this.color = color;
        }





    }
}
