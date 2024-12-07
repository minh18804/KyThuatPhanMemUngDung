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
        public UserInfoConfigWindow(User _user, Window _previousWindow)
        {
            //user = _user;
            //previousWindow = _previousWindow;
            InitializeComponent();
            //FirstNameTextBox.Text = user.Firstname;
            //LastNameTextBox.Text = user.Lastname;
            //Roles.SelectedIndex = user.Roles;
            //UsernameTextBox.Text = user.Username; 
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
            //string _firstname = FirstNameTextBox.Text;
            //string _lastname = LastNameTextBox.Text;
            //string _username = UsernameTextBox.Text;
            //string _roles = Roles.SelectedItem.ToString();
            //string _password = PasswordTextBox.Text;
            //EnumRoles temproles = (EnumRoles)Enum.Parse(typeof(EnumRoles), _roles);
            //int roles = (int)temproles;
            //int minimumLength = 8;

            //if(UsernameTextBox.Text.Length < minimumLength || ((NewPasswordTextBox.Text.Length < minimumLength) && (NewPasswordPanel.Visibility == Visibility.Visible)) )
            //{
            //    MessageBox.Show("Username hoặc mật khẩu thay đổi quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //using(SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    if (NewPasswordPanel.Visibility == Visibility.Visible)
            //    {
            //        if(ConfirmNewPasswordTextBox.Text != NewPasswordTextBox.Text)
            //        {
            //            MessageBox.Show("Mật khẩu xác nhận không trùng nhau", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            //            return;
            //        }
            //        string hasPasswordQuery = "UPDATE Users SET Username = @newusername, Password = @password, FirstName = @firstname, Lastname = @lastname, Roles = @roles WHERE Username = @oldusername";
            //        SqlCommand hasPasswordCommand = new SqlCommand(hasPasswordQuery, conn);
            //        hasPasswordCommand.Parameters.AddWithValue("@newusername", _username);
            //        hasPasswordCommand.Parameters.AddWithValue("@password", NewPasswordTextBox.Text);
            //        hasPasswordCommand.Parameters.AddWithValue("@firstname", _firstname);
            //        hasPasswordCommand.Parameters.AddWithValue("@lastname", _lastname);
            //        hasPasswordCommand.Parameters.AddWithValue("@roles", roles);
            //        hasPasswordCommand.Parameters.AddWithValue("@oldusername", user.Username);
            //        hasPasswordCommand.ExecuteNonQuery();

            //        MessageBox.Show("Thay đổi thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }

            //    else if(NewPasswordPanel.Visibility == Visibility.Collapsed)
            //    {
            //        string hasPasswordQuery = "UPDATE Users SET Username = @newusername, FirstName = @firstname, LastName = @lastname, Roles = @roles WHERE Username = @oldusername";
            //        SqlCommand hasPasswordCommand = new SqlCommand(hasPasswordQuery, conn);
            //        hasPasswordCommand.Parameters.AddWithValue("@newusername", UsernameTextBox.Text);
            //        hasPasswordCommand.Parameters.AddWithValue("@firstname", _firstname);
            //        hasPasswordCommand.Parameters.AddWithValue("@lastname", _lastname);
            //        hasPasswordCommand.Parameters.AddWithValue("@roles", roles);
            //        hasPasswordCommand.Parameters.AddWithValue("@oldusername", user.Username);

            //        MessageBox.Show("Thay đổi thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //}
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }

        private void Roles_Loaded(object sender, RoutedEventArgs e)
        {
            Roles.Items.Add("Admin");
            Roles.Items.Add("Cấp xã");
            Roles.Items.Add("Cấp huyện");
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
    }
}
