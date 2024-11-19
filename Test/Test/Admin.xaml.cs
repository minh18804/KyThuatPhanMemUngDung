﻿using System;
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
using System.Data.SqlClient;

namespace Test
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        /// <summary>
        /// Tạo các biến toàn cục và lưu thông tin từ cửa số trước
        /// </summary>
        string username;
        string password;
        public AdminWindow(string _username, string _password, string _lastname)
        {
            InitializeComponent();
            Greeting.Content = $"Xin chào, Admin {_lastname}";                   
            InfoButton.Content = _lastname[0].ToString();                        
            username = _username;
            password = _password;
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
            UserInfoWindow userInfoWindow = new UserInfoWindow(username, this);
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
    }
}
