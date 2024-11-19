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
    /// Interaction logic for Window5.xaml
    /// </summary>

    public partial class Window5 : Window
    {   
        private Window _previousWindow;
        public string _username;
        public string _password;
        public Window5(string username, string password, Window previousWindow)
        {
            InitializeComponent();
            _username = username;
            _password = password;
            _previousWindow = previousWindow;
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            if (_newPassword.Text != newPassword.Text)
            {
                MessageBox.Show("Mật khẩu mới không trùng khớp với phần xác nhận mật khẩu", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (oldPassword.Text != _password)
            {
                MessageBox.Show("Mật khẩu cũ không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery = "UPDATE Users SET Password = @password WHERE Username = @username";
                
                SqlCommand Cmd = new SqlCommand(checkQuery, conn);
                Cmd.Parameters.AddWithValue("@username", _username);
                Cmd.Parameters.AddWithValue("@password", _password);

                MessageBox.Show("Đổi mật khẩu thành công, vui lòng đăng nhập lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);

                MainWindow login = new MainWindow();
                login.Show();
                this.Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            _previousWindow.Show();
            Close();
        }
    }
}
