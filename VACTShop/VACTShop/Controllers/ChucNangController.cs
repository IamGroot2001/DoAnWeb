using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;
using PagedList;
using System.IO;

namespace VACTShop.Controllers
{
    public class ChucNangController : Controller
    {
        dbWebQuanAoNamDataContext context = new dbWebQuanAoNamDataContext();
        //------------------------------ Sản Phẩm ---------------------------------------vinh
        public ActionResult DSsanpham(int? page)
        {
            if(Session["TKAdmin"]==null)
            {
                return RedirectToAction("Index", "Home");
            }
            int pagesize = 5;
            int pageNum = (page ?? 1);
            var list = context.SANPHAMs.OrderByDescending(s => s.MaSP).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }
        public ActionResult loai(int id)
        {
            var list = context.LOAISANPHAMs.Where(n => n.MaLSP == id);
            return View(list.Single());
        }
        public ActionResult NCC(int id)
        {
            var list = context.NHACUNGCAPs.Where(n => n.MaNCC == id);
            return View(list.Single());
        }
        [HttpGet]
        public ActionResult Themsanpham()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.MaNCC = new SelectList(context.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.Loai = new SelectList(context.LOAISANPHAMs.ToList().OrderBy(n => n.MaLSP), "MaLSP", "TenLSP");
            return View();
        }
        [HttpPost]
        [ValidateInput (false)]
        public ActionResult Themsanpham(SANPHAM sp, FormCollection collection, HttpPostedFileBase fileUpload)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.MaNCC = new SelectList(context.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.Loai = new SelectList(context.LOAISANPHAMs.ToList().OrderBy(n => n.MaLSP), "MaLSP", "TenLSP");

            var TenSp = collection["Ten"];
            var gia = collection["Gia"];
            var Mota = collection["textarea"];
            var Date = collection["Date"];
            var SoLuong = collection["SoLuong"];
            var loai = collection["Loai"];
            var ncc = collection["MaNCC"];
            var size = collection["Size"];


            var filename = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/Asset/img"), filename);
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBaoAnh = "Hình Ảnh Đã Tồn Tại";
                return View();
            }
            else
            {
                fileUpload.SaveAs(path);
            }

            sp.TenSP = TenSp;
            sp.GiaBan = decimal.Parse(gia);
            sp.MoTa = Mota;
            sp.NgayCapNhat = DateTime.Parse(Date);
            sp.SoLuongTon = Int32.Parse(SoLuong);
            sp.MaLSP = Int32.Parse(loai);
            sp.MaNCC = Int32.Parse(ncc);
            sp.AnhBia = filename;
            context.SANPHAMs.InsertOnSubmit(sp);
            context.SubmitChanges();
            return RedirectToAction("DSsanpham", "ChucNang");
        }
    //================================================Xoa san pham=======================================================//

        [HttpGet]
        public ActionResult Xoasanpham(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }
        [HttpPost,ActionName("XoaSanPham")]
        public ActionResult XNXoaSanPham(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.SANPHAMs.DeleteOnSubmit(sp);
                context.SubmitChanges();
                return RedirectToAction("DSsanpham");
            }
        }
        //================================================Sua san pham=======================================================//
        [HttpGet]
        public ActionResult Suasanpham(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaNCC = new SelectList(context.NHACUNGCAPs.ToList().OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaloaiSP = new SelectList(context.LOAISANPHAMs.ToList().OrderBy(n => n.MaLSP), "MaLSP", "TenLSP", sp.MaLSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpPost, ActionName("Suasanpham")]
        public ActionResult XacNhanSuasanpham(FormCollection collection, int id, HttpPostedFileBase fileUpload)
        {
            var img = "";
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (fileUpload != null)
            {
                img = Path.GetFileName(fileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Asset/img"), img);
                if (!System.IO.File.Exists(path))//Sản Phẩm Chưa Tồn Tại
                {
                    fileUpload.SaveAs(path);
                }
            }
            else
            {
                img = collection["Anh"];
            }
            SANPHAM sp = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            sp.AnhBia = img;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            UpdateModel(sp);
            context.SubmitChanges();
            return RedirectToAction("DSsanpham");
        }
        //=================================================================Details============================//
        public ActionResult Details(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SANPHAM ncc = context.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }
        //======================================================Loai San Pham=================================//
        public ActionResult DSLSP(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int pagesize = 10;
            int pageNum = (page ?? 1);
            var list = context.LOAISANPHAMs.OrderBy(s => s.MaLSP).ToList();
            return View(list.ToPagedList(pageNum, pagesize));
        }
        //====================================================Them Loai San Pham===============================//
        [HttpGet]
        public ActionResult ThemLSP()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemLSP(LOAISANPHAM lsp)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                context.LOAISANPHAMs.InsertOnSubmit(lsp);
                context.SubmitChanges();
                return RedirectToAction("DSLSP", "ChucNang");
            }
        }
        //==============================================================Sua Loai San Pham===================================//
        [HttpGet]
        public ActionResult SuaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LOAISANPHAM lsp = context.LOAISANPHAMs.SingleOrDefault(n => n.MaLSP == id);
                if (lsp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(lsp);
            }
        }

        [HttpPost, ActionName("SuaLSP")]
        public ActionResult XacNhanSuaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LOAISANPHAM ncc = context.LOAISANPHAMs.SingleOrDefault(n => n.MaLSP == id);
                ViewBag.MaLSP = ncc.MaLSP;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSLSP");
            }
        }

        //===============================================Xoa Loai San Pham======================================//
        [HttpGet]
        public ActionResult XoaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                LOAISANPHAM ncc = context.LOAISANPHAMs.SingleOrDefault(n => n.MaLSP == id);
                ViewBag.MaLSP = ncc.MaLSP;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }

        [HttpPost, ActionName("XoaLSP")]
        public ActionResult XNXoaLSP(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                LOAISANPHAM ncc = context.LOAISANPHAMs.SingleOrDefault(n => n.MaLSP == id);
                ViewBag.MaLSP = ncc.MaLSP;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.LOAISANPHAMs.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSLSP");
            }
        }

        //===================================================== Nhà cung cấp =======================================//
        public ActionResult DSNCC(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int pagesize = 10;
                int pageNum = (page ?? 1);
                var list = context.NHACUNGCAPs.OrderByDescending(s => s.MaNCC).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }
        //====================================================Them Nha Cung Cap============================================//
        [HttpGet]
        public ActionResult ThemNCC()
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemNCC(NHACUNGCAP ncc)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                context.NHACUNGCAPs.InsertOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSNCC", "ChucNang");
            }
        }
        //======================================================Sửa nhà cung cấp==================================================//
        [HttpGet]
        public ActionResult SuaNCC(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                NHACUNGCAP ncc = context.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }

        [HttpPost, ActionName("SuaNCC")]
        public ActionResult XacNhanSua(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                NHACUNGCAP ncc = context.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
                ViewBag.MaNCC = ncc.MaNCC;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSNCC");
            }
        }
        //============================================================Xoá nhà cung cấp===================================//
        [HttpGet]
        public ActionResult XoaNCC(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                NHACUNGCAP ncc = context.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
                ViewBag.MaNCC = ncc.MaNCC;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }
        [HttpPost, ActionName("XoaNCC")]
        public ActionResult XacNhanXoa(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                NHACUNGCAP ncc = context.NHACUNGCAPs.SingleOrDefault(n => n.MaNCC == id);
                ViewBag.MaNCC = ncc.MaNCC;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.NHACUNGCAPs.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSNCC");
            }
        }
        //===================================Góp Ý Khách Hàng==========================//
        public ActionResult DSGOPY(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int pagesize = 5;
                int pageNum = (page ?? 1);
                var list = context.HOTROs.OrderByDescending(s => s.MaKH).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }
        //===================================Chi Tiet Góp Ý Khách Hàng==========================//

        [HttpGet]
        public ActionResult GOPY_CHITIET(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HOTRO ht = context.HOTROs.SingleOrDefault(n => n.MaKH == id);
                ViewBag.MaKH = ht.MaKH;
                if (ht == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ht);
            }
        }
        //==============================Xoá Góp Ý Khách Hàng==========================//
        [HttpPost, ActionName("GOPY_CHITIET")]
        public ActionResult XNGopY_ChiTiet(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HOTRO ncc = context.HOTROs.SingleOrDefault(n => n.MaKH == id);
                ViewBag.MaKH = ncc.MaKH;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.HOTROs.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSGOPY");
            }
        }
        //--------------------------- Khách Hàng ----------------------------------//
        public ActionResult DSKH(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int pagesize = 7;
                int pageNum = (page ?? 1);
                var list = context.KHACHHANGs.OrderByDescending(s => s.MaKH).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }
        //====================================Sửa Khách Hàng==================================//

        [HttpGet]
        public ActionResult SuaKH(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                KHACHHANG ncc = context.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }

        [HttpPost, ActionName("SuaKH")]
        public ActionResult XacNhanSuaKH(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                KHACHHANG ncc = context.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
                ViewBag.MaKH = ncc.MaKH;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSKH");
            }
        }
        //======================================================Xoá Khách Hàng======================================//
        [HttpGet]
        public ActionResult XoaKH(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                KHACHHANG ncc = context.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
                ViewBag.MaKH = ncc.MaKH;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ncc);
            }
        }
        [HttpPost, ActionName("XoaKH")]
        public ActionResult XacNhanXoaKH(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                KHACHHANG ncc = context.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
                ViewBag.MaKH = ncc.MaKH;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.KHACHHANGs.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DSKH");
            }
        }
        //==========================================Đơn Đặt Hàng==============================//
        [HttpGet]
        public ActionResult DonDatHang(int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string date = "01/01/2001";
                int pagesize = 5; 
                int pageNum = (page ?? 1);
                var GioHienTai = DateTime.Parse(date);
                var list = context.DONDATHANGs.Where(s => s.NgayDat >= GioHienTai).OrderByDescending(i => i.NgayDat).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }
        [HttpPost]
        public ActionResult DonDatHang(string date, string date2, int? page)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                int pagesize = 5;
                int pageNum = (page ?? 1);
                var Date = DateTime.Parse(date);

                if (date2 == "")
                {
                    var listdate = context.DONDATHANGs.Where(s => s.NgayDat >= Date).OrderByDescending(i => i.NgayDat).ToList();
                    return View(listdate.ToPagedList(pageNum, pagesize));
                }
                var Date2 = DateTime.Parse(date2);
                var list = context.DONDATHANGs.Where(s => s.NgayDat >= Date && s.NgayDat <= Date2).OrderByDescending(i => i.NgayDat).ToList();
                return View(list.ToPagedList(pageNum, pagesize));
            }
        }
        //=========================Chi Tiêt Đơn Đặt Hàng=================================////
        public ActionResult ChiTietDonDatHang(int? id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return HttpNotFound();
            }          
            var list = context.CHITIETDONDATHANGs.Where(s => s.MaDDH == id).OrderByDescending(s => s.MaSP).ToList();
            return View(list);
        }
        //========================================Xoá Tất Cả Chi Tiết Của Đơn Đặt Hàng đó===========================//

        public ActionResult XoaTatChiTietDonDatHang(int? id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                CHITIETDONDATHANG ncc = context.CHITIETDONDATHANGs.Where(n => n.MaDDH == id).FirstOrDefault();
                ViewBag.MaDonHang = ncc.MaDDH;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.CHITIETDONDATHANGs.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DonDatHang", "ChucNang");
            }
        }
        //============================================Xoá Đơn Đặt Hàng==============================// 
        public ActionResult XoaDonDatHang(int? id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                DONDATHANG ncc = context.DONDATHANGs.Where(n => n.MaDDH == id).FirstOrDefault();

                ViewBag.MaDonHang = ncc.MaDDH;
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                context.DONDATHANGs.DeleteOnSubmit(ncc);
                context.SubmitChanges();
                return RedirectToAction("DonDatHang");
            }
        }
        //=============================================Sửa Đơn Đặt Hàng====================================//

        [HttpGet]
        public ActionResult SuaDonDatHang(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            DONDATHANG ncc = context.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ncc);
        }

        [HttpPost, ActionName("SuaDonDatHang")]
        public ActionResult XacNhanSuaDonDatHang(int id)
        {
            if (Session["TKAdmin"] == null)
            {
                return RedirectToAction("Index", "Fashion");
            }
            else
            {
                DONDATHANG ncc = context.DONDATHANGs.SingleOrDefault(n => n.MaDDH == id);
                if (ncc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                UpdateModel(ncc);
                context.SubmitChanges();
                return RedirectToAction("DonDatHang");
            }
        }

    }
}