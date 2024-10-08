﻿using BENETCore_072025.DataAccess.DO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.DBContext
{
    public class DBHotelContext : DbContext
    {
        public DBHotelContext(DbContextOptions options) : base(options)
        {
        }
        //entities
        public DbSet<Rooms> rooms { get; set; }
        public DbSet<RoomTypes> roomTypes { get; set; }
        public DbSet<Accounts> accounts { get; set; }
        public DbSet<Function> function { get; set; }
        public DbSet<UserPermission> userPermission { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
