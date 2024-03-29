using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Vieon.Controllers.Design_Pattern.Decorator
{
    public interface IUser
    {     
        bool Edit();
        bool Create();
        bool Details(int? id);

        bool Delete(int? id);
        bool DeleteConfirm(int id);
        
    }
}