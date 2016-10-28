using DataEntityFramework.Business;
using DataEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSTM.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public IEnumerable<User> Get()
        {
            return Users.GetAll();
        }
    }
}
