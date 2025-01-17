using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SDT { get; set; }
        public string CapTrucThuoc { get; set; }
        public string TenXa { get; set; }
        public string TenHuyen { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
    }
    public class CanBoNghiepVu : User
    {
        public bool IsAdmin { get; set; }
    }
    public class CongTy : User
    {
        public string LinhVuc { get; set; }
        public string GiongVatNuoi { get; set; }
        public string NguonGenGiong { get; set; }
        public string ThucAnChanNuoi { get; set; }
        public int SoLuong { get; set; }
        public string TenLoaiConVat { get; set; }
        public string TrangThaiConVat { get; set; }
        public bool Banned { get; set; }
    }
    public class LoaiConVat
    {
        public string TenLoaiConVat { get; set; }
        public string TrangThai { get; set; }
    }
}
