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
using System.Windows.Threading;

namespace Test
{
    public partial class AdminWindow : Window
    {
        private List<User> users        = new List<User>();
        public User AdminUser;
        private DispatcherTimer timer;
        string connectionString         = "Data Source=localhost;Initial Catalog=contact;Integrated Security=true";

        public AdminWindow(User adminUser)
        {
            InitializeComponent();
            InitializeClock();
            Greeting.Content            = $"Xin chào, Admin";
            InfoButton.Content          = " ";
            AdminUser = adminUser;

            LoadUserData();

            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string numberOfUserQuery    = "SELECT COUNT(*) FROM Company";
                SqlCommand command          = new SqlCommand(numberOfUserQuery, conn);
                numberOfUser.Content        = command.ExecuteScalar();
            }
        }
        private void LoadUserData()
        {
            userDataGrid.ItemsSource = null;

            users = new List<User>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query            = "SELECT c.CompanyID, c.CompanyName, c.Username, a.LevelName, c.Password, c.LoginTime, c.LogoutTime, x.TenXa, h.TenHuyen, CASE WHEN c.AdministratorLevel = 2 THEN x.TenXa WHEN c.AdministratorLevel = 1 THEN h.TenHuyen END AS AdministrativeName FROM Company c LEFT JOIN Xa x ON c.IDXa = x.IDXa LEFT JOIN Huyen h ON c.IDHuyen = h.IDHuyen LEFT JOIN AdministratorLevel a on c.AdministratorLevel = a.LevelID WHERE isAdmin = 0";
                SqlCommand cmd          = new SqlCommand(query, conn);
                SqlDataReader reader    = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        CompanyID               = (int)reader["CompanyID"],
                        CompanyName             = reader["CompanyName"]?.ToString(),
                        Username                = reader["Username"]?.ToString(),
                        AdministratorLevel      = reader["LevelName"].ToString(),
                        TenXa                   = reader["TenXa"] != DBNull.Value ? reader["TenXa"].ToString() : null,
                        TenHuyen                = reader["TenHuyen"] != DBNull.Value ? reader["TenHuyen"].ToString() : null,
                        Password                = "***",
                        LoginTime               = reader["LoginTime"] != DBNull.Value ? (DateTime?)reader["LoginTime"] : null,
                        LogoutTime              = reader["LogoutTime"] != DBNull.Value ? (DateTime?)reader["LogoutTime"] : null,
                        isAdmin                 = false
                    });

                }
            }

            userDataGrid.ItemsSource = users;
        }

        private List<User> SearchUsers(string searchText)
        {
            return users.Where(user => (user.Username?.ToLower().Contains(searchText)       ?? false) ||
                                       (user.CompanyName?.ToLower().Contains(searchText)    ?? false) ||
                                       (user.TenHuyen?.ToLower().Contains(searchText)       ?? false) ||
                                       (user.TenXa?.ToLower().Contains(searchText)          ?? false)).ToList();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                userDataGrid.ItemsSource    = users;
            }
            else
            {
                var filteredUsers           = SearchUsers(searchText);
                userDataGrid.ItemsSource    = filteredUsers;
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query        = "UPDATE Company SET LogoutTime = @logouttime WHERE Username = @username";
                SqlCommand cmd      = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@logouttime", DateTime.Now);
                cmd.Parameters.AddWithValue("@username", AdminUser.Username);
                cmd.ExecuteNonQuery();
            }

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
            popupInfoMenu.IsOpen = false;
            UserInfoWindow userInfoWindow = new UserInfoWindow(AdminUser.Username, this);
            userInfoWindow.Show();
            Hide();
        }
        public void InitializeClock() { 
            timer = new DispatcherTimer(); 
            timer.Interval = TimeSpan.FromSeconds(1); 
            timer.Start(); 
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
            string password             = ""; 
            string connectionString     = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True"; 
            using (SqlConnection conn   = new SqlConnection(connectionString)) 
            { 
                conn.Open(); 
                string query            = "SELECT Password FROM Company WHERE Username = @Username"; 
                SqlCommand cmd          = new SqlCommand(query, conn); 
                cmd.Parameters.AddWithValue("@Username", username); 
                SqlDataReader reader    = cmd.ExecuteReader();

                if (reader.Read()) 
                { 
                    password = reader["Password"].ToString(); 
                } 

            } 
            return password; 
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox       = sender as CheckBox;
            string username         = checkBox?.Tag?.ToString();
            User user               = users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                if (user.Password == "***")
                {
                    user.Password               = GetPasswordFromDatabase(user.Username);
                    userDataGrid.ItemsSource    = null;
                    userDataGrid.ItemsSource    = users;
                }
                else
                {
                user.Password                   = "***";
                userDataGrid.ItemsSource        = null;
                userDataGrid.ItemsSource        = users;
                }
            }
        }
        private void configData_Checked(object sender, RoutedEventArgs e)
        {   
            CheckBox checkbox                   = sender as CheckBox;
            User tempUser                       = new User();
            string username                     = checkbox?.Tag?.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query                    = "SELECT c.CompanyID, c.CompanyName, c.Password, a.LevelName, x.TenXa, h.TenHuyen FROM Company c JOIN AdministratorLevel a ON c.AdministratorLevel = a.LevelID JOIN Xa x ON c.IDXa = x.IDXa JOIN Huyen h ON c.IDHuyen = h.IDHuyen WHERE Username = @username";
                SqlCommand cmd                  = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();

                SqlDataReader reader    = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tempUser.CompanyID              = (int)reader["CompanyID"];
                    tempUser.CompanyName            = reader["CompanyName"].ToString();
                    tempUser.Username               = username;
                    tempUser.Password               = reader["Password"].ToString();
                    tempUser.AdministratorLevel     = reader["LevelName"].ToString();
                    tempUser.TenXa                  = reader["TenXa"].ToString();
                    tempUser.TenHuyen               = reader["TenHuyen"].ToString();
                }
            }
            popupInfoMenu.IsOpen                        = false;
            UserInfoConfigWindow userInfoConfigWindow   = new UserInfoConfigWindow(tempUser, this);
            userInfoConfigWindow.Show();
            Hide();
        }

        private void configData_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox               = sender as CheckBox;
            string username                 = checkBox?.Tag?.ToString();
            User user                       = users.FirstOrDefault(u => u.Username == username);
            using (SqlConnection conn       = new SqlConnection(connectionString))
            {
                conn.Open();
                string query                = "SELECT c.CompanyID, c.CompanyName, c.Password, a.LevelName, x.TenXa, h.TenHuyen FROM Company c JOIN AdministratorLevel a ON c.AdministratorLevel = a.LevelID JOIN Xa x ON c.IDXa = x.IDXa JOIN Huyen h ON c.IDHuyen = h.IDHuyen WHERE Username = @username";
                SqlCommand cmd              = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();

                SqlDataReader reader        = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.CompanyID          = (int)reader["CompanyID"];
                    user.CompanyName        = reader["CompanyName"].ToString();
                    user.Username           = username;
                    user.Password           = "***";
                    user.AdministratorLevel = reader["LevelName"].ToString();
                    user.TenXa              = reader["TenXa"].ToString();
                    user.TenHuyen           = reader["TenHuyen"].ToString();
                }
            }
            userDataGrid.ItemsSource        = null;
            userDataGrid.ItemsSource        = users;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void CongTy_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Company_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
    public class User
    {
        public int CompanyID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string AdministratorLevel { get; set; }
        public string TenXa { get; set; }
        public string TenHuyen { get; set; }
        public DateTime? LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }
        public bool isAdmin { get; set; }
    }
}

