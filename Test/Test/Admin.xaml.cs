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
    public partial class AdminWindow : Window
    {
        private List<User> users = new List<User>();
        public User AdminUser;

        public AdminWindow(User adminUser)
        {
            InitializeComponent();
            Greeting.Content = $"Xin chào, Admin {adminUser.Lastname}";
            InfoButton.Content = adminUser.Lastname[0];
            AdminUser = adminUser;

            LoadUserData();

            string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=true";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string numberOfUserQuery = "SELECT COUNT(*) FROM Users";
                SqlCommand command = new SqlCommand(numberOfUserQuery, conn);
                numberOfUser.Content = command.ExecuteScalar();
            }
        }
        private void LoadUserData()
        {
            userDataGrid.ItemsSource = null;

            users = new List<User>();
            string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Username, Roles, FirstName, LastName, Password FROM Users";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Username = reader["Username"]?.ToString(),
                        Roles = reader["Roles"] != DBNull.Value ? (int)reader["Roles"] : -1,
                        Firstname = reader["FirstName"]?.ToString(),
                        Lastname = reader["LastName"]?.ToString(),
                        Password = "***"
                    });
                }
            }

            userDataGrid.ItemsSource = users;
        }

        private List<User> SearchUsers(string searchText)
        {
            return users.Where(user => (user.Username?.ToLower().Contains(searchText) ?? false) ||
                                       (user.Firstname?.ToLower().Contains(searchText) ?? false) ||
                                       (user.Lastname?.ToLower().Contains(searchText) ?? false)).ToList();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                userDataGrid.ItemsSource = users;
            }
            else
            {
                var filteredUsers = SearchUsers(searchText);
                userDataGrid.ItemsSource = filteredUsers;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupMenu.IsOpen = !popupMenu.IsOpen;
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }

        private void testt_Click(object sender, RoutedEventArgs e)
        {
            //Chưa phát triển
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            //popupInfoMenu.IsOpen = false;
            //UserInfoWindow userInfoWindow = new UserInfoWindow(username, this);
            //userInfoWindow.Show();
            //Hide();
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(this);
            changePasswordWindow.Show();
            Hide();
        }

        private void userInfoConfig_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = false;
            UserInfoConfigWindow userInfoConfigWindow = new UserInfoConfigWindow(AdminUser, this);
            userInfoConfigWindow.Show();
            Hide();
        }
        private string GetPasswordFromDatabase(string username) { 
            string password = ""; 
            string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True"; 
            using (SqlConnection conn = new SqlConnection(connectionString)) 
            { 
                conn.Open(); 
                string query = "SELECT Password FROM Users WHERE Username = @Username"; 
                SqlCommand cmd = new SqlCommand(query, conn); 
                cmd.Parameters.AddWithValue("@Username", username); 
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read()) 
                { 
                    password = reader["Password"].ToString(); 
                } 

            } 
            return password; 
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            string username = checkBox.Tag.ToString();
            User user = users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Password = GetPasswordFromDatabase(user.Username);
                userDataGrid.ItemsSource = null;
                userDataGrid.ItemsSource = users;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            string username = checkBox.Tag.ToString();
            User user = users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Password = "***";
                userDataGrid.ItemsSource= null;
                userDataGrid.ItemsSource = users;
            }
        }
    }
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Roles { get; set; }
    }
}

