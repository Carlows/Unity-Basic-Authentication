using BasicAuthentication.Helpers;
using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasicAuthentication.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User FindUser(string username)
        {
 	        return _context.users
                   .Where(u => u.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase))
                   .SingleOrDefault();
        }

        public void CreateUser(string username, string password)
        {
 	        var newUser = new User()
            {
                Name = username,
                PasswordHash = PasswordHash.HashPassword(password)
            };

            _context.users.Add(newUser);
            _context.SaveChanges();
        }


        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}