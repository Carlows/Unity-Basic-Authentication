using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace BasicAuthentication.Models
{
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public BasicAuthenticationIdentity(string name, string password) : base(name, "Basic")
        {
            Password = password;
        }

        public string Password { get; set; }
    }
}