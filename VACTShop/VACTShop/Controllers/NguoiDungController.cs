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
    }
}