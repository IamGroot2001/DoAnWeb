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
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection collection, int? page)
        {
            string sTuKhoa = collection["txtTimKiem"].ToString();
            if(sTuKhoa==null)
            {
                List<SANPHAM> lstKQTK = data.SANPHAMs.Where(n => n.TenSP.Contains((string)Session["TuKhoa"])).ToList();
                //phan trang
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                if(lstKQTK.Count==0)
                {
                    ViewBag.ThongBao = "Không tìm thấy sản phẩm nào cả";
                    return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
                }
                ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kêt quả!";
                return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                Session["TuKhoa"] = sTuKhoa;
                List<SANPHAM> lstKQTK = data.SANPHAMs.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();
                //Phân trang 
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                if (lstKQTK.Count == 0)
                {
                    ViewBag.ThongBao = "Không tìm thấy sản phẩm nào cả";
                    return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
                }
                ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kêt quả!";
                return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpGet]
        public ActionResult KetQuaTimKiem(int? page,string sTuKhoa)
        {
            if(sTuKhoa!=null)
            {
                Session["TuKhoa"] = sTuKhoa;
                List<SANPHAM> lstKQTK = data.SANPHAMs.Where(n => n.TenSP.Contains(sTuKhoa)).ToList();
                //Phân trang 
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                if (lstKQTK.Count == 0)
                {
                    ViewBag.ThongBao = "Không tìm thấy sản phẩm nào cả";
                    return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
                }
                ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count +" kêt quả!";
                return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                List<SANPHAM> lstKQTK = data.SANPHAMs.Where(n => n.TenSP.Contains((string)Session["TuKhoa"])).ToList();
                //phan trang
                int pageNumber = (page ?? 1);
                int pageSize = 8;
                if (lstKQTK.Count == 0)
                {
                    ViewBag.ThongBao = "Không tìm thấy sản phẩm nào cả";
                    return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
                }
                ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " kêt quả!";
                return View(lstKQTK.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult SapXepSanPham(string kieuSapXep, int?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if(kieuSapXep==null)
            {
                kieuSapXep = Session["KieuSapXep"].ToString();
                if(kieuSapXep=="giamdan")
                {
                    Session["KieuSapXep"] = "giamdan";
                    ViewBag.SapXepSanPham = "Sản phẩm theo giá giảm dàn";
                    var sanphamgiamdan = data.SANPHAMs.OrderByDescending(s => s.GiaBan);
                    return View(sanphamgiamdan.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    Session["KieuSapXep"] = "tangdan";
                    ViewBag.SapXepSanPham = "Sản phẩm theo giá tăng dần";
                    var sanphamtangdan = data.SANPHAMs.OrderBy(s => s.GiaBan);
                    return View(sanphamtangdan.ToPagedList(pageNumber, pageSize));
                }
            }
            else
            {
                if (kieuSapXep == "giamdan")
                {
                    Session["KieuSapXep"] = "giamdan";
                    ViewBag.SapXepSanPham = "Sản phẩm theo giá giảm dàn";
                    var sanphamgiamdan = data.SANPHAMs.OrderByDescending(s => s.GiaBan);
                    return View(sanphamgiamdan.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    Session["KieuSapXep"] = "tangdan";
                    ViewBag.SapXepSanPham = "Sản phẩm theo giá tăng dần";
                    var sanphamtangdan = data.SANPHAMs.OrderBy(s => s.GiaBan);
                    return View(sanphamtangdan.ToPagedList(pageNumber, pageSize));
                }
            }
        }
    }
}