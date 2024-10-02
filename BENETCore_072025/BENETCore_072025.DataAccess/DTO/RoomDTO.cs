using BENETCore_072025.DataAccess.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.DTO
{
    public class RoomDTO
    {
        public int idroomType { get; set; }
        public int numberRoom { get; set; }
        public bool isAvailable { get; set; }
    }
}
