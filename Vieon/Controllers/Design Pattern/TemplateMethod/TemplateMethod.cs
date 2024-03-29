using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers.Design_Pattern
{
    public abstract class TemplateMethod<T> : Controller where T : class
    {
        protected VieONVipProEntities db = new VieONVipProEntities();

        public abstract ActionResult Index();

        public abstract ActionResult Details(int? id);

        public abstract ActionResult Edit(int? id);

        public abstract ActionResult Create();
        public abstract ActionResult Delete(int? id);

        public abstract ActionResult DeleteConfirmed(int id);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        
    }
}