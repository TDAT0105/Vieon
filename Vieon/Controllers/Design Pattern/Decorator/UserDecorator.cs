using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers.Design_Pattern.Decorator
{
    public class UserDecorator : IUser
    {
        private User _user;
        private VieONVipProEntities _db;

        public UserDecorator(User user, VieONVipProEntities db)
        {
            _user = user;
            _db = db;
        }

        public UserDecorator(VieONVipProEntities db)
        {          
            _db = db;
        }

        public bool Edit()
        {
            ThanhToan thanhToan = _db.ThanhToans.FirstOrDefault(t => t.ID_User == _user.ID_User);

            try
            {
                if (_user.RoleUser != "Admin")
                {
                    if (thanhToan == null)
                    {
                        _user.RoleUser = "User";
                    }
                    else
                    {
                        _user.RoleUser = "Vip";
                    }
                }

                _db.Entry(_user).State = EntityState.Modified;
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Create()
        {
            try
            {
                _db.Users.Add(_user);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Details(int? id)
        {
           if(id == null)
            {
                return false;
            }
            
            return true;
        }

        public bool Delete(int? id)
        {
            if (id == null)
            {
                return false;
            }

            return true;
        }

        public bool DeleteConfirm(int id)
        {
            try
            {
                User user = _db.Users.Find(id);
                _db.Users.Remove(user);
                _db.SaveChanges();
                return true;
            }
            catch 
            {
                return false; 
            }

        }
    }
}
