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
        public ActionResult Index(int ? page)
        {
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var Sachmoi = LaySanPham(20);
            return View(Sachmoi.ToPagedList(pageNum, pagesize));
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
            return PartialView(hienthiDSSanPham);
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

      

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(FormCollection collection)
        {


            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "NguoiDung");
            }
            else
            {
                HOTRO ht = new HOTRO();
                KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
                var user = data.KHACHHANGs.SingleOrDefault(p => p.MaKH == kh.MaKH);
                ht.MaKH = kh.MaKH;
                ht.MaHoTen = kh.HoTenKH;
                ht.Email = kh.EmailKH;
                string lydo = collection["LyDo"];
                ht.LyDo = lydo;
                if (lydo == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    data.HOTROs.InsertOnSubmit(ht);
                    data.SubmitChanges();
                    return RedirectToAction("Contact", "Home");
                }
            }
        }

        public ActionResult Shop()
        {
            return View();
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