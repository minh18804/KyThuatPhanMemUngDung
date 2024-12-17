using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class QuanLyGiongVatNuoiWindow : Window
    {
        User user;
        Window previousWindow;
        List<Company> companies;
        string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=true";
        public QuanLyGiongVatNuoiWindow(User user, Window previousWindow)
        {
            this.user = user;
            this.previousWindow = previousWindow;
            InitializeComponent();
            Greeting.Content = $"Xin chào, {user.CompanyName}";
            LoadUserData();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Company SET LogoutTime = @logouttime WHERE Username = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@logouttime", DateTime.Now);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.ExecuteNonQuery();
            }

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userInfoWindow = new UserInfoWindow(user.Username, this);
            userInfoWindow.Show();
            popupInfoMenu.IsOpen = false;
            Hide();
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupMenu.IsOpen = !popupMenu.IsOpen;
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(this);
            changePasswordWindow.Show();
            Hide();
        }

        private void LoadUserData()
        {
            userDataGrid.ItemsSource = null;

            companies = new List<Company>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT congty.IDCongTy, congty.Ten, huyen.TenHuyen, congty.SDT, loaikinhdoanh.TenGiongVatNuoi FROM CongTy congty JOIN AdministratorLevel admin ON congty.CapTrucThuoc = admin.LevelID JOIN Huyen huyen ON congty.IDHuyen = huyen.IDHuyen JOIN GiongVatNuoi loaikinhdoanh ON congty.IDGiongVatNuoi = loaikinhdoanh.IDGiongVatNuoi WHERE congty.IDLinhVuc = 1 AND congty.CapTrucThuoc = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    companies.Add(new Company
                    {
                        IDCongTy = (int)reader["IDCongTy"],
                        Name = reader["Ten"].ToString(),
                        TenHuyen = reader["TenHuyen"].ToString(),
                        SDT = reader["SDT"].ToString(),
                        TenGiongVatNuoi = reader["TenGiongVatNuoi"].ToString()
                    });
                    userDataGrid.ItemsSource = companies;
                }
            }
        }
        public class Company
        {
            public int IDCongTy { get; set; }
            public string Name { get; set; }
            public string TenHuyen { get; set; }
            public string SDT { get; set; }
            public string TenGiongVatNuoi { get; set; }
        }
    }
}
