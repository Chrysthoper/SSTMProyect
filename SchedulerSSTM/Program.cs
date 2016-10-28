using DataEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulerSSTM
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new DBContext())
            {
                context.Users.FirstOrDefault();
            }
            Console.WriteLine("DON'T PRESS ENTER OR ANY KEY");
            Timer timer = new Timer(Scheduler, null, 0, (int)TimeSpan.FromMinutes(1).TotalMilliseconds);
            Console.ReadLine();
        }

        static void Scheduler(Object obj)
        {
            SendEmailNewTasks();

            DateTime time = DateTime.Now;
            if (time.Hour == 8)
            {
                SendEmailReminderDaily();
            }
        }

        public static void SendEmailReminderDaily()
        {
            try
            {
                using (var context = new DBContext())
                {
                    Console.WriteLine("Searching for Tasks on Daily Reminder");
                    var list_users = context.Tasks
                        .Where(x => x.currentState == (int)DataEntityFramework.Models.Task.TaskState.New).ToList()
                        .GroupBy(g => g.assignedTo)
                        .Select(s => new { idUser = s.Key, tasks = s.ToList() }).ToList();
                    Console.WriteLine("Users with Daily Tasks found: " + list_users.Count);
                    foreach (var u in list_users)
                    {
                        var user = context.Users.FirstOrDefault(x => x.id == u.idUser);

                        StringBuilder builder = new StringBuilder();
                        builder.Append("Hello Mrs./Mr. this is just a reminder from SSTM.<br><br>").AppendLine().AppendLine();
                        builder.Append("You have these list of tasks for today::<br><br>").AppendLine().AppendLine();
                        foreach (var t in u.tasks)
                        {
                            builder.Append(t.name).AppendLine("<br>");
                        }

                        builder.AppendLine().AppendLine().Append("<br><br>THANK YOU FOR YOUR TIME");

                        SendEmailValidation(user.email, builder.ToString());
                        Console.WriteLine("Email Send to " + user.email);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR ON SendEmailReminderDaily: " + ex.Message);
            }
        }

        public static void SendEmailNewTasks()
        {
            try
            {
                using (var context = new DBContext())
                {
                    var list_users = context.Tasks
                        .Where(x => x.newTask).ToList()
                        .GroupBy(g => g.assignedTo)
                        .Select(s => new { idUser = s.Key, tasks = s.ToList() }).ToList();
                    Console.WriteLine("Users with New Tasks found: " + list_users.Count);
                    foreach (var u in list_users)
                    {
                        var user = context.Users.FirstOrDefault(x => x.id == u.idUser);

                        StringBuilder builder = new StringBuilder();
                        builder.Append("Hello Mrs./Mr. this is just a reminder from SSTM.<br><br>").AppendLine().AppendLine();
                        builder.Append("You have been assigned to these tasks listed below:<br><br>").AppendLine().AppendLine();
                        foreach (var t in u.tasks)
                        {
                            builder.Append(t.name).AppendLine("<br>");
                        }

                        builder.AppendLine().AppendLine().Append("<br><br>THANK YOU FOR YOUR TIME");

                        SendEmailValidation(user.email, builder.ToString());

                        foreach (var t in u.tasks)
                        {
                            t.newTask = false;
                            context.SaveChanges();
                        }

                        Console.WriteLine("Emails Send");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("ERROR ON SendEmailNewTasks: " + ex.Message);
            }
            
        }

        public static void SendEmailValidation(string email, string body)
        {
            try
            {
                MailMessage mM = new MailMessage();
                mM.From = new MailAddress("iwantthisjobplease@hotmail.com");
                mM.To.Add(email);
                mM.Subject = "New Task Assigned SSTM";
                mM.Body = body;
                mM.IsBodyHtml = true;
                SmtpClient sC = new SmtpClient("smtp.live.com");
                sC.Port = 25;
                //I know this is not a good practise just having some credencials in code
                //but that's why I created an email just for the exercise.
                sC.Credentials = new NetworkCredential("iwantthisjobplease@hotmail.com", "M3x1c02016");
                sC.EnableSsl = true;
                sC.Send(mM);
            }
            catch (System.Exception ex)
            {
                Console.Write(ex.Message);
            }
        }


    }
}
