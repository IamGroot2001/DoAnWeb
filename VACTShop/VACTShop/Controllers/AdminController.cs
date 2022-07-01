using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;
namespace VACTShop.Controllers
{
    public class AdminController : Controller
    {
        dbWebQuanAoNamDataContext context= new dbWebQuanAoNamDataContext();
        public ActionResult Index()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.TongDonHang = TongDonHang();
            ViewBag.TongSanPham = TongSanPham();
            ViewBag.TongKhachHang = TongKhachHang();
            ViewBag.TongDoanhThu = TongDoanhThu();
            ViewBag.TrangThaiDonHang = TrangThaiDonHang();
            ViewBag.fTrangThaiDonHang = fTrangThaiDonHang();
            ViewBag.TrangThaiGiaoHang = TrangThaiGiaoHang();
            ViewBag.fTrangThaiGiaoHang = fTrangThaiGiaoHang();

            return View();
        }
       
        //Tính toán các thống kế
        private int TrangThaiGiaoHang()
        {
            bool a = true;
            var dem = (from s in context.DONDATHANGs where s.TinhTrangGiaoHang == a select s).Count();
            //var count = db.Invoices.OrderByDescending(s => s.StatusInvoice = a).Count();
            return dem;
        }
        private int fTrangThaiGiaoHang()
        {
            bool a = false;
            var dem = (from s in context.DONDATHANGs where s.TinhTrangGiaoHang == a select s).Count();
            //var count = db.Invoices.OrderByDescending(s => s.StatusInvoice = a).Count();
            return dem;
        }

        private int TrangThaiDonHang()
        {
            bool a = true;
            var dem = (from s in context.DONDATHANGs where s.DaThanhToan == a select s).Count();
            //var count = db.Invoices.OrderByDescending(s => s.StatusInvoice = a).Count();
            return dem;
        }
        private int fTrangThaiDonHang()
        {
            bool a = false;
            var dem = (from s in context.DONDATHANGs where s.DaThanhToan == a select s).Count();
            //var count = db.Invoices.OrderByDescending(s => s.StatusInvoice = a).Count();
            return dem;
        }

        private int TongDoanhThu()
        {
            int tong = 0;
            var dem = context.DONDATHANGs.OrderByDescending(s => s.ThanhTien).Count();
            foreach (var item in context.DONDATHANGs)
            {
                tong = (int)(tong + item.ThanhTien);
            }
            return tong;
        }

        private int TongDonHang()
        {
            var dem = context.DONDATHANGs.OrderByDescending(s => s.MaDDH).Count();
            return dem;
        }

        private int TongSanPham()
        {
            var dem = context.SANPHAMs.OrderByDescending(s => s.MaSP).Count();
            return dem;
        }

        private int TongKhachHang()
        {
            var dem = context.KHACHHANGs.OrderByDescending(s => s.MaKH).Count();
            return dem;
        }
        //===============================Đăng Nhập Admin==============================//
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            string user = collection["form-username"];
            string pass = collection["form-password"];

            ADMIN ad = context.ADMINs.SingleOrDefault(a => a.TaiKhoanAdmin == user && a.MatKhauAdmin == pass);
            if (ad == null)
            {
                ViewBag.ThongBaoAdmin = "Tài Khoản Hoặc Mật Khẩu Sai";
                return this.Login();
            }
            Session["TKAdmin"] = ad;
            return RedirectToAction("Index", "Admin");
        }
    }
}