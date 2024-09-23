using BENETCore_072025.DataAccess.DO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.DBContext
{
    public class DBContext : DbContext
    {

        //entities
        public DbSet<Rooms> rooms { get; set; }
        public DbSet<RoomTypes> roomTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
