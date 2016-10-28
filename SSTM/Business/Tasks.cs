using DataEntityFramework;
using DataEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSTM.Business
{
    public class Tasks
    {
        public static IEnumerable<Task> GetAll(string email)
        {
            using(var context = new DBContext())
            {
                var user = context.Users.FirstOrDefault(x => x.email == email);
                return context.Tasks.Where(x => x.assignedTo == user.id).ToList();
            }
        }
        public static IEnumerable<Task> GetAll(int id)
        {
            using (var context = new DBContext())
            {
                return context.Tasks.Where(x => x.assignedTo == id).ToList();
            }
        }
    }
}