using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSTM.Models
{
    public class Task
    {
        public enum TaskState
        {
            New,
            InProgress,
            Waiting,
            StandBy,
            Done
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Boolean newTask { get; set; } //Default 1
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; } //Optional
        public DateTime completeDate { get; set; }
        public int currentState { get; set; }
        public decimal currentProgress { get; set; } //Default 0
        public int assignedBy { get; set; }
        public User AssignedByUser { get; set; }
        public int assignedTo { get; set; }
        public User AssignedToUser { get; set; }
        public string comments { get; set; } //Optional
    }
}