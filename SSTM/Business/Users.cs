using DataEntityFramework;
using DataEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSTM.Business
{
    public class Users
    {
        public static IEnumerable<User> GetAll()
        {
            using (var context = new DBContext())
            {
                return context.Users.ToList();
            }
        }
    }
}