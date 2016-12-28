using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaumplanungCore.Models
{
    public class CourseExceptions
    {
        [Key]
        public int CourseExceptionsId { set; get; }
        public DateTime? Date { set; get; }
        public int Block { set; get; }
        public int CourseId { set; get; }
    }
}
