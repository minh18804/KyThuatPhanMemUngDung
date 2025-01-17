using System;
using System.Collections.Generic;
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
    /// Interaction logic for KhachWindow.xaml
    /// </summary>
    public partial class CapXaWindow : Window
    {
        CanBoNghiepVu user;

        public CapXaWindow(CanBoNghiepVu _user)
        {
            InitializeComponent();
            user = _user;
        }
        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            Provider.SetLogoutTime(user);
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
        /// <summary>
        /// Menu chức năng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupMenu.IsOpen = !popupMenu.IsOpen;
        }

        private void QLNS_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }

        private void testt_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }
        /// <summary>
        /// Nút thông tin cá nhân (nút có hình là chữ cái đầu tiên của tên người dùng)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }
        /// <summary>
        /// Nút thông tin người dùng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userInfoWindow = new UserInfoWindow(user, this);
            userInfoWindow.Show();
            popupInfoMenu.IsOpen = false;
            Hide();
        }
        /// <summary>
        /// Nút thay đổi thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(this);
            changePasswordWindow.Show();
            Hide();
        }

        private void quanLyGiongVatNuoi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void quanLyNguonGen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void quanLyThucAnChanNuoi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }
    }
}