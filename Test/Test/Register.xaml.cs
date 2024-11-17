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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }
        private string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";
        public enum EnumRoles
        {
            GiamDoc,
            NhanVien,
            Khach
        }
        private int GenerateID(EnumRoles role)
        {
            Random random = new Random();
            string prefix;
            switch (role)
            {
                case EnumRoles.GiamDoc:
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;
            string firstname = FirstName.Text;
            string lastname = LastName.Text;
            string _roles = Roles.SelectedItem.ToString();
            EnumRoles temproles = (EnumRoles)Enum.Parse(typeof(EnumRoles), _roles); 
            int roles = (int)temproles;
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Không được để trống username hoặc password", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }  
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

                MainWindow login = new MainWindow();
                login.Show();
                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            Close();
        }

        private void Roles_Loaded(object sender, RoutedEventArgs e)
        {
            Roles.Items.Add("GiamDoc");
            Roles.Items.Add("NhanVien");
            Roles.Items.Add("Khach");
        }
    }
}
