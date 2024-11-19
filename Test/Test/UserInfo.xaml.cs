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
    /// Interaction logic for UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        /// <summary>
        /// Khởi tạo các biến toàn cục và lưu các dữ liệu từ cửa sổ trước
        /// </summary>
        private string username;
        private string password;
        private Window previousWindow;
        private bool _isPasswordVisible;
        public UserInfoWindow(string _username, Window _previousWindow)
        {
            InitializeComponent();
            username = _username;
            previousWindow = _previousWindow;
            LoadUserInfo();
        }

        /// <summary>
        /// Lấy dữ liệu người dùng từ SQL
        /// </summary>
        private void LoadUserInfo()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT FirstName, LastName, Roles, Username, Password FROM Users WHERE Username=@username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    FirstNameText.Text = reader["FirstName"].ToString();
                    LastNameText.Text = reader["LastName"].ToString();

                    if ((int)reader["Roles"] == 0) RolesText.Text = "Admin";
                    else if ((int)reader["Roles"] == 1) RolesText.Text = "Nhân viên";
                    else RolesText.Text = "Khách";

                    UsernameText.Text = reader["Username"].ToString();
                    password = reader["Password"].ToString();
                }
            }
        }

        /// <summary>
        /// Nút đăng xuất
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

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupMenu.IsOpen = !popupMenu.IsOpen;
        }
        private void showHide_Click(object sender, RoutedEventArgs e)
        {
            if (_isPasswordVisible)
            {
                PasswordText.Text = "***";
                showHide.Content = "Hiện";
                _isPasswordVisible = false;
            }
            else
            {
                PasswordText.Text = password;
                showHide.Content = "Ẩn";
                _isPasswordVisible = true;
            }
        }
        private void return_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }

        private void changeInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void test1_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }

        private void test2_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }
        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = false;
        }

        private void QLNS_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(this);
            popupInfoMenu.IsOpen = false;
            changePasswordWindow.Show();
            Hide();
        }
    }
}

