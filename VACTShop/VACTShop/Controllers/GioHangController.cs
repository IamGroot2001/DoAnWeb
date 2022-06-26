﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        [HttpGet]
        public ActionResult DatHang()
        {

            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> gioHangs = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(gioHangs);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            // Thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            var ngaygiao = DateTime.Now;
            ddh.NgayGiao = ngaygiao;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.ThanhTien = Decimal.Parse(TongTien().ToString());
            String diaChi = collection["DiaChi"];
            /*ddh.DiaChi = diaChi;*/
            /*ddh.DiaChi = String.Format(collection["DiaChi"]);*/
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();

            // Thêm chi tiết đơn hàng
            foreach(var item in gh)
            {
                CHITIETDONDATHANG ctddh = new CHITIETDONDATHANG();
                ctddh.MaDDH = ddh.MaDDH;
                ctddh.MaSP = item.masp;
                ctddh.SoLuong = item.soluong;
                ctddh.DonGia = (decimal)item.giaban;
                ddh.DiaChi = diaChi;
                data.CHITIETDONDATHANGs.InsertOnSubmit(ctddh);
            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

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

        public ActionResult XacNhanDonHang()
        {
            return PartialView();
        }
    }
}