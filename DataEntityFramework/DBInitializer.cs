using DataEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntityFramework
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DBContext>
    {
        protected override void Seed(DBContext context)
        {
            var users = new List<User>
            {
                new User{ name="Chrysthoper Contreras", email="chrys_18@hotmail.com", phone="6441185262"},
                new User{ name="Neil Ally", email="neilally@hp.com", phone="1234567890"},
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}