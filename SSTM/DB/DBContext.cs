using SSTM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SSTM.DB
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DBContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Token> Tokens { get; set; }

    }
}