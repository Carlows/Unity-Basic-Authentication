using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthentication.Models
{
    public class UserViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public bool remember { get; set; }
    }
}