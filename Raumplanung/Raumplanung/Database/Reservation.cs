using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raumplanung.Entities
{
    class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        public Room Room { set; get; }
        public Teacher Teacher { set; get; }
        public DateTime Date { set; get; }

        public Reservation()
        {
        }

        public Reservation(Room r, Teacher t, DateTime date)
        {
            Room = r;
            Teacher = t;
            Date = date;
        }
    }
}
