using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VACTShop.Models;
using PagedList;

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
            int pagesize = 10;
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
    }
}