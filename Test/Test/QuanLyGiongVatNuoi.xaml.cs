using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class QuanLyGiongVatNuoiWindow : Window
    {
        CanBoNghiepVu user;
        Window previousWindow;
        List<CongTy> companies;
        List<string> tenHuyen;
        struct Xa
        {
            public string TenXa { get; set; }
            public string TenHuyenTrucThuoc { get; set; }
        }
        List<Xa> tenXa;
        public QuanLyGiongVatNuoiWindow(CanBoNghiepVu user, Window previousWindow)
        {
            this.user = user;
            this.previousWindow = previousWindow;
            InitializeComponent();
            DataGridConfig();
            Greeting.Content = $"Xin chào, {user.Name}";
            if (user.CapTrucThuoc == "Xa") 
                NumberOfCongTyActive.Content = $"Số công ty đang hoạt động trên xã {user.TenXa}: ";
            else
                NumberOfCongTyActive.Content = $"Số công ty đang hoạt động trên {user.TenHuyen}: ";
            LoadCompanyData();
            LoadHuyen();
            LoadXa();
            AddColumnsToBangHuyen();
            AddColumnsToBangXa();
        }
        private void AddColumnsToBangHuyen() { 
            DataGridTextColumn huyenColumn = new DataGridTextColumn 
            { 
                Header = "Các huyện thuộc tỉnh Ninh Bình", 
                Binding = new Binding("."), 
                Width = new DataGridLength(1, DataGridLengthUnitType.Star) 
            }; 
            BangHuyen.Columns.Add(huyenColumn); 
        }
        private void AddColumnsToBangXa() 
        { 
            DataGridTextColumn xaColumn = new DataGridTextColumn 
            { 
                Header = "Các xã", 
                Binding = new Binding("TenXa"), 
                Width = new DataGridLength(1, DataGridLengthUnitType.Star) 
            }; 
            BangXa.Columns.Add(xaColumn); 
        }
        private void LoadXa()
        {
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT x.TenXa, h.TenHuyen FROM Xa x JOIN Huyen h ON x.TrucThuocHuyen = h.IDHuyen ", cmd => { }, reader =>
            {
                tenXa = new List<Xa>();
                while (reader.Read())
                {
                    tenXa.Add(new Xa{
                        TenXa = reader["TenXa"].ToString(),
                        TenHuyenTrucThuoc = reader["TenHuyen"].ToString()
                    });
                }
            });
            BangXa.ItemsSource = tenXa.Select(x => new { x.TenXa }).ToList();
        }
        private void LoadHuyen()
        {
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT TenHuyen FROM Huyen", cmd => { }, reader =>
                {
                    tenHuyen = new List<string>();
                    while (reader.Read())
                    {
                        tenHuyen.Add(reader["TenHuyen"].ToString());
                    }
                });
            BangHuyen.ItemsSource = tenHuyen;
        }
            private void LoadCompanyData()
        {
            companies = Provider.LoadCompaniesData();

            if (user.CapTrucThuoc == "Huyen")
            {
                companies = companies.Where(c => c.TenHuyen == user.TenHuyen).ToList();
                numberOfCompany.Content = companies.Count;
            }
            else
            {
                companies = companies.Where(c => c.TenXa == user.TenXa).ToList();
                numberOfCompany.Content = companies.Count;
            }
            userDataGrid.ItemsSource = companies;
        }
        private void DataGridConfig()
        {
            userDataGrid.Columns.Clear();

            DataGridTemplateColumn checkBoxColumn = new DataGridTemplateColumn
            {
                Header = "Dừng hoạt động công ty"
            };
            checkBoxColumn.CellTemplate = new DataTemplate();
            FrameworkElementFactory checkBoxFactory = new FrameworkElementFactory(typeof(CheckBox));
            checkBoxFactory.SetBinding(CheckBox.TagProperty, new Binding("ID")); // Bind Tag
            checkBoxFactory.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(CheckBox_Checked)); // Add event handler
            checkBoxFactory.SetValue(CheckBox.ClickModeProperty, ClickMode.Press);
            checkBoxColumn.CellTemplate.VisualTree = checkBoxFactory;
            userDataGrid.Columns.Add(checkBoxColumn);

            if (previousWindow is CapHuyenWindow)
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
                    Header = "SDT công ty",
                    Binding = new Binding("SDT")
                });
                userDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Nhiệm vụ",
                    Binding = new Binding("GiongVatNuoi")
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
                    Header = "Trạng thái",
                    Binding = new Binding("TrangThaiConVat")
                });
                userDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Đã bị dừng hoạt động",
                    Binding = new Binding("Banned")
                });
            }
            //else if (previousWindow is CapXaWindow)
            //{

            //}
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            Provider.SetLogoutTime(user);

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
        }

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userInfoWindow = new UserInfoWindow(user.Username, this);
            userInfoWindow.Show();
            popupInfoMenu.IsOpen = false;
            Hide();
        }

        private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            popupMenu.IsOpen = !popupMenu.IsOpen;
        }

        private void changePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(this);
            changePasswordWindow.Show();
            Hide();
        }
        private List<CongTy> SearchUsers(string searchText)
        {
            if (previousWindow is CapHuyenWindow)
            {
                return companies.Where(company => (company.Username?.ToLower().Contains(searchText) ?? false) ||
                                           (company.Name?.ToLower().Contains(searchText) ?? false) ||
                                           (company.TenHuyen?.ToLower().Contains(searchText) ?? false) ||
                                           (company.GiongVatNuoi?.ToLower().Contains(searchText) ?? false)).ToList();
            }
            else
            {
                return companies.Where(company => (company.Username?.ToLower().Contains(searchText) ?? false) ||
                           (company.Name?.ToLower().Contains(searchText) ?? false) ||
                           (company.TenXa?.ToLower().Contains(searchText) ?? false) ||
                           (company.GiongVatNuoi?.ToLower().Contains(searchText) ?? false)).ToList();
            }
        }
        private void search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchbox.Text.ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                userDataGrid.ItemsSource = companies;
            }
            else
            {
                var filteredUsers = SearchUsers(searchText);
                userDataGrid.ItemsSource = filteredUsers;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn dừng hoạt động công ty này không?", "Xác nhận dừng hoạt động", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                return;

            CheckBox checkBox = sender as CheckBox;
            int idCongTy = new int();
            int.TryParse(checkBox?.Tag?.ToString(), out idCongTy);
            
            int hasChanged = SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, "UPDATE CongTy SET Banned = 1 WHERE IDCongTy = @idcongty", cmd => cmd.Parameters.AddWithValue("@idcongty", idCongTy));
            if (hasChanged == 0)
            {
                result = MessageBox.Show("Lỗi");
            }
            else result = MessageBox.Show("Thành công", "Xác nhận dừng hoạt động");

            userDataGrid.Items.Refresh();
            LoadCompanyData();
        }

        private void return_Click(object sender, RoutedEventArgs e)
        {
            previousWindow.Show();
            Close();
        }

        private void sanXuatConGiongRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = "SELECT TenConVat FROM LoaiConVat";
            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while(reader.Read())
            //        GiongVatNuoiTraCuu.Items.Add(reader["TenConVat"].ToString());
            //    reader.Close();
            //    cmd.ExecuteNonQuery();
            //}
            
            //numberOfCompanyInFieldLabel.Content = "Số lượng công ty có nhiệm vụ Sản xuất con giống: ";
            //var companies = userDataGrid.ItemsSource as List<CongTy>;
            //if (companies != null)
            //{
            //    int companiesCount = companies.Count(c => c.GiongVatNuoi == "SanXuat");
            //    numberOfCompanyInField.Content = companiesCount.ToString();
            //}
            //else
            //{
            //    numberOfCompanyInField.Content = "0";
            //}
        }

        private void sanXuatTinhPhoiAuTrungRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = "SELECT TenConVat FROM LoaiConVat";
            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //        GiongVatNuoiTraCuu.Items.Add(reader["TenConVat"].ToString());
            //    reader.Close();
            //    cmd.ExecuteNonQuery();
            //}
            //numberOfCompanyInFieldLabel.Content = "Số lượng công ty có nhiệm vụ Sản xuất tinh phôi ấu trùng: ";
            //var companies = userDataGrid.ItemsSource as List<Company>;
            //if (companies != null)
            //{
            //    int companiesCount = companies.Count(c => c.GiongVatNuoi == "SanXuatTinhPhoiAuTrung");
            //    numberOfCompanyInField.Content = companiesCount.ToString();
            //}
            //else
            //{
            //    numberOfCompanyInField.Content = "0";
            //}
        }

        private void muaBanTinhPhoiAuTrungRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    conn.Open();
            //    string query = "SELECT TenConVat FROM LoaiConVat";
            //    SqlCommand cmd = new SqlCommand(query, conn);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //        GiongVatNuoiTraCuu.Items.Add(reader["TenConVat"].ToString());
            //    reader.Close();
            //    cmd.ExecuteNonQuery();
            //}
            //numberOfCompanyInFieldLabel.Content = "Số lượng công ty có nhiệm vụ Mua bán Tinh phôi ấu trùng: ";
            //var companies = userDataGrid.ItemsSource as List<Company>;
            //if (companies != null)
            //{
            //    int companiesCount = companies.Count(c => c.GiongVatNuoi == "SanXuat");
            //    numberOfCompanyInField.Content = companiesCount.ToString();
            //}
            //else
            //{
            //    numberOfCompanyInField.Content = "0";
            //}
        }

        private void muaBanConGiongRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            numberOfCompanyInFieldLabel.Content = "Số lượng công ty có nhiệm vụ Mua bán con giống: ";
        }

        private void khaoNghiemRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            numberOfCompanyInFieldLabel.Content = "Số lượng công ty có nhiệm vụ Khảo nghiệm: ";
        }

        private void muaBanRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void soHuuTrauBoDucLonRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void statisticButton_Click(object sender, RoutedEventArgs e)
        {
            CompanyTable.Visibility = Visibility.Collapsed;
            AdministratorManagement.Visibility = Visibility.Collapsed;
            Statistic.Visibility = Visibility.Visible;
            popupInfoMenu.IsOpen = false;
        }

        private void returnToMain_Click(object sender, RoutedEventArgs e)
        {
            CompanyTable.Visibility = Visibility.Visible;
            Statistic.Visibility = Visibility.Collapsed;
        }

        private void administratorManageButton_Click(object sender, RoutedEventArgs e)
        {
            CompanyTable.Visibility = Visibility.Collapsed;
            Statistic.Visibility = Visibility.Collapsed;
            AdministratorManagement.Visibility = Visibility.Visible;
        }

        private void BangHuyen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { 
            if (BangHuyen.SelectedItem is string selectedHuyen)
            {
                var column = BangXa.Columns[0] as DataGridTextColumn;
                if (column != null)
                {
                    column.Header = $"Các xã thuộc {selectedHuyen}";
                }
                var filteredXa = tenXa.Where(x => x.TenHuyenTrucThuoc == selectedHuyen).Select(x => new { x.TenXa }).ToList(); 
                BangXa.ItemsSource = filteredXa;
            }
        }

        private void return_1_Click(object sender, RoutedEventArgs e)
        {
            AdministratorManagement.Visibility = Visibility.Collapsed;
            CompanyTable.Visibility = Visibility.Visible;
        }
    }
}
