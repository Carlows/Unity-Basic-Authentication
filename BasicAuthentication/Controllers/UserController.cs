using BasicAuthentication.Filters;
using BasicAuthentication.Infrastructure;
using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicAuthentication.Controllers
{
    [BasicAuthFilter]
    public class UserController : ApiController
    {
        private readonly IUserManager _manager;

        public UserController(IUserManager mgr)
        {
            _manager = mgr;
        }

        [Route("api/user/profile")]
        [HttpGet]
        public UserProfileViewModel Profile()
        {
            var user = _manager.FindUser(User.Identity.Name);

            var model = new UserProfileViewModel
            {
                Name = user.Name,
                ProfilePicPath = Path.Combine("Content", user.ProfileImgPath)
            };

            return model;
        }

        [Route("api/user/validate")]
        [HttpGet]
        public IHttpActionResult Validate()
        {
            return Ok();
        }
    }
}
