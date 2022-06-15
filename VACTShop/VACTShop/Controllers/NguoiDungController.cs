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
                else
                {
                    kh.HoTenKH = hoten;
                    kh.EmailKH = email;
                    kh.DiaChiKH = diachi;
                    kh.DienThoaiKH = dienthoai;
                    kh.NgaySinhKH = DateTime.Parse(ngaysinh);
                    kh.TaiKhoanKH = taikhoan;
                    kh.MatKhauKH = matkhau;
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
                
                @ViewData["error"] = "Mật Khẩu Không Trùng Khớp";
                return this.DangKy();
            }    
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
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(a => a.TaiKhoanKH == TK && a.MatKhauKH == MK);
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
    }
}