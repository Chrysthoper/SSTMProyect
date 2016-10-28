using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DataEntityFramework.Business
{
    public class General
    {
        public static void SendEmailValidation(Guid token, string email)
        {
            try
            {
                MailMessage mM = new MailMessage();
                mM.From = new MailAddress("iwantthisjobplease@hotmail.com");
                mM.To.Add(email);
                mM.Subject = "Token validation for SSTM";
                mM.Body = "Click in the URL:" + "http://localhost:47155/Login/ConfirmToken?token=" + token;
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