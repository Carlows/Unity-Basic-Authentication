using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicAuthentication.Infrastructure
{
    public interface IUserRepository
    {
        User FindUser(string username);
        void CreateUser(string username, string password);
        void UpdateUser(User user);
    }
}
