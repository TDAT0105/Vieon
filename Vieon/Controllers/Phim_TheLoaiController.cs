using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vieon.Controllers.Design_Pattern;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class Phim_TheLoaiController :  TemplateMethod<Phim_TheLoai>
    {
        public override ActionResult Index()
        {
            var phim_TheLoai = db.Phim_TheLoai.Include(p => p.Phim).Include(p => p.TheLoai);
            return View(phim_TheLoai.ToList());
        }

        public override ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            if (phim_TheLoai == null)
            {
                return HttpNotFound();
            }
            return View(phim_TheLoai);
        }

        public override ActionResult Create()
        {
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim");
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create([Bind(Include = "ID_Phim_TheLoai,ID_Phim,ID_TheLoai")] Phim_TheLoai phim_TheLoai)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem phim đã có thể loại đó chưa
                if (!PhimTheLoaiExists(phim_TheLoai.ID_Phim, phim_TheLoai.ID_TheLoai))
                {
                    db.Phim_TheLoai.Add(phim_TheLoai);
                    db.SaveChanges();
                    return RedirectToAction("Edit", "Phims", new { id = phim_TheLoai.ID_Phim });
                }
                else
                {
                    ModelState.AddModelError("", "Phim đã có thể loại này.");
                }
            }

            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_TheLoai.ID_Phim);
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai", phim_TheLoai.ID_TheLoai);
            return View(phim_TheLoai);
        }

        private bool PhimTheLoaiExists(int? phimId, int? theLoaiId)
        {
            return db.Phim_TheLoai.Any(ptl => ptl.ID_Phim == phimId && ptl.ID_TheLoai == theLoaiId);
        }

        public override ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            if (phim_TheLoai == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_TheLoai.ID_Phim);
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai", phim_TheLoai.ID_TheLoai);
            return View(phim_TheLoai);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit([Bind(Include = "ID_Phim_TheLoai,ID_Phim,ID_TheLoai")] Phim_TheLoai phim_TheLoai)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem phim đã có thể loại đó chưa
                if (!PhimTheLoaiExists(phim_TheLoai.ID_Phim, phim_TheLoai.ID_TheLoai))
                {
                    db.Entry(phim_TheLoai).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Edit", "Phims", new { id = phim_TheLoai.ID_Phim });
                }
                else
                {
                    ModelState.AddModelError("", "Phim đã có thể loại này.");
                }
            }
            ViewBag.ID_Phim = new SelectList(db.Phims, "ID_Phim", "TenPhim", phim_TheLoai.ID_Phim);
            ViewBag.ID_TheLoai = new SelectList(db.TheLoais, "ID_TheLoai", "TenTheLoai", phim_TheLoai.ID_TheLoai);
            return View(phim_TheLoai);
        }

        public override ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            if (phim_TheLoai == null)
            {
                return HttpNotFound();
            }
            return View(phim_TheLoai);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public override ActionResult DeleteConfirmed(int id)
        {
            Phim_TheLoai phim_TheLoai = db.Phim_TheLoai.Find(id);
            int? idPhim = phim_TheLoai.ID_Phim;
            db.Phim_TheLoai.Remove(phim_TheLoai);
            db.SaveChanges();
            return RedirectToAction("Edit", "Phims", new { id = idPhim });
        }
    }
}
