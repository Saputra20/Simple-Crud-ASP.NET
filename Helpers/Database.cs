using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Simple_Api.Models.Domain;

namespace Simple_Api.Helpers
{
    public class Database : DbContext
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {}

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}