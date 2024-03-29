using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vieon.Controllers.Design_Pattern.Decorator;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class UsersController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            var userDetail = new UserDecorator(db);
            
            if (!userDetail.Details(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_User,SDT,HoTen,Email,MatKhau,RoleUser")] User user)
        {
            if (ModelState.IsValid)
            {
                var userCreate = new UserDecorator(user, db);
                if (userCreate.Create())
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

           

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_User,SDT,HoTen,Email,MatKhau,RoleUser")] User user)
        {
            if (ModelState.IsValid)
            {
                var userEdit = new UserDecorator(user, db);
                if(userEdit.Edit())
                {
                    return RedirectToAction("Index");
                }
             
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            var userDelete = new UserDecorator(db);

            if (!userDelete.Delete(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var userDeleteConfirm = new UserDecorator(db);
                if (userDeleteConfirm.DeleteConfirm(id))
                    return RedirectToAction("Index");
            }
            catch
            {
                return Content("<script>alert('ID không tồn tại')</script>");    
            }
            return RedirectToAction("Index");
        }

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
