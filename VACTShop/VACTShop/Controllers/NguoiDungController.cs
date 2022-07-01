using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;

namespace VACTShop.Controllers
{
    public class NguoiDungController : Controller
    {
        dbWebQuanAoNamDataContext data = new dbWebQuanAoNamDataContext();
        private static readonly int CHECK_EMAIL = 1;
        private static readonly int CHECK_TAIKHOAN = 1;

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        // GET: NguoiDung    
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HOTEN"];
            var email = collection["EMAIL"];
            var diachi = collection["DIACHI"];
            var dienthoai = collection["DIENTHOAI"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NGAYSINH"]);
            var taikhoan = collection["TAIKHOAN"];
            var matkhau = collection["MATKHAU"];
            var nhaplaimatkhau = collection["NHAPLAIMATKHAU"];
            if(matkhau.Equals(nhaplaimatkhau))
            {
                if (hoten == null || email == null || diachi == null || dienthoai == null || ngaysinh == null || taikhoan == null || matkhau == null)
                {
                    return HttpNotFound();
                }
                else if(checkUser(email,CHECK_EMAIL))
                {
                    ViewData["error"] = "Email này đã tồn tại";
                    return this.DangKy();
                }
                else if (checkTaiKhoan(taikhoan,CHECK_TAIKHOAN))
                {
                    ViewData["error"] = "Tài Khoản này đã tồn tại";
                    return this.DangKy();
                }
                else
                {
                    //=====================Mã Hoá Mật Khẩu========================//
                    MD5 md5 = new MD5CryptoServiceProvider();

                    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(nhaplaimatkhau));

                    byte[] bytedata = md5.Hash;

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytedata.Length; i++)
                    {

                        builder.Append(bytedata[i].ToString("x2"));
                    }

                    string MaHoa = builder.ToString();
                    //=======================Kết Thúc Mã Hoá======================//

                    kh.HoTenKH = hoten;
                    kh.EmailKH = email;
                    kh.DiaChiKH = diachi;
                    kh.DienThoaiKH = dienthoai;
                    kh.NgaySinhKH = DateTime.Parse(ngaysinh);
                    kh.TaiKhoanKH = taikhoan;
                    kh.MatKhauKH = MaHoa;
                    data.KHACHHANGs.InsertOnSubmit(kh);
                    data.SubmitChanges();

                    //sentmail
                    string subject = "VACT Shop";
                    string mess = "Chào mừng " + kh.HoTenKH + " đến với The VACT Shop";
                    SendEmail(kh.EmailKH, subject, mess);
                    return RedirectToAction("DangNhap","NguoiDung");
                }
            } 
            else
            {
                ViewData["error"] = "Mật Khẩu Không Trùng Khớp";
                return this.DangKy();
            }    
        }
        private bool checkUser(string str, int value)
        {
            if (value == 1)
            {
                var a = data.KHACHHANGs.FirstOrDefault(p => p.EmailKH == str);
                if (a != null) return true;
            }
            return false;
        }
        private bool checkTaiKhoan(string str, int value)
        {
            if (value == 1)
            {
                var a = data.KHACHHANGs.FirstOrDefault(p => p.TaiKhoanKH == str);
                if (a != null) return true;
            }
            return false;
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            string TK = collection["TaiKhoan"];
            string MK = collection["Password"];
            //====================================Bắt Đầu Mã Hoá=======================//
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(MK));

            byte[] bytedata = md5.Hash;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytedata.Length; i++)
            {

                builder.Append(bytedata[i].ToString("x2"));
            }

            string MaHoa = builder.ToString();
            //==================================Kết Thúc Mã Hoá============================//
            //var user = data.KHACHHANGs.SingleOrDefault(a => a.TaiKhoanKH == TK);

            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(a => a.TaiKhoanKH == TK && a.MatKhauKH == MaHoa);
            if (kh != null)
            {
                Session["User"] = kh.HoTenKH;
                Session["Taikhoan"] = kh;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Thongbao = "Tên Tài Khoản Hoặc Mật Khẩu Không Đúng";
            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["User"] = null;
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
        //Gửi Mail
        public static void SendEmail(string address, string subject, string message)
        {
            if (new EmailAddressAttribute().IsValid(address)) // check có đúng mail khách hàng
            {
                string email = "buivanty15@gmail.com";
                var senderEmail = new MailAddress(email, "VACT Shop (tin nhắn tự động)");
                var receiverEmail = new MailAddress(address, "Receiver");
                var password = "dpukaghhwhgrokpo";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
        } 

        [HttpGet]
        public ActionResult ChangePassword()
        {
            KHACHHANG user = (KHACHHANG)Session["TaiKhoan"];
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var user = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == kh.MaKH);
            var po = collection["passold"];
            var pn = collection["passnew"];
            var pa = collection["passagain"];
            if (String.IsNullOrEmpty(po) || String.IsNullOrEmpty(pn) || String.IsNullOrEmpty(pa))
            {
                ViewData["1"] = "Thông tin không được để trống";
            }
            else if (String.IsNullOrEmpty(po) && String.IsNullOrEmpty(pn) && !String.IsNullOrEmpty(pa))
            {
                ViewData["3"] = "Vui lòng nhập mật khẩu mới!";
                return this.ChangePassword();
            }
            else if (!String.IsNullOrEmpty(po) && !String.IsNullOrEmpty(pn) && !String.IsNullOrEmpty(pa))
            {
                if (!String.Equals(MD5Hash(po), kh.MatKhauKH))
                {
                    ViewData["1"] = "Mật khẩu không đúng!";
                    return this.ChangePassword();
                }
                else if (!String.Equals(pn, pa))
                {
                    ViewData["3"] = "Mật khẩu mới và mật khẩu cũ không trùng khớp!";
                    return this.ChangePassword();
                }
                else
                {
                    user.MatKhauKH = MD5Hash(pn);
                    Session["User"] = kh.HoTenKH;
                    data.SubmitChanges();
                    ViewData["3"] = "Cập nhật thành công!";
                    return RedirectToAction("Index", "Home");
                }
            }
            return this.ChangePassword();
        }
        [HttpGet]
        public ActionResult ThongTinKhachHang()
        {
            KHACHHANG user = (KHACHHANG)Session["TaiKhoan"];
            if (user == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult ThongTinKhachHang(FormCollection collection)
        {
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var user = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == kh.MaKH);
            var hoten = collection["HOTEN"];
            var email = collection["EMAIL"];
            var diachi = collection["DIACHI"];
            var dienthoai = collection["DIENTHOAI"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NGAYSINH"]);
            if(hoten == null || email == null || diachi == null || dienthoai == null ||ngaysinh==null)
            {
                ViewBag.Error = "Thông tin không được để trống";
                return this.ThongTinKhachHang();
            }
            else
            {
                user.HoTenKH = hoten;
                user.EmailKH = email;
                user.DiaChiKH = diachi;
                user.DienThoaiKH = dienthoai;
                kh.NgaySinhKH = DateTime.Parse(ngaysinh);
                Session["TaiKhoan"] = kh;
                data.SubmitChanges();
                ViewData["Info"] = "Cập nhật thành công !";
                return RedirectToAction("Index", "Home");
            }
            return this.ThongTinKhachHang();
        }


    }
}