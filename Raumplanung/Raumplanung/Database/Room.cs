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
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { set; get; }
        public String Name { set; get; }
        public Boolean? Free { set; get; }

        public Room(String name)
        {
            this.Free = true;
            this.Name = name;
        }

        public Room()
        {
            this.Free = true;
        }

    }
}
