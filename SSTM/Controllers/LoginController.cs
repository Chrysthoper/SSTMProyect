using SSTM.Business;
using SSTM.DB;
using SSTM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SSTM.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            using(var context = new DBContext())
            {
                var user = context.Users.FirstOrDefault();
            }
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Authentication(User user)
        {
            var User = Authenticate.Authentication(user.email);
            if(User != null)
            {
                FormsAuthentication.SetAuthCookie(User.email, true);
                Response.StatusCode = (int)HttpStatusCode.OK;
                return Content("Welcome to Super Simple Task Manager", MediaTypeNames.Text.Plain);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content("The user didn't exist", MediaTypeNames.Text.Plain);    
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            var User = Authenticate.Authentication(user.email);
            if (User != null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content("This email already exist", MediaTypeNames.Text.Plain);
            }

            if(Authenticate.TokenExist(user.email))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Content("Validate this email with the token we already send you", MediaTypeNames.Text.Plain);
            }

            var token = Authenticate.CreateToken(user);

            General.SendEmailValidation(token.token, user.email);

            Response.StatusCode = (int)HttpStatusCode.OK;
            return Content("A token has been send to you email. Please click in the url to confirm the token.", MediaTypeNames.Text.Plain);
        }
        
        [HttpGet]
        public ActionResult ConfirmToken(Guid token)
        {
            if (Authenticate.TokenExist(token))
            {
                var user = Authenticate.CreateUser(token);
                FormsAuthentication.SetAuthCookie(user.email, true);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");
        }
    }
}
