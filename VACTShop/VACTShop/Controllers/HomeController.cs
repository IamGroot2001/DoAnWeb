using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;
using PagedList;
using PagedList.Mvc;

namespace VACTShop.Controllers
{
    public class HomeController : Controller
    {

        dbWebQuanAoNamDataContext data = new dbWebQuanAoNamDataContext();
        private List<SANPHAM> LaySanPham(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            return View();
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

        public ActionResult HienThiDSSanPham(int? page)
        {
            // tạo biển quy định số sản phẩm trên mỗi trang
            int pagesize = 8;
            // tạo biển số trang
            int pageNum = (page ?? 1);
            var Sanphammoi = LaySanPham(12);
            return View(Sanphammoi.ToPagedList(pageNum, pagesize));
            /*var hienthiDSSanPham = LaySanPham(int.MaxValue);
            return PartialView(hienthiDSSanPham);*/
        }

        public ActionResult XemChiTietSanPham(int id)
        {
            var sanpham = from s in data.SANPHAMs
                          where s.MaSP == id
                          select s;
              return View(sanpham.Single());
        }

        public ActionResult HienThiSPTheoPhanLoai(int? id, int ? page)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            int pagesize = 9;
            int pageNum = (page ?? 1);
            var sp = from s in data.SANPHAMs where s.MaLSP == id select s;
            return View(sp.ToPagedList(pageNum, pagesize));
        }

        public ActionResult SPTheoThuongHieu(int? id, int? page)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            int pagesize = 9;
            int pageNum = (page ?? 1);
            var sp = from s in data.SANPHAMs where s.MaNCC == id select s;
            return View(sp.ToPagedList(pageNum, pagesize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Shop(int? page)
        {
            int pagesize = 9;
            int pageNum = (page ?? 1);
            var list = data.SANPHAMs.OrderByDescending(s => s.MaSP).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }

        public ActionResult HienThiThuongHieu()
        {
            var thuonghieu = from cd in data.NHACUNGCAPs select cd;
            return PartialView(thuonghieu);
        }

        public ActionResult GioHang()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

    }
}