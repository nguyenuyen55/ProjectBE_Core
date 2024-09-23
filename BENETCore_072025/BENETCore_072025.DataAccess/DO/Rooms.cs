﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.DO
{
    public class Rooms
    {
        [Key]
       public int Id { get; set; } 
       public int idRoomType { get; set; }
        public RoomTypes? roomType { get; set; }
        public int NumberRoom { get; set; }
        public bool IsAvailable { get; set; }

    }
}