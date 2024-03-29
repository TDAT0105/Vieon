using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using Vieon.Models;

namespace Vieon.Controllers.Design_Pattern.Decorator
{
    public class DaoDienDecorator : IUser
    {
        private DaoDien _daodien;
        private VieONVipProEntities _db;
        string Name;
        public DaoDienDecorator(DaoDien daoDien, VieONVipProEntities db)
        {
            _daodien = daoDien;
            _db = db;
        }

        public DaoDienDecorator(VieONVipProEntities db)
        {
            _db = db;
        }

        public DaoDienDecorator(VieONVipProEntities db, string name, DaoDien daoDien)
        {
            Name = name;
            _db = db;
            _daodien = daoDien;
        }

        public bool Edit()
        {
            DaoDien daoDien = _db.DaoDiens.FirstOrDefault(x=>x.ID_DaoDien == _daodien.ID_DaoDien);
            try
            {
                daoDien.TenDaoDien = Name;
                _db.Entry(daoDien).State = EntityState.Modified;
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
                _db.DaoDiens.Add(_daodien);
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
            if (id == null)
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
                DaoDien daodien = _db.DaoDiens.Find(id);
                _db.DaoDiens.Remove(daodien);
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