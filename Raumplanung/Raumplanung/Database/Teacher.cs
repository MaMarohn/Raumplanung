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
        private string _name;

        public Teacher(String name)
        {
            this._name = name;
        }

        public Teacher(String name, int countAssignedRooms)
        {
            this._name = name;
            this.CountAssignedRooms = countAssignedRooms;
        }

        public int Id { set; get; }
        public String Name
        {
            set { this._name = value; }
            get { return _name; }
        }

        public int CountAssignedRooms { set; get; }
    }
}
