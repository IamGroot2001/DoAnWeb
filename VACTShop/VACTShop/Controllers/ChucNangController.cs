using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;

namespace VACTShop.Controllers
{
    public class ChucNangController : Controller
    {
        dbWebQuanAoNamDataContext data = new dbWebQuanAoNamDataContext();
        private List<SANPHAM> LaySanPham(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        // GET: ChucNang
        public ActionResult HienThiHangMoiVe()
        {
            var hangmoive = LaySanPham(8);
            return PartialView(hangmoive);
        }
        public ActionResult HienThiLoaiSanPham()
        {
            var loaiSanPham = from cd in data.LOAISANPHAMs select cd;
            return PartialView(loaiSanPham);
        }

        public ActionResult HienThiDSSanPham()
        {
            var hienthiDSSanPham = LaySanPham(int.MaxValue);
            return View(hienthiDSSanPham);
        }   

        public ActionResult XemChiTietSanPham(int id)
        {
            var sanpham = from s in data.SANPHAMs
                          where s.MaSP == id
                          select s;
            return View(sanpham.Single());
        }

        public ActionResult HienThiSPTheoPhanLoai(int id)
        {
            var sp = from s in data.LOAISANPHAMs where s.MaLSP == id select s;
            return PartialView(sp);
        }
    }
}