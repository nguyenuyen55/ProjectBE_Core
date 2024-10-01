using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.DO
{
    public class RoomTypes
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }

        public Rooms Rooms { get; set; }
    }
}
