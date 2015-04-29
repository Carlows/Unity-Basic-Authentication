using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicAuthentication.Infrastructure
{
    public interface IUserManager
    {
        bool AuthenticateCredentials(string username, string password);
        User FindUser(string username);
        void UploadProfileImage(string username, HttpPostedFileBase file, string serverPath);
        string GetProfileImage(string username);
    }
}
