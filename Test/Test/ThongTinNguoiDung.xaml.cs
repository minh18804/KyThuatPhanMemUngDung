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
    /// Interaction logic for Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        private string _username;
        private string _password;
        private Window _previousWindow;
        private bool _isPasswordVisible;
        public Window6(string username, Window previousWindow)
        {
            InitializeComponent();
            _username = username;
            _previousWindow = previousWindow;
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT FirstName, LastName, Roles, Username, Password FROM Users WHERE Username=@username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", _username);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    FirstNameText.Text = reader["FirstName"].ToString();
                    LastNameText.Text = reader["LastName"].ToString();
                    RolesText.Text = reader["Roles"].ToString();
                    UsernameText.Text = reader["Username"].ToString();
                    _password = reader["Password"].ToString();
                }
            }
        }


        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupMenu.IsOpen = !popupMenu.IsOpen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isPasswordVisible)
            {
                PasswordText.Text = "***";
                ShowHideButton.Content = "Hiện";
                _isPasswordVisible = false;
            }
            else
            {
                PasswordText.Text = _password;
                ShowHideButton.Content = "Ẩn";
                _isPasswordVisible = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Handle Test button click
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // Handle Test1 button click
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Handle Đổi mật khẩu button click
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // Handle Thông tin người dùng button click
        }
    }
}

