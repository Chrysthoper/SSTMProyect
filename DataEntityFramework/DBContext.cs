using DataEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DataEntityFramework
{
    public class DBContext : DbContext, IDisposable
    {
        public DBContext() : base("DBContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Token> Tokens { get; set; }

    }
}