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
using System.Windows.Threading;

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
        private DispatcherTimer timer;
        private bool _isPasswordVisible;
        string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";

        public UserInfoWindow(string _username, Window _previousWindow)
        {
            InitializeComponent();
            InitializeClock();
            username = _username;
            previousWindow = _previousWindow;
            LoadUserInfo();
        }

        /// <summary>
        /// Lấy dữ liệu người dùng từ SQL
        /// </summary>
        private void LoadUserInfo()
        {
            using (SqlConnection conn   = new SqlConnection(connectionString))
            {
                conn.Open();
                string query            = "SELECT c.CompanyID, c.CompanyName, c.Username, a.LevelName, c.Password, c.LoginTime, c.LogoutTime, x.TenXa, h.TenHuyen, CASE WHEN c.AdministratorLevel = 2 THEN x.TenXa WHEN c.AdministratorLevel = 1 THEN h.TenHuyen END AS AdministrativeName FROM Company c LEFT JOIN Xa x ON c.IDXa = x.IDXa LEFT JOIN Huyen h ON c.IDHuyen = h.IDHuyen LEFT JOIN AdministratorLevel a on c.AdministratorLevel = a.LevelID WHERE Username = @username";
                SqlCommand cmd          = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    companyID.Text      = reader["CompanyID"].ToString();
                    companyName.Text    = reader["CompanyName"].ToString() ;
                    adminLevel.Text     = reader["LevelName"].ToString();
                    if (adminLevel.Text == "Xa")    tenXaHuyen.Text = reader["TenXa"].ToString();
                    else                            tenXaHuyen.Text = reader["TenHuyen"].ToString();
                    UsernameText.Text   = reader["Username"].ToString();
                    password            = reader["Password"].ToString();
                    PasswordText.Text = "***";
                }
            }
        }
        public void InitializeClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE Company SET LogoutTime = @logouttime WHERE Username = @username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@logouttime", DateTime.Now);
                cmd.Parameters.AddWithValue("@username", username);
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

