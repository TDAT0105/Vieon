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
            List<string> errorMessages = new List<string>();

            try
            {
                if (ModelState.IsValid)
                {
                    us.RoleUser = "User";

                    // Kiểm tra Họ tên
                    if (string.IsNullOrWhiteSpace(us.HoTen))
                    {
                        errorMessages.Add("Tên không hợp lệ");
                    }
                    var regexName = new Regex(@"^[a-zA-ZÀÁẠÃÄÂÈÉẸÊẼÌÍĨỊÒÓỌÕÔƠÙÚŨƯỶÝỴÂĐ\s]+$");
                    if (!regexName.IsMatch(us.HoTen))
                    {
                        errorMessages.Add("Họ tên chỉ được bao gồm chữ cái và dấu");
                    }
                    else if (us.HoTen.Length > 50)
                    {
                        errorMessages.Add("Họ tên không được dài quá 50 ký tự");
                    }
                    else if (us.HoTen.Length < 6)
                    {
                        errorMessages.Add("Họ tên không được ngắn hơn 6 ký tự");
                    }
                    // Kiểm tra Số điện thoại
                    if (string.IsNullOrWhiteSpace(us.SDT))
                    {
                        errorMessages.Add("Số điện thoại không được để trống");
                    }

                    else
                    {
                        var regex = new Regex(@"^[0-9]+$");
                        if (!regex.IsMatch(us.SDT))
                        {
                            errorMessages.Add("Số điện thoại chỉ được bao gồm số");
                        }

                        if (us.SDT.Length != 10 && us.SDT.Length != 11)
                        {
                            errorMessages.Add("Số điện thoại phải có 10 hoặc 11 số");
                        }
                    }

                    // Kiểm tra Mật khẩu
                    if (string.IsNullOrWhiteSpace(us.MatKhau))
                    {
                        errorMessages.Add("Mật khẩu không được để trống");
                    }

                    else if (us.MatKhau.Length < 6)
                    {
                        errorMessages.Add("Mật khẩu phải có ít nhất 6 kí tự");
                    }

                    // Kiểm tra Email
                    if (string.IsNullOrWhiteSpace(us.Email))
                    {
                        errorMessages.Add("Email không được để trống");
                    }

                    else
                    {
                        var regexEmail = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                        if (!regexEmail.IsMatch(us.Email))
                        {
                            errorMessages.Add("Email không hợp lệ");
                        }
                    }

                    // Kiểm tra trùng lặp số điện thoại và email
                    var khachhang = db.Users.FirstOrDefault(k => k.SDT == us.SDT);
                    if (khachhang != null)
                    {
                        errorMessages.Add("Số điện thoại này đã được đăng kí");
                    }
                    var khachhangEmail = db.Users.FirstOrDefault(k => k.Email == us.Email);
                    if (khachhangEmail != null)
                    {
                        errorMessages.Add("Email này đã được đăng kí");
                    }

                    // Nếu không có lỗi, thêm người dùng vào cơ sở dữ liệu
                    if (errorMessages.Count == 0)
                    {
                        us.HoTen = us.HoTen.ToUpper();
                        db.Users.Add(us);
                        db.SaveChanges();

                        ViewBag.SuccessMessage = "Đăng ký thành công";
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, ví dụ: log thông báo lỗi hoặc thông báo cho người dùng
                errorMessages.Add("Không được để trống thông tin ");
            }

            ViewBag.ErrorMessages = errorMessages;

            // Chỉ hiển thị thông báo chung khi có ít nhất hai lỗi
            if (errorMessages.Count >= 2)
            {
                ViewBag.InvalidInput = true;
            }

            return View();
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