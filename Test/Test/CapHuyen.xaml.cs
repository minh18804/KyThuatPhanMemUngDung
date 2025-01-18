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
    /// Interaction logic for NhanVienWindow.xaml
    /// </summary>
    public partial class CapHuyenWindow : Window
    {
        CanBoNghiepVu user;
        public CapHuyenWindow(CanBoNghiepVu user)
        {
            this.user = user;
            InitializeComponent();
            Greeting.Content = $"Xin chào, {user.Name}";
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, "UPDATE CongTy SET LogoutTime = @logouttime WHERE Username = @username", cmd =>
            {
                cmd.Parameters.AddWithValue("@logouttime", DateTime.Now);
                cmd.Parameters.AddWithValue("@username", user.Username);
            });

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
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

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userInfoWindow = new UserInfoWindow(user, this);
            userInfoWindow.Show();
            popupInfoMenu.IsOpen = false;
            Hide();
        }

        private void QLNS_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }

        private void quanLyGiongVatNuoi_Click(object sender, RoutedEventArgs e)
        {
            QuanLyGiongVatNuoiWindow quanLyGiongVatNuoiWindow = new QuanLyGiongVatNuoiWindow(user, this);
            quanLyGiongVatNuoiWindow.Show();
            Hide();
        }

        private void quanLyNguonGen_Click(object sender, RoutedEventArgs e)
        {
            QuanLyNguonGenGiongWindow quanLyNguonGenGiongWindow = new QuanLyNguonGenGiongWindow(user, this);
            quanLyNguonGenGiongWindow.Show();
            Hide();
        }

        private void quanLyThucAnChanNuoi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
