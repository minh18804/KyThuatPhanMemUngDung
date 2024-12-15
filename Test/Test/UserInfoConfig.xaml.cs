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
using static Test.RegisterWindow;

namespace Test
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UserInfoConfigWindow : Window
    {
        string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";
        User user;
        Window previousWindow;
        bool _isPasswordVisible = false;
        public UserInfoConfigWindow(User user, Window previousWindow)
        {
            this.user = user;
            this.previousWindow = previousWindow;
            InitializeComponent();
            CompanyNameTextBox.Text = user.CompanyName;


            UsernameTextBox.Text = user.Username;
        }

        private void changePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isCollapsed = NewPasswordPanel.Visibility == Visibility.Collapsed;
            NewPasswordPanel.Visibility = isCollapsed ? Visibility.Visible : Visibility.Collapsed;
        }
        private void showHide_Click(object sender, RoutedEventArgs e)
        {
            if (_isPasswordVisible)
            {
                PasswordTextBox.Text = "***";
                showHide.Content = "Hiện";
                _isPasswordVisible = false;
            }
            else
            {
                PasswordTextBox.Text = user.Password;
                showHide.Content = "Ẩn";
                _isPasswordVisible = true;
            }
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            string _username = UsernameTextBox.Text;
            string _companyName = CompanyNameTextBox.Text;
            string _adminLevel = adminLevel.SelectedItem.ToString();
            string _tenXaHuyen = tenXaHuyen.SelectedItem.ToString();
            string _newpassword = NewPasswordTextBox.Text;
            int minimumLength = 8;

            if ((NewPasswordTextBox.Text.Length < minimumLength) && (NewPasswordPanel.Visibility == Visibility.Visible))
            {
                MessageBox.Show("Mật khẩu thay đổi quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                if (NewPasswordPanel.Visibility == Visibility.Visible)
                {
                    if (ConfirmNewPasswordTextBox.Text != NewPasswordTextBox.Text)
                    {
                        MessageBox.Show("Mật khẩu xác nhận không trùng nhau", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string hasPasswordQuery = "UPDATE Company SET CompanyName = @companyname, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), Password = @newpassword, IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = 0 WHERE Username = @username";
                    SqlCommand hasPasswordCommand = new SqlCommand(hasPasswordQuery, conn);
                    hasPasswordCommand.Parameters.AddWithValue("@username", _username);
                    hasPasswordCommand.Parameters.AddWithValue("@newpassword", _newpassword);
                    hasPasswordCommand.Parameters.AddWithValue("@companyname", _companyName);
                    if (_adminLevel == "Huyện")
                    {
                        hasPasswordCommand.Parameters.AddWithValue("@levelname", "Huyen");
                        hasPasswordCommand.Parameters.AddWithValue("@tenhuyen", _tenXaHuyen);
                        hasPasswordCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        hasPasswordCommand.Parameters.AddWithValue("@levelname", "Xa");
                        hasPasswordCommand.Parameters.AddWithValue("@tenxa", _tenXaHuyen);
                        hasPasswordCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thay đổi thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else if (NewPasswordPanel.Visibility == Visibility.Collapsed)
                {
                    string hasPasswordQuery = "UPDATE Company SET CompanyName = @companyname, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = 0 WHERE Username = @username";
                    SqlCommand hasPasswordCommand = new SqlCommand(hasPasswordQuery, conn);
                    hasPasswordCommand.Parameters.AddWithValue("@username", _username);
                    hasPasswordCommand.Parameters.AddWithValue("@companyname", _companyName);
                    if (_adminLevel == "Huyện")
                    {
                        hasPasswordCommand.Parameters.AddWithValue("@levelname", "Huyen");
                        hasPasswordCommand.Parameters.AddWithValue("@tenhuyen", _tenXaHuyen);
                        hasPasswordCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        hasPasswordCommand.Parameters.AddWithValue("@levelname", "Xa");
                        hasPasswordCommand.Parameters.AddWithValue("@tenxa", _tenXaHuyen);
                        hasPasswordCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Thay đổi thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        // sai chỉnh sửa tên xã huyện
        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }
        private void QLNS_Click(object sender, RoutedEventArgs e)
        {

        }

        private void test1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void test2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {

        }
        private void adminLevel_Loaded(object sender, RoutedEventArgs e)
        {
            adminLevel.Items.Add("Xã");
            adminLevel.Items.Add("Huyện");
            if (user.AdministratorLevel == "Huyen")
            {
                adminLevel.SelectedIndex = 1;
            }
            else adminLevel.SelectedIndex = 0;
        }

        private void adminLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedAdminLevel = adminLevel.SelectedItem as string;
            tenXaHuyen.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "";
                if (selectedAdminLevel == "Xã")
                {
                    query = "SELECT TenXa FROM Xa WHERE IDXa != 0";
                }
                else
                {
                    query = "SELECT TenHuyen FROM Huyen WHERE IDHuyen != 0";
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (selectedAdminLevel == "Xã")
                {
                    while (reader.Read())
                    {
                        tenXaHuyen.Items.Add(reader["TenXa"].ToString());
                    }
                }
                else
                {
                    while (reader.Read())
                    {
                        tenXaHuyen.Items.Add(reader["TenHuyen"].ToString());
                    }
                }
            }
        }
        private void tenXaHuyen_Loaded(object sender, RoutedEventArgs e)
        {
            if (user.AdministratorLevel == "Xa")
                tenXaHuyen.SelectedItem = user.TenXa;
            else
                tenXaHuyen.SelectedItem = user.TenHuyen;
        }
    }
}
