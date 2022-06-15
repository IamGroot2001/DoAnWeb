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
            return View();
        }
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