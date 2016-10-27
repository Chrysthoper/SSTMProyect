using SSTM.Business;
using SSTM.DB;
using SSTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSTM.Controllers
{
    public class TaskController : ApiController
    {
        public IEnumerable<Task> Get()
        {
            return Tasks.GetAll(User.Identity.Name);
        }

        public string Post(Task t)
        {
            try
            {
                var user = Authenticate.Authentication(User.Identity.Name);
                t.assignedBy = user.id;
                t.assignedTo = user.id;
                t.currentProgress = 0;
                t.currentState = (int)TaskState.New;
                t.newTask = true;
                using (var context = new DBContext())
                {
                    context.Tasks.Add(t);
                    context.SaveChanges();
                    return "Task Created";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
