using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;

namespace VACTShop.Controllers
{
    public class GioHangController : Controller
    {
        dbWebQuanAoNamDataContext data = new dbWebQuanAoNamDataContext();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> list = Session["GioHang"] as List<GioHang>;
            // nếu hàng chưa tồn tại thì khởi tạo listGiohang
            if (list == null)
            {
                list = new List<GioHang>();
                Session["GioHang"] = list;
            }
            return list;
        }

        // thêm vào giỏ hàng
        public ActionResult ThemGioHang(int masp, string strUrl)
        {
            // lấy ra session Gio hang
            List<GioHang> gioHangs = LayGioHang();
            // kiểm tra sản phẩm này đã tồn tại trong giỏ hàng Session["GioHang"] chưa
            GioHang sp = gioHangs.Find(n => n.masp == masp);
            if (sp == null)
            {
                sp = new GioHang(masp);
                gioHangs.Add(sp);
                return Redirect(strUrl);
            }
            else
            {
                sp.soluong++;
                return Redirect(strUrl);
            }
        }
        // Tong so luong
        private int TongSoLuong()
        {
            int Tongsoluong = 0;
            List<GioHang> gioHangs = Session["GioHang"] as List<GioHang>;
            if (gioHangs != null)
            {
                Tongsoluong = gioHangs.Sum(n => n.soluong);
            }
            Session["TongSoLuong"] = Tongsoluong;
            return Tongsoluong;
        }
        // Tính tổng tiền
        private double TongTien()
        {
            double tongtien = 0;
            List<GioHang> gioHangs = Session["GioHang"] as List<GioHang>;
            if (gioHangs != null)
            {
                tongtien = gioHangs.Sum(n => n.thanhtien);
            }
            return tongtien;
        }
        // Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            List<GioHang> gioHangs = LayGioHang();
            if (gioHangs.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(gioHangs);
        }
        public ActionResult SoLuongGioHang()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGioHang(int id)
        {
            List<GioHang> gioHangs = LayGioHang();
            GioHang sessiongiohang = gioHangs.SingleOrDefault(n => n.masp == id);
            if (sessiongiohang != null)
            {
                gioHangs.RemoveAll(n => n.masp == id);
                return RedirectToAction("GioHang");
            }
            if (gioHangs.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int id, FormCollection f)
        {
            List<GioHang> gioHangs = LayGioHang();
            GioHang sessiongiohang = gioHangs.SingleOrDefault(n => n.masp == id);
            if (sessiongiohang != null)
            {
                sessiongiohang.soluong = int.Parse(f["Soluong"].ToString());

            }
            return RedirectToAction("Giohang");
        }
        public ActionResult RemoveAll()
        {
            List<GioHang> gioHangs = LayGioHang();
            gioHangs.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}