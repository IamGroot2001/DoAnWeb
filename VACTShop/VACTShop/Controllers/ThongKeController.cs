using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;

namespace VACTShop.Controllers
{
    public class ThongKeController : Controller
    {
        dbWebQuanAoNamDataContext db = new dbWebQuanAoNamDataContext();
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThongKe()
        {
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
        private int TrangThaiGiaoHang()
        {
            bool a = true;
            var dem = (from s in db.DONDATHANGs where s.TinhTrangGiaoHang == a select s).Count();
            return dem;
        }
        private int fTrangThaiGiaoHang()
        {
            bool a = false;
            var dem = (from s in db.DONDATHANGs where s.TinhTrangGiaoHang == a select s).Count();
            return dem;
        }

        private int TrangThaiDonHang()
        {
            bool a = true;
            var dem = (from s in db.DONDATHANGs where s.DaThanhToan == a select s).Count();
            return dem;
        }
        private int fTrangThaiDonHang()
        {
            bool a = false;
            var dem = (from s in db.DONDATHANGs where s.DaThanhToan == a select s).Count();
            return dem;
        }

        private int TongDoanhThu()
        {
            int tong = 0;
            var dem = db.DONDATHANGs.OrderByDescending(s => s.ThanhTien).Count();
            foreach (var item in db.DONDATHANGs)
            {
                tong = (int)(tong + item.ThanhTien);
            }
            return tong;
        }

        private int TongDonHang()
        {
            var dem = db.DONDATHANGs.OrderByDescending(s => s.MaDDH).Count();
            return dem;
        }

        private int TongSanPham()
        {
            var dem = db.SANPHAMs.OrderByDescending(s => s.MaSP).Count();
            return dem;
        }

        private int TongKhachHang()
        {
            var dem = db.KHACHHANGs.OrderByDescending(s => s.MaKH).Count();
            return dem;
        }
    }
}