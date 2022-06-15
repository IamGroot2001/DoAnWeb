using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Configuration;
using System.Data.SqlClient;
using VACTShop.Models;

namespace VACTShop.Controllers
{
    public class VACTController : Controller
    {
        dbWebQuanAoNamDataContext context = new dbWebQuanAoNamDataContext();
       // ------------------------------ Sản Phẩm ---------------------------------------vinh
        public ActionResult DSsanpham(int? page)
        {
            if (Session["TKAdmin"] == null)
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