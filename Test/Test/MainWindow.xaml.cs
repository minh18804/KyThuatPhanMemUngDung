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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //
        private string connectionString = "Data Source=localhost; Initial Catalog=contact; Integrated Security=True";

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            PerformLogin();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Window1 register = new Window1();
            register.Show();
            Hide();
        }

        private void Username_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                PerformLogin();
            }
        }

        private void Password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            string username = Username.Text;
            string password = Password.Password;
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("Không được để trống Username hoặc Password");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Username=@username AND Password=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 1)
                {
                    query = "SELECT Roles, FirstName, LastName FROM Users WHERE Username=@username AND Password=@password";
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    switch ((int)reader["Roles"])
                    {
                        case 0:
                            Window2 _GiamDoc = new Window2(username, password, (string)reader["LastName"]);
                            _GiamDoc.Show();
                            Close();
                            break;
                        case 1:
                            Window3 _NhanVien = new Window3(username, password, (string)reader["LastName"]);
                            _NhanVien.Show();
                            Close();
                            break;
                        case 2:
                            Window4 _Khach = new Window4(username, password, (string)reader["LastName"]);
                            _Khach.Show();
                            Close();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Username hoặc password không đúng");
                }
            }
        }

        private void PasswordChange_Click(object sender, RoutedEventArgs e)
        {
            Window5 DoiMatKhau = new Window5(null, null, this);
            DoiMatKhau.Show();
            Hide();
        }
    }
}
