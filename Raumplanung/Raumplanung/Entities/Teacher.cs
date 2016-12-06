using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raumplanung
{
    class Teacher
    {
        [Key]
        private int _id;
        private String _name;

        public int Id { set; get; }
        public String Name { set; get; }
    }
}
