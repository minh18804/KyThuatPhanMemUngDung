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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
namespace Test
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Chuỗi Initialize kết nối tới SQL:
        /// <param name="Data Source">: Nếu sử dụng cơ sở dữ liệu từ máy local thì sử dụng "localhost" còn không thì điền IP của CSDL </param>
        /// <param name="Initial Catalog">: Tên CSDL</param>
        /// <param name="Integrated Security">: luôn để True </param>
        /// </summary>
        private string connectionString = "Data Source=localhost; Initial Catalog=contact; Integrated Security=True";

        /// <summary>
        /// Hàm xử lý login: Nếu username hoặc user.Password trống thì hiện cảnh báo
        /// Nếu thông tin đúng thì bắt đầu phần quyền từ case 0 đến case 2 tương ứng chức vụ từ cao đến thấp
        /// </summary>
        private void PerformLogin()
        {
            User user = new User();
            user.Username = Username.Text;
            user.Password = Password.Password;
            bool? isAdmin = admin.IsChecked;
            bool? isCapXa = nhanVien.IsChecked;
            bool? isCapHuyen = khach.IsChecked;
            int roles;
            if (isAdmin == true) roles = 0;
            else if (isCapXa == true) roles = 1;
            else if (isCapHuyen == true) roles = 2;
            else roles = 3;

            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("Không được để trống Username hoặc Password", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Username=@username AND Password=@password AND Roles = @roles";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@roles", roles);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 1)
                {
                    query = "SELECT Roles, FirstName, LastName FROM Users WHERE Username=@username AND Password=@password";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    user.Roles = (int)reader["Roles"];
                    user.Firstname = (string)reader["FirstName"];
                    user.Lastname = (string)reader["LastName"];
                    switch (user.Roles)
                    {
                        case 0:
                            AdminWindow adminWindow = new AdminWindow(user);
                            adminWindow.Show();
                            Close();
                            break;
                        case 1:
                            NhanVienWindow nhanVienWindow = new NhanVienWindow(user.Username, user.Password, (string)reader["LastName"]);
                            nhanVienWindow.Show();
                            Close();
                            break;
                        case 2:
                            KhachWindow khachWindow = new KhachWindow(user.Username, user.Password, (string)reader["LastName"]);
                            khachWindow.Show();
                            Close();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Username hoặc password không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Question);
                }
            }
        }

        /// <summary>
        /// Hàm xử lý sự kiện login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_Click(object sender, RoutedEventArgs e)
        {
            PerformLogin();
        }
        /// <summary>
        /// Hàm xử lý sự kiện register: chuyển sang màn hình register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            Hide();
        }
        /// <summary>
        /// Hàm xử lý sự kiện bấm Enter là tự động đăng nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void username_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                PerformLogin();
            }
        }
        private void password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                PerformLogin();
            }
        }
        /// <summary>
        /// Hàm xử lý sự kiện changePassword: chuyển sang màn hình thay đổi mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordChange_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow ChangePasswordWindow = new ChangePasswordWindow(this);
            ChangePasswordWindow.Show();
            Hide();
        }

        private void nhanVien_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
