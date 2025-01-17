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
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace Test
{
    public partial class AdminWindow : Window
    {
        private List<CanBoNghiepVu> users;   
        private List<CongTy> companies;
        public CanBoNghiepVu AdminUser;
        private DispatcherTimer timer = new DispatcherTimer();
        bool isPasswordVisible = false;
        public AdminWindow(CanBoNghiepVu adminUser)
        {
            InitializeComponent();
            ClockProvider.InitializeClock(ref timer);
            Greeting.Content            = $"Xin chào, Admin";
            InfoButton.Content          = " ";
            AdminUser = adminUser;
            DataGridConfig(false);
            userDataGrid.ItemsSource    = Provider.LoadUsersData();
            numberOfUser.Content        = Provider.NumberOfUsersOrCompany("CanBoNghiepVu");
            users                       = Provider.LoadUsersData();
            companies                   = Provider.LoadCompaniesData();
        }
        private void DataGridConfig(bool isCongTy)
        {
            if (userDataGrid != null)
            {
                var keepColumns = userDataGrid.Columns[0];
                userDataGrid.Columns.Clear();
                userDataGrid.Columns.Add(keepColumns);
                userDataGrid.ItemsSource = null;

                if (isCongTy)
                {
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "ID Công Ty",
                        Binding = new Binding("ID")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên công ty",
                        Binding = new Binding("Name")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên đăng nhập",
                        Binding = new Binding("Username")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Mật khẩu",
                        Binding = new Binding("Password")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "SDT công ty",
                        Binding = new Binding("SDT")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Cấp trực thuộc",
                        Binding = new Binding("CapTrucThuoc")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên xã",
                        Binding = new Binding("TenXa")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên huyện",
                        Binding = new Binding("TenHuyen")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Nhiệm vụ",
                        Binding = Giong_Vat_Nuoi_RadioButton.IsChecked == true ? new Binding("GiongVatNuoi") : Nguon_Gen_Giong_RadioButton.IsChecked == true ? new Binding("NguonGenGiong") : new Binding("ThucAnChanNuoi")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Loại con vật",
                        Binding = new Binding("TenLoaiConVat")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Số lượng",
                        Binding = new Binding("SoLuong")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Đã bị dừng hoạt động",
                        Binding = new Binding("Banned")
                    });
                }
                else
                {
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "ID Cán bộ nghiệp vụ",
                        Binding = new Binding("ID")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên cán bộ nghiệp vụ",
                        Binding = new Binding("Name")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên đăng nhập",
                        Binding = new Binding("Username")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Mật khẩu",
                        Binding = new Binding("Password")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "SDT cán bộ nghiệp vụ",
                        Binding = new Binding("SDT")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Cấp trực thuộc",
                        Binding = new Binding("CapTrucThuoc")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên xã",
                        Binding = new Binding("TenXa")
                    });
                    userDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "Tên huyện",
                        Binding = new Binding("TenHuyen")
                    });
                    //ID = (int)reader["CanBoNghiepVuID"],
                    //        Name = reader["CanBoNghiepVuName"]?.ToString(),
                    //        Username = reader["Username"]?.ToString(),
                    //        Password = "***",
                    //        CapTrucThuoc = reader["LevelName"].ToString(),
                    //        SDT = reader["SDT"].ToString(),
                    //        TenXa = reader["TenXa"] != DBNull.Value ? reader["TenXa"].ToString() : null,
                    //        TenHuyen = reader["TenHuyen"] != DBNull.Value ? reader["TenHuyen"].ToString() : null,
                    //        LoginTime = reader["LoginTime"] != DBNull.Value ? (DateTime?)reader["LoginTime"] : null,
                    //        LogoutTime = reader["LogoutTime"] != DBNull.Value ? (DateTime?)reader["LogoutTime"] : null,
                    //        IsAdmin = false
                }
            }
        }
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            if(userDataGrid.ItemsSource != null)
            {
                userDataGrid.ItemsSource = null;
                userDataGrid.Items.Clear();
                if (CanBoNghiepVu.IsChecked == true)
                {
                    userDataGrid.ItemsSource = Provider.SearchUsers(searchText, Provider.LoadUsersData());
                }
                else
                {
                    userDataGrid.ItemsSource = Provider.SearchCompany(searchText, Provider.LoadCompaniesData());
                }
            }
                if (CanBoNghiepVu.IsChecked == true)
                {
                    userDataGrid.ItemsSource = Provider.SearchUsers(searchText, Provider.LoadUsersData());
                }
                else
                {
                    userDataGrid.ItemsSource = Provider.SearchCompany(searchText, Provider.LoadCompaniesData());
                }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;

            Provider.SetLogoutTime(AdminUser);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupMenu.IsOpen = !popupMenu.IsOpen;
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
            UserInfoWindow userInfoWindow = new UserInfoWindow(AdminUser, this);
            userInfoWindow.Show();
            Hide();
        }
        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(this);
            changePasswordWindow.Show();
            Hide();
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            if (CanBoNghiepVu.IsChecked == false)
            {
                CheckBox checkBox = sender as CheckBox;
                string username = checkBox?.Tag?.ToString();
                CongTy company = companies.FirstOrDefault(c => c.Username == username);

                if (company != null)
                {
                    if (company.Password == "***")
                    {
                        company.Password = Provider.GetPasswordFromDatabase(username, "CongTy");
                        userDataGrid.ItemsSource = null;
                        userDataGrid.ItemsSource = companies;
                    }
                    else
                    {
                        company.Password = "***";
                        userDataGrid.ItemsSource = null;
                        userDataGrid.ItemsSource = companies;
                    }
                }
            }
            else
            {
                CheckBox checkBox = sender as CheckBox;
                string username = checkBox?.Tag?.ToString();
                CanBoNghiepVu user = users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    if (user.Password == "***")
                    {
                        user.Password = Provider.GetPasswordFromDatabase(username, "CanBoNghiepVu");
                        userDataGrid.ItemsSource = null;
                        userDataGrid.ItemsSource = users;
                    }
                    else
                    {
                        user.Password = "***";
                        userDataGrid.ItemsSource = null;
                        userDataGrid.ItemsSource = users;
                    }
                }
            }
        }
        private void CongTy_Checked(object sender, RoutedEventArgs e)
        {
            if (userDataGrid.ItemsSource != null)
            {
                DataGridConfig(true);
                userDataGrid.ItemsSource = null;

                companies = new List<CongTy>();
                companies = Provider.LoadCompaniesData();
                if (Giong_Vat_Nuoi_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.GiongVatNuoi != "KhongThuocLinhVuc").ToList();
                else if (Nguon_Gen_Giong_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.NguonGenGiong != "KhongThuocLinhVuc").ToList();
                else
                    companies = companies.Where(c => c.ThucAnChanNuoi != "KhongThuocLinhVuc").ToList();
                userDataGrid.ItemsSource = companies;
                numberOfUser.Content = companies.Count;
                LinhVucHoatDongStackPanel.Visibility = Visibility.Visible;
            }
        }
        private void CanBoNghiepVu_Checked(object sender, RoutedEventArgs e)
        {
            DataGridConfig(false);
            if (userDataGrid != null)
            {
                userDataGrid.ItemsSource = null;
                userDataGrid.Items.Clear();

                users = new List<CanBoNghiepVu>();
                users = Provider.LoadUsersData();
                userDataGrid.ItemsSource = users;
                numberOfUser.Content = users.Count;
                LinhVucHoatDongStackPanel.Visibility = Visibility.Collapsed;
            }
        }
        private void Giong_Vat_Nuoi_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DataGridConfig(true);
            if (userDataGrid != null)
            {
                userDataGrid.ItemsSource = null;
                userDataGrid.Items.Clear();

                companies = new List<CongTy>();
                companies = Provider.LoadCompaniesData();
                if (Giong_Vat_Nuoi_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.GiongVatNuoi != "KhongThuocLinhVuc").ToList();
                else if (Nguon_Gen_Giong_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.NguonGenGiong != "KhongThuocLinhVuc").ToList();
                else
                    companies = companies.Where(c => c.ThucAnChanNuoi != "KhongThuocLinhVuc").ToList();
                userDataGrid.ItemsSource = companies;
                numberOfUser.Content = companies.Count;
            }
        }

        private void Nguon_Gen_Giong_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DataGridConfig(true);
            if (userDataGrid != null)
            {
                userDataGrid.ItemsSource = null;
                userDataGrid.Items.Clear();

                companies = new List<CongTy>();
                companies = Provider.LoadCompaniesData();
                if (Giong_Vat_Nuoi_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.GiongVatNuoi != "KhongThuocLinhVuc").ToList();
                else if (Nguon_Gen_Giong_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.NguonGenGiong != "KhongThuocLinhVuc").ToList();
                else
                    companies = companies.Where(c => c.ThucAnChanNuoi != "KhongThuocLinhVuc").ToList();
                userDataGrid.ItemsSource = companies;
                numberOfUser.Content = companies.Count;
            }
        }

        private void Thuc_An_Chan_Nuoi_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DataGridConfig(true);
            if (userDataGrid != null)
            {
                userDataGrid.ItemsSource = null;
                userDataGrid.Items.Clear();

                companies = new List<CongTy>();
                companies = Provider.LoadCompaniesData();
                if (Giong_Vat_Nuoi_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.GiongVatNuoi != "KhongThuocLinhVuc").ToList();
                else if (Nguon_Gen_Giong_RadioButton.IsChecked == true)
                    companies = companies.Where(c => c.NguonGenGiong != "KhongThuocLinhVuc").ToList();
                else
                    companies = companies.Where(c => c.ThucAnChanNuoi != "KhongThuocLinhVuc").ToList();
                userDataGrid.ItemsSource = companies;
                numberOfUser.Content = companies.Count;
            }
        }

        private void userDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Kiểm tra xem sự kiện double-click có xảy ra trên CheckBox hay không
            if (e.OriginalSource is FrameworkElement element && element.Parent is DataGridCell cell && cell.Content is CheckBox)
            {
                // Nếu sự kiện double-click xảy ra trên CheckBox, không làm gì cả
                return;
            }

            if (CanBoNghiepVu.IsChecked == true)
            {
                var selectedUser = userDataGrid.SelectedItem as CanBoNghiepVu;
                if (selectedUser != null)
                {
                    selectedUser.Password = Provider.GetPasswordFromDatabase(selectedUser.Username, "CanBoNghiepVu");

                    Main.Visibility = Visibility.Collapsed;
                    ConfigUserData.Visibility = Visibility.Visible;

                    NameTextBox.Text = selectedUser.Name;
                    SDTTextBox.Text = selectedUser.SDT;
                    adminLevel.SelectedValue = selectedUser.CapTrucThuoc;
                    UsernameTextBox.Text = selectedUser.Username;
                    PasswordTextBox.Password = selectedUser.Password;
                    if (selectedUser.CapTrucThuoc == "Xa")
                        tenXaHuyen.SelectedValue = selectedUser.TenXa;
                    else tenXaHuyen.SelectedValue = selectedUser.TenHuyen;
                }
                else
                {
                    MessageBox.Show("Không thể lấy thông tin người dùng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                var selectedCompany = userDataGrid.SelectedItem as CongTy;
                if (selectedCompany != null)
                {
                    selectedCompany.Password = Provider.GetPasswordFromDatabase(selectedCompany.Username, "CongTy");

                    Main.Visibility = Visibility.Collapsed;
                    ConfigUserData.Visibility = Visibility.Visible;

                    NameTextBox.Text = selectedCompany.Name;
                    SDTTextBox.Text = selectedCompany.SDT;
                    adminLevel.SelectedValue = selectedCompany.CapTrucThuoc;
                    UsernameTextBox.Text = selectedCompany.Username;
                    PasswordTextBox.Password = selectedCompany.Password;

                    if (selectedCompany.CapTrucThuoc == "Xa")
                        tenXaHuyen.SelectedValue = selectedCompany.TenXa;
                    else tenXaHuyen.SelectedValue = selectedCompany.TenHuyen;
                }
                else
                {
                    MessageBox.Show("Không thể lấy thông tin công ty.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void AdministratorManageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void adminLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string query;
            if (adminLevel.SelectedItem != null)
            {
                string selectedRole = adminLevel.SelectedItem.ToString();
                tenXaHuyen.Items.Clear();
                if (selectedRole == "Xa")
                {
                    query = "SELECT TenXa, TrucThuocHuyen FROM Xa WHERE IDXa != 0";
                }
                else
                {
                    query = "SELECT TenHuyen FROM Huyen WHERE IDHuyen != 0";
                }

                SqlHelper.ExecuteReader(SqlHelper.connectionString, query, cmd => { },
                    reader =>
                    {
                        while (reader.Read())
                        {
                            if (selectedRole == "Xa")
                            {
                                tenXaHuyen.Items.Add(reader["TenXa"].ToString());
                            }
                            else
                            {
                                tenXaHuyen.Items.Add(reader["TenHuyen"].ToString());
                            }
                        }
                    });
            }
        }
        private void adminLevel_Loaded(object sender, RoutedEventArgs e)
        {
            adminLevel.Items.Add("Huyen");
            adminLevel.Items.Add("Xa");
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            string query;
            if (CanBoNghiepVu.IsChecked == true)
            {
                if (NewPasswordPanel.Visibility == Visibility.Visible)
                {
                    if (ConfirmNewPasswordTextBox.Text != NewPasswordTextBox.Text)
                    {
                        MessageBox.Show("Mật khẩu xác nhận không trùng nhau", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else if (NewPasswordTextBox.Text.Length < 8)
                    {
                        MessageBox.Show("Mật khẩu thay đổi quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (adminLevel.SelectedItem.ToString() == "Huyen")
                        query = "UPDATE CanBoNghiepVu SET CanBoNghiepVuName = @cbnvname, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), Password = @newpassword, IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                    else
                        query = "UPDATE CanBoNghiepVu SET CanBoNghiepVuName = @cbnvname, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), Password = @newpassword, IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query,
                        cmd =>
                        {
                            cmd.Parameters.AddWithValue("@cbnvname", NameTextBox.Text);
                            cmd.Parameters.AddWithValue("@newpassword", NewPasswordTextBox.Text);
                            cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                            if (adminLevel.SelectedItem.ToString() == "Huyen")
                            {
                                cmd.Parameters.AddWithValue("@tenhuyen", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@tenxa", DBNull.Value);
                                cmd.Parameters.AddWithValue("@levelname", "Huyen");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@tenxa", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@levelname", "Xa");
                            }
                        });
                    MessageBox.Show("Thay đổi thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {

                    if (adminLevel.SelectedItem.ToString() == "Huyen")
                        query = "UPDATE CanBoNghiepVu SET CanBoNghiepVuName = @cbnvname, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                    else
                        query = "UPDATE CanBoNghiepVu SET CanBoNghiepVuName = @cbnvname, AdministratorLevel = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query,
                        cmd =>
                        {
                            cmd.Parameters.AddWithValue("@cbnvname", NameTextBox.Text);
                            cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                            if (adminLevel.SelectedItem.ToString() == "Huyen")
                            {
                                cmd.Parameters.AddWithValue("@tenhuyen", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@tenxa", DBNull.Value);
                                cmd.Parameters.AddWithValue("@levelname", "Huyen");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@tenxa", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@levelname", "Xa");
                            }
                        });
                }
                MessageBox.Show("Thay đổi thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                DataGridConfig(false);
            }
            else
            {
                if (NewPasswordPanel.Visibility == Visibility.Visible)
                {
                    if (ConfirmNewPasswordTextBox.Text != NewPasswordTextBox.Text)
                    {
                        MessageBox.Show("Mật khẩu xác nhận không trùng nhau", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else if (NewPasswordTextBox.Text.Length < 8)
                    {
                        MessageBox.Show("Mật khẩu thay đổi quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (adminLevel.SelectedItem.ToString() == "Huyen")
                        query = "UPDATE CongTy SET Ten = @companyname, CapTrucThuoc = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), Password = @newpassword, IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                    else
                        query = "UPDATE CongTy SET Ten = @companyname, CapTrucThuoc = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), Password = @newpassword, IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query,
                        cmd =>
                        {
                            cmd.Parameters.AddWithValue("@companyname", NameTextBox.Text);
                            cmd.Parameters.AddWithValue("@newpassword", NewPasswordTextBox.Text);
                            cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                            if (adminLevel.SelectedItem.ToString() == "Huyen")
                            {
                                cmd.Parameters.AddWithValue("@tenhuyen", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@tenxa", DBNull.Value);
                                cmd.Parameters.AddWithValue("@levelname", "Huyen");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@tenxa", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@levelname", "Xa");
                            }
                        });
                }
                else
                {

                    if (adminLevel.SelectedItem.ToString() == "Huyen")
                        query = "UPDATE CongTy SET Ten = @companyname, CapTrucThuoc = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";
                    else
                        query = "UPDATE CongTy SET Ten = @companyname, CapTrucThuoc = (SELECT LevelID FROM AdministratorLevel WHERE LevelName = @levelname), IDHuyen = (SELECT TrucThuocHuyen FROM Xa WHERE TenXa = @tenxa), IDXa = (SELECT IDXa FROM Xa WHERE TenXa = @tenxa) WHERE Username = @username";

                    SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query,
                        cmd =>
                        {
                            cmd.Parameters.AddWithValue("@companyname", NameTextBox.Text);
                            cmd.Parameters.AddWithValue("@username", UsernameTextBox.Text);
                            if (adminLevel.SelectedItem.ToString() == "Huyen")
                            {
                                cmd.Parameters.AddWithValue("@tenhuyen", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@tenxa", DBNull.Value);
                                cmd.Parameters.AddWithValue("@levelname", "Huyen");
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@tenxa", tenXaHuyen.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@levelname", "Xa");
                            }
                        });
                    MessageBox.Show("Thay đổi thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataGridConfig(true);
                }
            }
        }
            private void return_Click(object sender, RoutedEventArgs e)
            {
                Main.Visibility = Visibility.Visible;
                ConfigUserData.Visibility = Visibility.Collapsed;
            }
            private void showHide_Click(object sender, RoutedEventArgs e)
            {
                if (isPasswordVisible)
                {
                    isPasswordVisible = false;
                    showHide.Content = "Hiện";
                    PasswordTextBox.Visibility = Visibility.Visible;
                    showedPassword.Visibility = Visibility.Collapsed;
                    PasswordTextBox.Password = showedPassword.Text;
            }
            else
            {
                isPasswordVisible = true;
                showHide.Content = "Ẩn";
                PasswordTextBox.Visibility = Visibility.Collapsed;
                showedPassword.Visibility = Visibility.Visible;
                showedPassword.Text = PasswordTextBox.Password;
            }
        }

        private void changePasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isCollapsed = NewPasswordPanel.Visibility == Visibility.Collapsed;
            NewPasswordPanel.Visibility = isCollapsed ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

