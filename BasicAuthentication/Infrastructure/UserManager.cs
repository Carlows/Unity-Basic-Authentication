using BasicAuthentication.Helpers;
using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BasicAuthentication.Infrastructure
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _repository;

        public UserManager(IUserRepository repo)
        {
            _repository = repo;
        }

        public bool AuthenticateCredentials(string username, string password)
        {
            var user = _repository.FindUser(username);

            if (user == null)
                return false;

            return PasswordHash.ValidatePassword(password, user.PasswordHash);
        }
        
        public User FindUser(string username)
        {
            return _repository.FindUser(username);
        }

        public void UploadProfileImage(string username, HttpPostedFileBase file, string serverPath)
        {
            var user = _repository.FindUser(username);

            string dirPath = Path.Combine(serverPath, user.Name);

            if(!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            string extension = Path.GetExtension(file.FileName);
            string fileName = string.Format("{0:dd-MM-yyyy-HH-mm-ss}{1}", DateTime.Now, extension);
            string userFolder = Path.Combine(user.Name, fileName);
            string path = Path.Combine(serverPath, userFolder);
            
            file.SaveAs(path);

            user.ProfileImgPath = userFolder;
            _repository.UpdateUser(user);
        }

        public string GetProfileImage(string username)
        {
            var user = _repository.FindUser(username);

            string path = String.IsNullOrEmpty(user.ProfileImgPath) ? "notfound.jpg" : user.ProfileImgPath;

            return path;
        }
    }
}