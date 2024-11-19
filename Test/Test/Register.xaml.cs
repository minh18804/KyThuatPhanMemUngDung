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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        /// <summary>
        /// Tạo cấp độ chức vụ từ 0->2 tương ứng từ Admin->Khach
        /// </summary>
        public enum EnumRoles
        {
            Admin,
            NhanVien,
            Khach
        }
        public RegisterWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Chi tiết ở MainWindow.xaml.cs
        /// </summary>
        private string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";

        /// <summary>
        /// Thêm các lựa chọn cho ComboBox chức vụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Roles_Loaded(object sender, RoutedEventArgs e)
        {
            Roles.Items.Add("Admin");
            Roles.Items.Add("NhanVien");
            Roles.Items.Add("Khach");
        }
        /// <summary>
        /// Hàm tạo mã số nhân viên
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private int GenerateID(EnumRoles role)
        {
            Random random = new Random();
            string prefix;
            switch (role)
            {
                case EnumRoles.Admin:
                    prefix = "0";
                    break;
                case EnumRoles.NhanVien:
                    prefix = "1";
                    break;
                case EnumRoles.Khach:
                    prefix = "2";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            string randomNumber = random.Next(100000, 999999).ToString();
            return int.Parse(prefix + randomNumber);
        }

        /// <summary>
        /// Hàm xử lý sự kiện register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Username.Text) || String.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Không được để trống username hoặc password", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(LastName.Text))
            {
                MessageBox.Show("Không được để trống họ và tên", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Roles.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn chức vụ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(Username.Text.Length < 8 || Password.Password.Length < 8)
            {
                MessageBox.Show("Username hoặc password quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string username = Username.Text;
            string password = Password.Password;
            string firstname = FirstName.Text;
            string lastname = LastName.Text;
            string _roles = Roles.SelectedItem.ToString();
            EnumRoles temproles = (EnumRoles)Enum.Parse(typeof(EnumRoles), _roles); 
            int roles = (int)temproles;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username"; 
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn); 
                checkCmd.Parameters.AddWithValue("@username", username); 
                int userCount = (int)checkCmd.ExecuteScalar(); 
                if (userCount > 0) { 
                    MessageBox.Show("Username đã tồn tại. Vui lòng chọn username khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error); 
                    return; 
                }

                string query = "INSERT INTO Users (Username, Password, FirstName, Lastname, Roles) VALUES (@username, @password, @firstname, @lastname, @roles)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@roles", roles);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đăng ký thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
        }

        /// <summary>
        /// Hàm xử lý sự kiện back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
