using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;
using VACTShop.VNPAY;

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
        public List<VANCHUYEN> GetVANCHUYENVMs()
        {
            var vanchuyen = new List<VANCHUYEN>();
            return vanchuyen;
        }
        public ActionResult NVC(int id)
        {
            var list = data.VANCHUYENs.Where(n => n.MaVC == id);
            return View(list.Single());
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
            ViewBag.TenVanChuyen = new SelectList(data.VANCHUYENs.ToList().OrderBy(n => n.MaVC), "MaVC", "TenVanChuyen");
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
            ViewBag.VC = new SelectList(data.VANCHUYENs.ToList().OrderBy(n => n.MaVC), "MaVC", "TenVanChuyen");
            return View(gioHangs);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            // Thêm đơn hàng
            VANCHUYEN vc = new VANCHUYEN();
            ViewBag.VC = new SelectList(GetVANCHUYENVMs(), "MaVC", "TenVanChuyen");
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
            string DiaChi = collection["DiaChi"];
            string nvc = collection["TenVanChuyen"];
            ddh.DiaChi = DiaChi;

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
                
                data.CHITIETDONDATHANGs.InsertOnSubmit(ctddh);
            }
            data.SubmitChanges();

            //Mail xác nhận đặt hàng
            string subject = "Biên nhận";
            string mess = "Cảm ơn " + kh.HoTenKH + " đã đặt hàng!\n" +
                            "Mã đơn hàng: " + ddh.MaDDH + "\n" +
                            "Ngày đặt hàng: " + String.Format("{0:dd/MM/yyyy}", ddh.NgayDat) + "\n" +
                            "Ngày giao: " + String.Format("{0:dd/MM/yyyy}", ddh.NgayGiao) + "\n" +
                            "Tổng tiền: " + String.Format("{0:0,0}", ddh.ThanhTien) + " vnđ"+"\n"+
                            "Địa chỉ: " + kh.DiaChiKH + "\n"+
                            "Đơn vị vận chuyển: " + vc.TenVanChuyen + "\n";

            SendEmail(kh.EmailKH, subject, mess);


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
        public ActionResult Payment(FormCollection collection)
        {
            List<GioHang> gh = Session["GioHang"] as List<GioHang>;
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOHFYY20220624";
            string accessKey = "WnATmOxM3j81MiWC";
            string serectkey = "abJUbwX6vQRQx55Tj1HZccoJqMMIuyaz";
            string orderInfo = "test";
            string returnUrl = "https://localhost:44388/GioHang/ConfirmPaymentClient";
            string notifyurl = "https://53d0-123-21-167-207.ap.ngrok.io/GioHang/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = gh.Sum(n => n.thanhtien).ToString();//sửa lại giá để lấi giá đơn hàng
            string orderid = DateTime.Now.Ticks.ToString();
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            JObject jmessage = JObject.Parse(responseFromMomo);
            //luu database
            VANCHUYEN vc = new VANCHUYEN();
            ViewBag.VC = new SelectList(GetVANCHUYENVMs(), "MaVC", "TenVanChuyen");
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            var ngaygiao = DateTime.Now;
            ddh.NgayGiao = ngaygiao;
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            ddh.ThanhTien = Decimal.Parse(TongTien().ToString());
            string DiaChi = collection["DiaChi"];
            string nvc = collection["TenVanChuyen"];
            ddh.DiaChi = DiaChi;

            /*ddh.DiaChi = diaChi;*/
            /*ddh.DiaChi = String.Format(collection["DiaChi"]);*/
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();

            // Thêm chi tiết đơn hàng
            foreach (var item in gh)
            {
                CHITIETDONDATHANG ctddh = new CHITIETDONDATHANG();
                ctddh.MaDDH = ddh.MaDDH;
                ctddh.MaSP = item.masp;
                ctddh.SoLuong = item.soluong;
                ctddh.DonGia = (decimal)item.giaban;

                data.CHITIETDONDATHANGs.InsertOnSubmit(ctddh);
            }
            data.SubmitChanges();

            //Mail xác nhận đặt hàng
            string subject = "Biên nhận";
            string mess = "Cảm ơn " + kh.HoTenKH + " đã đặt hàng!\n" +
                            "Mã đơn hàng: " + ddh.MaDDH + "\n" +
                            "Ngày đặt hàng: " + String.Format("{0:dd/MM/yyyy}", ddh.NgayDat) + "\n" +
                            "Ngày giao: " + String.Format("{0:dd/MM/yyyy}", ddh.NgayGiao) + "\n" +
                            "Tổng tiền: " + String.Format("{0:0,0}", ddh.ThanhTien) + " vnđ" + "\n" +
                            "Địa chỉ: " + kh.DiaChiKH + "\n" +
                            "Đơn vị vận chuyển: " + vc.TenVanChuyen + "\n";

            SendEmail(kh.EmailKH, subject, mess);
            Session["GioHang"] = null;
            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public ActionResult ConfirmPaymentClient()
        {
            return PartialView();
        }
        public ActionResult XacNhanDonHang()
        {
            return PartialView();
        }
        public ActionResult PaymentVNPAY()
        {
            List<GioHang> gioHangs = Session["GioHang"] as List<GioHang>;

            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];
            string amount = gioHangs.Sum(n => n.thanhtien * 100).ToString();//sửa lại giá để lấi giá đơn hàng


            XuLy pay = new XuLy();

            //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Version", "2.1.0");

            //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_Command", "pay");

            //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_TmnCode", tmnCode);

            //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_Amount", amount);

            //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_BankCode", "");

            //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

            //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_CurrCode", "VND");

            //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_IpAddr", ChuyenDoi.GetIpAddress());

            //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_Locale", "vn");

            //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang");

            //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_OrderType", "other");

            //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_ReturnUrl", returnUrl);

            //mã hóa đơn
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString());

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);
            Session["GioHang"] = null;
            return Redirect(paymentUrl);
        }

        public ActionResult PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                XuLy pay = new XuLy();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //mã hóa đơn
                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef"));

                //mã giao dịch tại hệ thống VNPAY
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo"));

                //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode");
                //hash của dữ liệu trả về
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                //check chữ ký đúng hay không?
                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret);

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        //Thanh toán thành công
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return View();
        }
    }
}