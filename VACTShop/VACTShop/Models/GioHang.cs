using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VACTShop.Models
{
    public class GioHang
    {
        dbWebQuanAoNamDataContext context = new dbWebQuanAoNamDataContext();
        public int masp { set; get; }
        public string tensp { set; get; }
        public string mota { set; get; }
        public string anhbia { set; get; }
        public int soluong { set; get; }
        public double giaban { set; get; }
        public double thanhtien { get { return giaban * soluong; } }

        public GioHang(int MaSP)
        {
            masp = MaSP;
            SANPHAM sp = context.SANPHAMs.Single(s => s.MaSP == masp);
            mota = sp.MoTa;
            tensp = sp.TenSP;
            anhbia = sp.AnhBia;
            giaban = double.Parse(sp.GiaBan.ToString());
            soluong = 1;
        }
    }
}