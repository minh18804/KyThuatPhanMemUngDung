using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Test
{
    /// <summary>
    /// Interaction logic for UserInfoWindow.xaml
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        /// <summary>
        /// Khởi tạo các biến toàn cục và lưu các dữ liệu từ cửa sổ trước
        /// </summary>
        CanBoNghiepVu user;
        CongTy congty;
        bool isCongTy = new bool();
        private Window previousWindow;
        private bool _isPasswordVisible;
        string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";

        public UserInfoWindow(CongTy _congty, Window _previousWindow)
        {
            InitializeComponent();
            _congty = congty;
            previousWindow = _previousWindow;
            isCongTy = true;
            LoadUserInfo();
        }
        public UserInfoWindow(CanBoNghiepVu _user, Window _previousWindow)
        {
            InitializeComponent();
            user = _user;
            previousWindow = _previousWindow;
            isCongTy = false;
            LoadUserInfo();
        }
        /// <summary>
        /// Lấy dữ liệu người dùng từ SQL
        /// </summary>
        private void LoadUserInfo()
        {
            IDTextBox.Text = user.ID.ToString();
            NameTextBox.Text = user.Name;
            UsernameTextBox.Text = user.Username;
        }

        /// <summary>
        /// Nút đăng xuất
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            if(!isCongTy)
                Provider.SetLogoutTime(user);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
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
                PasswordTextBox.Text = Provider.GetPasswordFromDatabase(user.Username, user is CanBoNghiepVu ? "CanBoNghiepVu" : "CongTy");
                showHide.Content = "Ẩn";
                _isPasswordVisible = true;
            }
        }
        private void return_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }
        private void saveChangesOrMakeChange_Click(object sender, RoutedEventArgs e)
        {
            if (saveChangesOrMakeChange.Content.ToString() == "Thay đổi thông tin")
            {
                NameTextBox.IsEnabled = true;
                PhoneNumberTextBox.IsEnabled = true;    
                adminLevel.IsEnabled = true;
                tenXaHuyen.IsEnabled = true;
                changePasswordBtn.Visibility = Visibility.Visible;
                saveChangesOrMakeChange.Content = "Lưu thay đổi";
            }
            else
            {
                string _name = NameTextBox.Text;
                string _phoneNumber = PhoneNumberTextBox.Text;
                string _adminLevel = adminLevel.SelectedItem.ToString();
                string _tenXaHuyen = tenXaHuyen.SelectedItem.ToString();
                int minimumLength = 8;

                if ((NewPasswordTextBox.Text.Length < minimumLength) && (NewPasswordPanel.Visibility == Visibility.Visible))
                {
                    MessageBox.Show("Mật khẩu thay đổi quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (NewPasswordPanel.Visibility == Visibility.Visible)
                {
                    if (ConfirmNewPasswordTextBox.Text != NewPasswordTextBox.Text)
                    {
                        MessageBox.Show("Mật khẩu xác nhận không trùng nhau", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (!isCongTy)
                    {
                        string query_1 = "UPDATE CanBoNghiepVu SET CanBoNghiepVuName = @ten, SDT = @phonenumber, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), Password = @newpassword, isAdmin = 0";
                        string query_2;
                        if (adminLevel.SelectedItem.ToString() == "Xa")
                        {
                            query_2 = ", IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@newpassword", NewPasswordTextBox.Text);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenxa", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                        else
                        {
                            query_2 = ", IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = 0 WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@newpassword", NewPasswordTextBox.Text);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenhuyen", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                    }
                    else
                    {
                        string query_1 = "UPDATE CongTy SET Ten = @ten, SDT = @phonenumber, CapTrucThuoc = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), isAdmin = 0";
                        string query_2;
                        if (adminLevel.SelectedItem.ToString() == "Xa")
                        {
                            query_2 = ", IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@newpassword", NewPasswordTextBox.Text);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenxa", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                        else
                        {
                            query_2 = ", IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = 0 WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@newpassword", NewPasswordTextBox.Text);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenhuyen", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                    }
                }
                else
                {
                    if (!isCongTy)
                    {
                        string query_1 = "UPDATE CanBoNghiepVu SET CanBoNghiepVuName = @ten, SDT = @phonenumber, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), isAdmin = 0";
                        string query_2;
                        if (adminLevel.SelectedItem.ToString() == "Xa")
                        {
                            query_2 = ", IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenxa", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                        else
                        {
                            query_2 = ", IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = 0 WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenhuyen", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                    }
                    else
                    {
                        string query_1 = "UPDATE CongTy SET Ten = @ten, SDT = @phonenumber, CapTrucThuoc = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), isAdmin = 0";
                        string query_2;
                        if (adminLevel.SelectedItem.ToString() == "Xa")
                        {
                            query_2 = ", IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenxa", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                        else
                        {
                            query_2 = ", IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = 0 WHERE Username = @username";
                            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query_1 + query_2, cmd =>
                            {
                                cmd.Parameters.AddWithValue("@ten", _name);
                                cmd.Parameters.AddWithValue("@phonenumber", _phoneNumber);
                                cmd.Parameters.AddWithValue("@levelname", _adminLevel);
                                cmd.Parameters.AddWithValue("@tenhuyen", _tenXaHuyen);
                                cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                                cmd.ExecuteNonQuery();
                            });
                        }
                    }
                }
                MessageBox.Show("Thay đổi thông tin thành công. Vui lòng đăng nhập lại", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }

        }
        private void adminLevel_Loaded(object sender, RoutedEventArgs e)
        {
            adminLevel.Items.Add("Xa");
            adminLevel.Items.Add("Huyen");
            if (user.CapTrucThuoc == "Huyen")
            {
                adminLevel.SelectedIndex = 1;
            }
            else adminLevel.SelectedIndex = 0;
        }

        private void adminLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedAdminLevel = adminLevel.SelectedItem as string;
            tenXaHuyen.Items.Clear();
            string query = "";
            if (selectedAdminLevel == "Xa")
            {
                query = "SELECT TenXa FROM Xa WHERE IDXa != 0";
            }
            else
            {
                query = "SELECT TenHuyen FROM Huyen WHERE IDHuyen != 0";
            }
            SqlHelper.ExecuteReader(SqlHelper.connectionString, query, cmd => { }, reader =>
            {
                if (selectedAdminLevel == "Xa")
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
            });
        }
        private void tenXaHuyen_Loaded(object sender, RoutedEventArgs e)
        {
            if (user.CapTrucThuoc == "Xa")
                tenXaHuyen.SelectedItem = user.TenXa;
            else
                tenXaHuyen.SelectedItem = user.TenHuyen;
        }
        private void changePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isCollapsed = NewPasswordPanel.Visibility == Visibility.Collapsed;
            NewPasswordPanel.Visibility = isCollapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

