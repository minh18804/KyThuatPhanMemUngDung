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
using System.Data.SqlClient;

namespace Test
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>

    public partial class ChangePasswordWindow : Window
    {   
        /// <summary>
        /// Tạo các biến toàn cục lưu các biến từ cửa số trước
        /// </summary>
        private Window previousWindow;
        public ChangePasswordWindow(Window _previousWindow)
        {
            InitializeComponent();
            previousWindow = _previousWindow;
        }
        /// <summary>
        /// Hàm đổi mật khẩu:
        /// - Nếu password mới và password nhập lại không trùng nhau thì báo lỗi
        /// - Lấy dữ liệu từ SQL, nếu password cũ không trùng với password có trong CSDL thì báo lỗi
        /// - Thay đổi mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_newPassword.Password != newPassword.Password)
            {
                MessageBox.Show("Mật khẩu mới không trùng khớp với phần xác nhận mật khẩu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(newPassword.Password.Length < 8)
            {
                MessageBox.Show("Mật khẩu mới quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT Password FROM Users WHERE Username = @username";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", username.Text);

                SqlDataReader reader = checkCmd.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("Mật khẩu cũ không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    reader.Close();
                    return;
                }
                else if (reader["Password"].ToString() != oldPassword.Password)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    reader.Close();
                    return;
                }
                reader.Close();

                string changeQuery = "UPDATE Users SET Password = @password WHERE Username = @username";
                SqlCommand changeCmd = new SqlCommand(changeQuery, conn);
                changeCmd.Parameters.AddWithValue("@username", username.Text);
                changeCmd.Parameters.AddWithValue("@password", newPassword.Password);
                changeCmd.ExecuteNonQuery();
                MessageBox.Show("Đổi mật khẩu thành công, vui lòng đăng nhập lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }
    }
}
