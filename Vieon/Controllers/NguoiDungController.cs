using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Vieon.Models;

namespace Vieon.Controllers
{
    public class NguoiDungController : Controller
    {
        VieONVipProEntities db = new VieONVipProEntities();
        [HttpGet]
        public ActionResult DangNhap()
        {
            if (Session["TaiKhoan"] == "")
            {
                return View();
            }
            return RedirectToAction("index", "PhimKhachs");
        }
        [HttpPost]
        public ActionResult DangNhap(User us)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(us.SDT))
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                }
                if (string.IsNullOrEmpty(us.MatKhau))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                }

                if (ModelState.IsValid)
                {

                    var khach = db.Users.FirstOrDefault(k => k.SDT == us.SDT && k.MatKhau == us.MatKhau);


                    if (khach != null)
                    {
                        ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                        Session["ID"] = khach.ID_User;
                        Session["TaiKhoan"] = khach;
                        Session["SDT"] = khach.SDT;
                        Session["HoTen"] = khach.HoTen;
                        Session["Email"] = khach.Email;
                        Session["Role"] = khach.RoleUser;
                        if (khach.RoleUser == "Admin")
                        {
                            return RedirectToAction("Index", "Phims");
                        }
                        else
                        {
                            return RedirectToAction("Index", "PhimKhachs");
                        }

                    }
                    else
                    {
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            if (Session["TaiKhoan"] == "")
            {
                return View();
            }
            return RedirectToAction("index", "Phims");
        }
        [HttpPost]
        public ActionResult DangKy(User us)
        {
            if (ModelState.IsValid)
            {
                us.RoleUser = "User";
                var regexName = new Regex(@"^[a-zA-ZÀÁẠÃÄÂÈÉẸÊẼÌÍĨỊÒÓỌÕÔƠÙÚŨƯỶÝỴÂĐ]+$");
                if (!regexName.IsMatch(us.HoTen))
                {
                    ModelState.AddModelError(string.Empty, "Họ tên chỉ được bao gồm chữ cái và dấu");
                }
                if (us.HoTen.Length > 50)
                {
                    ModelState.AddModelError(string.Empty, "Họ tên không được dài quá 50 ký tự");
                }

                if (!string.IsNullOrEmpty(us.SDT))
                {
                    var regex = new Regex(@"^[0-9]+$");
                    if (!regex.IsMatch(us.SDT))
                    {
                        ModelState.AddModelError(string.Empty, "Số điện thoại chỉ được bao gồm số");
                    }
                }
                if (us.SDT.Length != 10 && us.SDT.Length != 11)
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại phải có 10 số");
                }
                if (string.IsNullOrEmpty(us.MatKhau))
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                }
                if (string.IsNullOrEmpty(us.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                }
                var khachhang = db.Users.FirstOrDefault(k => k.SDT == us.SDT);
                if (khachhang != null)
                {
                    ModelState.AddModelError(string.Empty, "Số điện thoại này đã được đăng kí");
                }
                var khachhangEmail = db.Users.FirstOrDefault(k => k.Email == us.Email);
                if (khachhangEmail != null)
                {
                    ModelState.AddModelError(string.Empty, "Email này đã được đăng kí");
                }

                if (ModelState.IsValid)
                {
                    us.RoleUser = "User";
                    us.HoTen.ToUpper();
                    db.Users.Add(us);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("DangNhap");
        }
        public ActionResult DangXuat()
        {
            Session["ID"] = "";
            Session["TaiKhoan"] = "";
            Session["HoTen"] = "";
            Session["SDT"] = "";
            Session["Email"] = "";
            Session["Role"] = "";
            return RedirectToAction("index", "PhimKhachs");
        }
    }
}