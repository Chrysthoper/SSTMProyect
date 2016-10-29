using DataEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataEntityFramework.Business
{
    public class Tasks
    {
        public static IEnumerable<Task> GetAll(string email)
        {
            using(var context = new DBContext())
            {
                var user = context.Users.FirstOrDefault(x => x.email == email);
                if (user != null)
                    return context.Tasks.Where(x => x.assignedTo == user.id).ToList();
                else
                    return null;
            }
        }
        public static IEnumerable<Task> GetAll(int id)
        {
            using (var context = new DBContext())
            {
                return context.Tasks.Where(x => x.assignedTo == id).ToList();
            }
        }

        public static string Create(Task t)
        {
            using (var context = new DBContext())
            {
                context.Tasks.Add(t);
                context.SaveChanges();
                return "Task Created";
            }
        }
    }
}