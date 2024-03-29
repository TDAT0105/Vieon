using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class UserKhachsController : Controller
    {
        private VieONVipProEntities db = new VieONVipProEntities();

        // GET: UserKhachs
        public ActionResult Index()
        {
            try
            {
                var userID = (int)Session["ID"];
                var user = db.Users.Include(p => p.ID_User == userID);             
                return View(user);
            }
            catch
            {
                return Content("<script>alert('Lỗi.'); window.location.href='/PhimKhachs/Index';</script>");
            }
        }

        // GET: UserKhachs/Details/5
        public ActionResult Details(int? id)
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
