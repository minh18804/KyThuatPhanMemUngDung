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
            bool hasError = false;
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

            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT RecoveryCode1, RecoveryCode2, RecoveryCode3 FROM CanBoNghiepVu WHERE Username = @username",
                cmd => cmd.Parameters.AddWithValue("@username", username.Text),
                reader =>
                {
                    if (!reader.Read())
                    {
                        MessageBox.Show("Không tồn tại tài khoản", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        hasError = true;
                    }
                    else if ((reader["RecoveryCode1"].ToString() != recoveryCode.Text) && 
                            (reader["RecoveryCode2"].ToString() != recoveryCode.Text) && 
                            (reader["RecoveryCode3"].ToString() != recoveryCode.Text))
                    {
                        MessageBox.Show("Mã khôi phục không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        hasError = true;
                    }
                });

            if (!hasError)
            {
                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, "UPDATE CanBoNghiepVu SET Password = @password WHERE Username = @username",
                    cmd =>
                    {
                        cmd.Parameters.AddWithValue("@username", username.Text);
                        cmd.Parameters.AddWithValue("@password", newPassword.Password);
                    }
                );
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
