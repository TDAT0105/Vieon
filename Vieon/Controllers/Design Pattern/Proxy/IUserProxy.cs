using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vieon.Models;

namespace Vieon.Controllers.Design_Pattern.Proxy
{
    public interface IUserProxy
    {
        User GetUserByPhoneNumber(string phoneNumber);
        User GetUserByEmail(string email);
        User GetUserByPhoneAndPassword(string phoneNumber, string password);
        void AddUser(User user);
        void SaveChanges();

    }
}