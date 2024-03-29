using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vieon.Models;

namespace Vieon.Controllers.Design_Pattern.Proxy
{
    public class UserProxy : IUserProxy
    {
        private VieONVipProEntities _dbContext;

        public UserProxy()
        {
            _dbContext = new VieONVipProEntities();
        }

        public User GetUserByPhoneNumber(string phoneNumber)
        {
            return _dbContext.Users.FirstOrDefault(u => u.SDT == phoneNumber);
        }

        public User GetUserByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetUserByPhoneAndPassword(string phoneNumber, string password)
        {
           return _dbContext.Users.FirstOrDefault(k => k.SDT == phoneNumber && k.MatKhau == password);
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        
    }

}