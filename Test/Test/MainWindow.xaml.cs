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
            bool? isCapXa = capXa.IsChecked;
            bool? isCapHuyen = capHuyen.IsChecked;
            if (isAdmin == true) user.isAdmin = true;
            else if (isCapXa == true) user.AdministratorLevel = "Xa";
            else if (isCapHuyen == true) user.AdministratorLevel = "Huyen";

            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("Không được để trống Username hoặc Password", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (isAdmin == false)
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM Company c JOIN AdministratorLevel a ON c.AdministratorLevel = a.LevelID WHERE c.Username=@username AND c.Password=@password AND a.LevelName=@levelname";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@levelname", user.AdministratorLevel);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        query = "UPDATE Company SET LoginTime = @logintime WHERE Username = @username";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@logintime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.ExecuteNonQuery();

                        query = "SELECT c.CompanyName, x.TenXa, h.TenHuyen, c.CompanyID FROM Company c JOIN Xa x ON c.IDXa = x.IDXa JOIN Huyen h ON c.IDHuyen = h.IDHuyen WHERE Username=@username AND Password=@password";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.ExecuteNonQuery();
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        user.CompanyID = (int)reader["CompanyID"];
                        user.CompanyName = (string)reader["CompanyName"];
                        user.TenXa = (string)reader["TenXa"];
                        user.TenHuyen = (string)reader["TenHuyen"];
                        switch (user.AdministratorLevel)
                        {
                            case "Xa":
                                CapXaWindow capXaWindow = new CapXaWindow(user);
                                capXaWindow.Show();
                                Close();
                                break;
                            case "Huyen":
                                CapHuyenWindow capHuyenWindow = new CapHuyenWindow(user);
                                capHuyenWindow.Show();
                                Close();
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username hoặc password không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
                }
                else
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM Company WHERE Username=@username AND Password=@password AND isAdmin=@isadmin";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@isadmin", user.isAdmin);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        query = "UPDATE Company SET LoginTime = @logintime WHERE Username = @username";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@logintime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@username", user.Username);
                        cmd.ExecuteNonQuery();

                        AdminWindow adminWindow = new AdminWindow(user);
                        adminWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Username hoặc password không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Question);
                    }
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
    }
}
