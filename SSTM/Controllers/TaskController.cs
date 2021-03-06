﻿using DataEntityFramework.Business;
using DataEntityFramework;
using DataEntityFramework.Models;
using System;
using System.Collections;
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

        public IEnumerable<Task> Get(int id)
        {
            return Tasks.GetAll(id);
        }

        public string Post(Task t)
        {
            try
            {
                var user = Authenticate.Authentication(User.Identity.Name);
                t.currentProgress = 0;
                t.newTask = true;
                return Tasks.Create(t);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
        [HttpGet]
        [Route("api/taskstates")]
        public Hashtable TaskStates()
        {
            var a = Enum.GetNames(typeof(Task.TaskState));
            Hashtable hashtable = new Hashtable();
            foreach (var i in a)
                hashtable[(int)(Enum.Parse(typeof(Task.TaskState), i))] = i;
            return hashtable;
        }
        
    }
}
