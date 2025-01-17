using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace Test
{
    public static class Provider
    {
        public static bool ValidateUser(CanBoNghiepVu user)
        {
            if (user.IsAdmin)
            {
                return SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, "SELECT COUNT(1) FROM CanBoNghiepVu WHERE Username=@username AND Password=@password AND isAdmin=@isadmin",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
                    }) == 1;
            }
            else
            {
                return SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, "SELECT COUNT(1) FROM CanBoNghiepVu c JOIN AdministratorLevel a ON c.AdministratorLevel = a.LevelID WHERE c.Username=@username AND c.Password=@password AND a.LevelName=@levelname",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@levelname", user.CapTrucThuoc);
                    }) == 1;
            }
        }
        public static bool ValidateCompany(CongTy company)
        {
            return SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, "SELECT COUNT(1) FROM CongTy WHERE Username=@username AND Password=@password", 
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@username", company.Username);
                    cmd.Parameters.AddWithValue("@password", company.Password);
                }) == 1;
        }
        public static bool ValidateUser(CanBoNghiepVu user, string recoveryCode)
        {
            bool isValid = false;
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT RecoveryCode1, RecoveryCode2, RecoveryCode3 FROM CanBoNghiepVu WHERE Username = @username",
                cmd => cmd.Parameters.AddWithValue("@username", user.Username),
                reader =>
                    {
                        if (reader.Read())
                        {
                            if ((reader["RecoveryCode1"].ToString() == recoveryCode) ||
                                (reader["RecoveryCode2"].ToString() == recoveryCode) ||
                                (reader["RecoveryCode3"].ToString() == recoveryCode))
                            {
                                isValid = true;
                            }
                        } 
                    });
            return isValid;
        }
        public static void SetLoginTime(CanBoNghiepVu user)
        {
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, "UPDATE CanBoNghiepVu SET LoginTime = @logintime WHERE Username = @username",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@logintime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                });
        }
        public static void SetLogoutTime(CanBoNghiepVu user)
        {
            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, "UPDATE CanBoNghiepVu SET LoginTime = @logintime WHERE Username = @username",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@logintime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                });

        }
        public static CanBoNghiepVu LoadUserData(CanBoNghiepVu user)
        {
            CanBoNghiepVu _user = new CanBoNghiepVu();
            _user = user;
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT c.CanBoNghiepVuName, x.TenXa, h.TenHuyen, c.CanBoNghiepVuID, c.Password FROM CanBoNghiepVu c JOIN Xa x ON c.IDXa = x.IDXa JOIN Huyen h ON c.IDHuyen = h.IDHuyen WHERE Username = @username",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@username", user.Username);
                },
                reader =>
                {
                    if (reader.Read())
                    {
                        _user.ID = reader["CanBoNghiepVuID"] != DBNull.Value ? (int)reader["CanBoNghiepVuID"] : 0;
                        _user.Name = reader["CanBoNghiepVuName"] != DBNull.Value ? reader["CanBoNghiepVuName"].ToString() : string.Empty;
                        _user.Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : string.Empty;
                        _user.TenXa = reader["TenXa"] != DBNull.Value ? reader["TenXa"].ToString() : string.Empty;
                        _user.TenHuyen = reader["TenHuyen"] != DBNull.Value ? reader["TenHuyen"].ToString() : string.Empty;
                    }
                });

            return _user;
        }
        public static List<CanBoNghiepVu> LoadUsersData()
        {
            List<CanBoNghiepVu> _users = new List<CanBoNghiepVu>();
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT c.SDT, c.CanBoNghiepVuID, c.CanBoNghiepVuName, c.Username, a.LevelName, c.Password, c.LoginTime, c.LogoutTime, x.TenXa, h.TenHuyen, CASE WHEN c.AdministratorLevel = 2 THEN x.TenXa WHEN c.AdministratorLevel = 1 THEN h.TenHuyen END AS AdministrativeName FROM CanBoNghiepVu c LEFT JOIN Xa x ON c.IDXa = x.IDXa LEFT JOIN Huyen h ON c.IDHuyen = h.IDHuyen LEFT JOIN AdministratorLevel a on c.AdministratorLevel = a.LevelID WHERE isAdmin = 0",
                cmd => { },
                reader =>
                {
                    while (reader.Read())
                    {
                        _users.Add(new CanBoNghiepVu
                        {
                            ID = (int)reader["CanBoNghiepVuID"],
                            Name = reader["CanBoNghiepVuName"]?.ToString(),
                            Username = reader["Username"]?.ToString(),
                            Password = "***",
                            CapTrucThuoc = reader["LevelName"].ToString(),
                            SDT = reader["SDT"].ToString(),
                            TenXa = reader["TenXa"] != DBNull.Value ? reader["TenXa"].ToString() : null,
                            TenHuyen = reader["TenHuyen"] != DBNull.Value ? reader["TenHuyen"].ToString() : null,
                            LoginTime = reader["LoginTime"] != DBNull.Value ? (DateTime?)reader["LoginTime"] : null,
                            LogoutTime = reader["LogoutTime"] != DBNull.Value ? (DateTime?)reader["LogoutTime"] : null,
                            IsAdmin = false
                        });

                    }
                });
            return _users;
        }
        public static CongTy LoadCompanyData(CongTy company)
        {
            CongTy _company = new CongTy();
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT c.IDCongTy, c.Ten, a.LevelName, c.Username, x.TenXa, h.TenHuyen, c.SDT, l.TenLinhVuc, g.TenGiongVatNuoi, n.TenNguonGenGiong, t.TenThucAnChanNuoi, c.SoLuong, convat.TenConVat, convat.TrangThaiConVat, c.LoginTime, c.LogoutTime, c.Banned FROM CongTy c JOIN AdministratorLevel a ON c.CapTrucThuoc = a.LevelID JOIN Xa x ON c.IDXa = x.IDXa JOIN Huyen h ON c.IDHuyen = h.IDHuyen JOIN LinhVuc l ON c.IDLinhVuc = l.IDLinhVuc JOIN GiongVatNuoi g ON c.IDGiongVatNuoi = g.IDGiongVatNuoi JOIN NguonGenGiong n ON c.IDNguonGenGiong = n.IDNguonGenGiong JOIN ThucAnChanNuoi t ON c.IDThucAnChanNuoi = t.IDThucAnChanNuoi JOIN LoaiConVat convat ON c.IDLoaiConVat = convat.IDLoaiConVat WHERE Username = @username AND Password = @password",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@username", company.Username);
                    cmd.Parameters.AddWithValue("@password", company.Password);
                },
                reader =>
                {
                    if (reader.Read())
                    {
                        _company.ID = (int)reader["IDCongTy"];
                        _company.Name = reader["Ten"]?.ToString();
                        _company.Username = reader["Username"]?.ToString();
                        _company.Password = "***";
                        _company.CapTrucThuoc = reader["LevelName"].ToString();
                        _company.TenXa = reader["TenXa"] != DBNull.Value ? reader["TenXa"].ToString() : null;
                        _company.TenHuyen = reader["TenHuyen"] != DBNull.Value ? reader["TenHuyen"].ToString() : null;
                        _company.SDT = reader["SDT"]?.ToString();
                        _company.LinhVuc = reader["TenLinhVuc"]?.ToString();
                        _company.GiongVatNuoi = reader["TenGiongVatNuoi"]?.ToString();
                        _company.NguonGenGiong = reader["TenNguonGenGiong"]?.ToString();
                        _company.ThucAnChanNuoi = reader["TenThucAnChanNuoi"]?.ToString();
                        _company.TenLoaiConVat = reader["TenConVat"].ToString();
                        _company.TrangThaiConVat = reader["TrangThaiConVat"].ToString();
                        _company.SoLuong = (int)reader["SoLuong"];
                        _company.LoginTime = reader["LoginTime"] != DBNull.Value ? (DateTime?)reader["LoginTime"] : null;
                        _company.LogoutTime = reader["LogoutTime"] != DBNull.Value ? (DateTime?)reader["LogoutTime"] : null;
                        _company.Banned = (bool)reader["Banned"];
                    }
                });
            return _company;
        }
        public static List<CongTy> LoadCompaniesData()
        {
            List<CongTy> _companies = new List<CongTy>();
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT c.IDCongTy, c.Ten, a.LevelName, c.Username, x.TenXa, h.TenHuyen, c.SDT, l.TenLinhVuc, g.TenGiongVatNuoi, n.TenNguonGenGiong, t.TenThucAnChanNuoi, c.SoLuong, convat.TenConVat, convat.TrangThaiConVat, c.LoginTime, c.LogoutTime, c.Banned  FROM CongTy c JOIN AdministratorLevel a ON c.CapTrucThuoc = a.LevelID JOIN Xa x ON c.IDXa = x.IDXa JOIN Huyen h ON c.IDHuyen = h.IDHuyen JOIN LinhVuc l ON c.IDLinhVuc = l.IDLinhVuc JOIN GiongVatNuoi g ON c.IDGiongVatNuoi = g.IDGIongVatNuoi JOIN NguonGenGiong n ON c.IDNguonGenGiong = n.IDNguonGenGiong JOIN ThucAnChanNuoi t ON c.IDThucAnChanNuoi = t.IDThucAnChanNuoi JOIN LoaiConVat convat ON c.IDLoaiConVat = convat.IDLoaiConVat",
            cmd => { },
                reader =>
                {
                    while (reader.Read())
                    {
                        _companies.Add(new CongTy
                        {
                            ID          = (int)reader["IDCongTy"],
                            Name        = reader["Ten"]?.ToString(),
                            Username    = reader["Username"]?.ToString(),
                            Password    = "***",
                            CapTrucThuoc = reader["LevelName"].ToString(),
                            TenXa       = reader["TenXa"] != DBNull.Value ? reader["TenXa"].ToString() : null,
                            TenHuyen    = reader["TenHuyen"] != DBNull.Value ? reader["TenHuyen"].ToString() : null,
                            SDT         = reader["SDT"]?.ToString(),
                            LinhVuc     = reader["TenLinhVuc"]?.ToString(),
                            GiongVatNuoi = reader["TenGiongVatNuoi"]?.ToString(),
                            NguonGenGiong = reader["TenNguonGenGiong"]?.ToString(),
                            ThucAnChanNuoi = reader["TenThucAnChanNuoi"]?.ToString(),
                            TenLoaiConVat = reader["TenConVat"].ToString(),
                            TrangThaiConVat = reader["TrangThaiConVat"].ToString(),
                            SoLuong = (int)reader["SoLuong"],
                            LoginTime = reader["LoginTime"] != DBNull.Value ? (DateTime?)reader["LoginTime"] : null,
                            LogoutTime = reader["LogoutTime"] != DBNull.Value ? (DateTime?)reader["LogoutTime"] : null,
                            Banned = reader["Banned"].Equals(true),
                        });

                    }
                });
            return _companies;
        }
        public static int NumberOfUsersOrCompany(string table)
        {
            return SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, $"SELECT COUNT(*) FROM {table}",
                cmd => { });
        }
        public static List<CanBoNghiepVu> SearchUsers(string searchText, List<CanBoNghiepVu> users)
        {
            if (string.IsNullOrEmpty(searchText))
                return users;

            return users.Where(user => (user.Username?.ToLower().Contains(searchText) ?? false) ||
                                       (user.Name?.ToLower().Contains(searchText) ?? false) ||
                                       (user.TenHuyen?.ToLower().Contains(searchText) ?? false) ||
                                       (user.TenXa?.ToLower().Contains(searchText) ?? false)).ToList();
        }
        public static List<CongTy> SearchCompany(string searchText, List<CongTy> companies)
        {
            if (string.IsNullOrEmpty(searchText))
                return companies;

            return companies.Where(company => (company.Username?.ToLower().Contains(searchText.ToLower()) ?? false) ||
                                  (company.Name?.ToLower().Contains(searchText.ToLower()) ?? false) ||
                                  (company.TenHuyen?.ToLower().Contains(searchText.ToLower()) ?? false) ||
                                  (company.TenXa?.ToLower().Contains(searchText.ToLower()) ?? false) ||
                                  (int.TryParse(searchText, out int id) && company.ID == id)).ToList();
        }
        public static string GetPasswordFromDatabase(string username, string table)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(table) || (table != "CanBoNghiepVu" && table != "CongTy")) 
                return null;

            string password = null;
            SqlHelper.ExecuteReader(SqlHelper.connectionString, $"SELECT Password FROM {table} WHERE Username = @Username",
                 cmd => cmd.Parameters.AddWithValue("@Username", username),
                 reader => {
                    if (reader.Read()) // Kiểm tra xem có dữ liệu để đọc không
                    { 
                         password = reader["Password"].ToString(); 
                    } 
                 });
            return password;
        }
        public static int GenerateID(CanBoNghiepVu canBoNghiepVu, int idHuyen, int idXa)
        {
            int administratorID;
            int soThuTu = 0;
            string canBoNghiepVuID;
            soThuTu = Provider.NumberOfUsersOrCompany("CanBoNghiepVu");
            string formattedSoThuTu = soThuTu.ToString("D3");
            string formattedIDHuyen = idHuyen.ToString("D3");
            string formattedIDXa = idXa.ToString("D3");

            if (canBoNghiepVu.CapTrucThuoc == "Huyen") administratorID = 2;
            else administratorID = 1;
            canBoNghiepVuID = $"{administratorID}{formattedIDHuyen}{formattedIDXa}{formattedSoThuTu}";
            return int.Parse(canBoNghiepVuID);
        }
        public static int GenerateID(CongTy congTy, int idHuyen, int idXa)
        {
            int administratorID;
            int soThuTu = 0;
            string congTyID;
            soThuTu = Provider.NumberOfUsersOrCompany("CongTy");
            string formattedSoThuTu = soThuTu.ToString("D4");
            string formattedIDHuyen = idHuyen.ToString("D3");
            string formattedIDXa = idXa.ToString("D3");

            if (congTy.CapTrucThuoc == "Huyen") administratorID = 2;
            else administratorID = 1;
            congTyID = $"2{administratorID}{formattedIDHuyen}{formattedIDXa}{formattedSoThuTu}";
            return int.Parse(congTyID);
        }
        public static string GenerateRecoveryCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            using (var crypto = new RNGCryptoServiceProvider())
            {
                var data = new byte[length];
                crypto.GetBytes(data);
                var result = new StringBuilder(length);
                foreach (var byteValue in data)
                {
                    result.Append(chars[byteValue % chars.Length]);
                }
                return result.ToString();
            }
        }
        public static bool SetUserData(CanBoNghiepVu canBoNghiepVu, string RecoveryCode1, string RecoveryCode2, string RecoveryCode3)
        {
            int rowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, "INSERT INTO CanBoNghiepVu (CanBoNghiepVuID, CanBoNghiepVuName, SDT, Username, Password, AdministratorLevel, IDXa, IDHuyen, RecoveryCode1, RecoveryCode2, RecoveryCode3, isAdmin) SELECT @canBoNghiepVuID, @canBoNghiepVuName, @sdt, @username, @password, a.LevelID, x.IDXa, (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenHuyen), @recoveryCode1, @recoveryCode2, @recoveryCode3, 0 FROM Xa x JOIN AdministratorLevel a ON a.LevelName = @administratorname WHERE x.TenXa = @tenXa;",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@canBoNghiepVuID", canBoNghiepVu.ID);
                    cmd.Parameters.AddWithValue("@canBoNghiepVuName", canBoNghiepVu.Name);
                    cmd.Parameters.AddWithValue("@sdt", canBoNghiepVu.SDT);
                    cmd.Parameters.AddWithValue("@username", canBoNghiepVu.Username);
                    cmd.Parameters.AddWithValue("@password", canBoNghiepVu.Password);
                    cmd.Parameters.AddWithValue("@administratorname", canBoNghiepVu.CapTrucThuoc);
                    cmd.Parameters.AddWithValue("@tenXa", canBoNghiepVu.TenXa);
                    cmd.Parameters.AddWithValue("@tenHuyen", canBoNghiepVu.TenHuyen);
                    cmd.Parameters.AddWithValue("@recoveryCode1", RecoveryCode1);
                    cmd.Parameters.AddWithValue("@recoveryCode2", RecoveryCode2);
                    cmd.Parameters.AddWithValue("@recoveryCode3", RecoveryCode3);
                });
            
            return rowsAffected > 0;
        }
        public static void HidePassword(ref List<CongTy> companies)
        {
            foreach (CongTy company in companies)
            {
                company.Password = "***";
            }
        }
        //public static int GetMaXaHoacHuyen(string CapTrucThuoc, string tenXaHuyen)
        //{
        //    if(CapTrucThuoc == "Xa")
        //    {
        //        SqlHelper.ExecuteScalar
        //    }
        //}

    }
    public static class ClockProvider
    {
        public static void InitializeClock(ref DispatcherTimer timer)
        {
            if (timer == null)
            {
                timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(1)
                };
                timer.Start();
            }
        }

    }
    //public class User
    //{
    //    public int ID { get; set; }
    //    public string Name { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string CapTrucThuoc { get; set; }
    //    public string TenXa { get; set; }
    //    public DateTime? LoginTime { get; set; }
    //    public DateTime? LogoutTime { get; set; }
    //    public string TenHuyen { get; set; }
    //}
    //public class CanBoNghiepVu : User
    //{
    //    public bool IsAdmin { get; set; }
    //}
    //public class CongTy : User
    //{
    //    public string LinhVuc { get; set; }
    //    public string GiongVatNuoi { get; set; }
    //    public string NguonGenGiong { get; set; }
    //    public string ThucAnChanNuoi { get; set; }
    //    public int SoLuong { get; set; }
    //    public string SDT { get; set; }
    //    public string TenLoaiConVat { get; set; }
    //    public string TrangThaiConVat { get; set; }
    //    public bool Banned { get; set; }
    //}
}
