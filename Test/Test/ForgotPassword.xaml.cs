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
    public partial class ForgotPasswordWindow : Window
    {
        private Window previousWindow;
        public ForgotPasswordWindow(Window previousWindow)
        {
            InitializeComponent();
            this.previousWindow = previousWindow;
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_newPassword.Password != newPassword.Password)
            {
                MessageBox.Show("Mật khẩu mới không trùng khớp với phần xác nhận mật khẩu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (newPassword.Password.Length < 8)
            {
                MessageBox.Show("Mật khẩu mới quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string connectionString         = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";

            using (SqlConnection conn       = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery           = "SELECT RecoveryCode1, RecoveryCode2, RecoveryCode3 FROM Company WHERE Username = @username";
                SqlCommand checkCmd         = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", username.Text);

                SqlDataReader reader        = checkCmd.ExecuteReader();
                if (!reader.Read())
                {
                    MessageBox.Show("Không tồn tại tài khoản", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    reader.Close();
                    return;
                }
                else if ((reader["RecoveryCode1"].ToString() != recoveryCode.Text) && (reader["RecoveryCode2"].ToString() != recoveryCode.Text) && (reader["RecoveryCode3"].ToString() != recoveryCode.Text))
                {
                    MessageBox.Show("Mã khôi phục không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    reader.Close();
                    return;
                }
                reader.Close();

                string changeQuery = "UPDATE Company SET Password = @password WHERE Username = @username";
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
