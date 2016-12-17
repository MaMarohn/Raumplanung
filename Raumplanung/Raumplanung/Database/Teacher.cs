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
        public int Id { set; get; }
        public string Name { set; get; }
        public int CountAssignedRooms { set; get; }

        public Teacher()
        {
            
        }

        public Teacher(String name)
        {
            this.Name = name;
        }

        public Teacher(String name, int countAssignedRooms)
        {
            this.Name = name;
            this.CountAssignedRooms = countAssignedRooms;
        }
    }
}
