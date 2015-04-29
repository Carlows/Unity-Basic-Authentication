using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthentication.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }

        public string ProfileImgPath { get; set; }
    }
}