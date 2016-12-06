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
        private int _id;
        private Room _room;
        private Teacher _teacher;

        public int Id { set; get; }
        public Room Room { set; get; }
        public Teacher Teacher { set; get; }
    }
}
