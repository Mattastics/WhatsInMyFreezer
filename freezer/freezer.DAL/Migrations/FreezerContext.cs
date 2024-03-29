﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace freezer.DAL.Migrations
{
    public class FreezerContext : DbContext
    {
        public string DbPath { get; }

        public DbSet<FoodItem> FoodItem { get; set; }
        public FreezerContext(DbContextOptions<FreezerContext> options): base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "freezer.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(connectionString: $"Data Source={DbPath}");
    }
}
