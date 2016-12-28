using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public class Course
    {
        public int CourseId { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public string Name { set; get; }
        public int Block { set; get; }
        public string TeacherId { set; get; }
        public int RoomId { set; get; }
        public ICollection<CourseExceptions> CourseExceptions { get; set; }

    }
}
