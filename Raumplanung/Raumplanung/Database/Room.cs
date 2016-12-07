using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raumplanung
{
    class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int _id;
        private String _name;

        public Room(String name)
        {
            this._name = name;
        }

        public Room()
        {
        }

        public int Id { set; get; }

        public String Name
        {
            set { this._name = value; }
            get { return _name; }
        }

    }
}
