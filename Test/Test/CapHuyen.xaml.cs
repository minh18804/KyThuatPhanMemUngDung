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
using static System.Net.Mime.MediaTypeNames;

namespace Test
{
    /// <summary>
    /// Interaction logic for NhanVienWindow.xaml
    /// </summary>
    /// 
    public partial class CapHuyenWindow : Window
    {
        List<CongTy> companies;
        List<string> tenHuyen;
        struct Xa
        {
            public string TenXa { get; set; }
            public string TenHuyenTrucThuoc { get; set; }
        }
        List<Xa> tenXa;
        CanBoNghiepVu user;
        public CapHuyenWindow(CanBoNghiepVu user)
        {
            this.user = user;
            InitializeComponent();
            Greeting.Content = $"Xin chào, {user.Name}";
            companies = Provider.LoadCompaniesData();
            LoadHuyen();
            LoadXa();
            AddColumnsToBangHuyen();
            AddColumnsToBangXa();
        }
        private void AddColumnsToBangHuyen()
        {
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
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT x.TenXa, h.TenHuyen FROM Xa x JOIN Huyen h ON x.TrucThuocHuyen = h.IDHuyen WHERE x.IDXa != 0", cmd => { }, reader =>
            {
                tenXa = new List<Xa>();
                while (reader.Read())
                {
                    tenXa.Add(new Xa
                    {
                        TenXa = reader["TenXa"].ToString(),
                        TenHuyenTrucThuoc = reader["TenHuyen"].ToString()
                    });
                }
            });
            BangXa.ItemsSource = tenXa.Select(x => new { x.TenXa }).ToList();
        }
        private void LoadHuyen()
        {
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT TenHuyen FROM Huyen WHERE IDHuyen != 0", cmd => { }, reader =>
            {
                tenHuyen = new List<string>();
                while (reader.Read())
                {
                    tenHuyen.Add(reader["TenHuyen"].ToString());
                }
            });
            BangHuyen.ItemsSource = tenHuyen;
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
                return;

            SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, "UPDATE CongTy SET LogoutTime = @logouttime WHERE Username = @username", cmd =>
            {
                cmd.Parameters.AddWithValue("@logouttime", DateTime.Now);
                cmd.Parameters.AddWithValue("@username", user.Username);
            });

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            popupInfoMenu.IsOpen = !popupInfoMenu.IsOpen;
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

        private void personInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoWindow userInfoWindow = new UserInfoWindow(user, this);
            userInfoWindow.Show();
            popupInfoMenu.IsOpen = false;
            Hide();
        }
        private void quanLyGiongVatNuoi_Click(object sender, RoutedEventArgs e)
        {
            QuanLyGiongVatNuoiWindow quanLyGiongVatNuoiWindow = new QuanLyGiongVatNuoiWindow(user, this);
            quanLyGiongVatNuoiWindow.Show();
            Hide();
        }

        private void quanLyNguonGen_Click(object sender, RoutedEventArgs e)
        {
            QuanLyNguonGenGiongWindow quanLyNguonGenGiongWindow = new QuanLyNguonGenGiongWindow(user, this);
            quanLyNguonGenGiongWindow.Show();
            Hide();
        }

        private void quanLyThucAnChanNuoi_Click(object sender, RoutedEventArgs e)
        {
            QuanLyGiongVatNuoiWindow quanLyGiongVatNuoiWindow = new QuanLyGiongVatNuoiWindow(user, this);
            quanLyGiongVatNuoiWindow.Show();
            Hide();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ThongKe_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            ThongKeGrid.Visibility = Visibility.Visible;
            QuanLyHanhChinhGrid.Visibility = Visibility.Collapsed;

            LinhVucTraCuuComboBox.Items.Add("GiongVatNuoi");
            LinhVucTraCuuComboBox.Items.Add("NguonGenGiong");
            LinhVucTraCuuComboBox.Items.Add("ThucAnChanNuoi");
            SqlHelper.ExecuteReader(SqlHelper.connectionString, "SELECT TenConVat FROM LoaiConVat", cmd => { }, reader =>
            {
                while (reader.Read())
                {
                    TraCuuLoaiConVat.Items.Add(reader["TenConVat"].ToString());
                }
            });
        }

        private void QuanLyHanhChinh_Click(object sender, RoutedEventArgs e)
        {
            QuanLyHanhChinhGrid.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            ThongKeGrid.Visibility = Visibility.Collapsed;
        }

        private void return_1_Click(object sender, RoutedEventArgs e)
        {
            QuanLyHanhChinhGrid.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Visible;
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
        private void LinhVucTraCuuComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LinhVucTraCuuComboBox.SelectedItem is string selectedLinhVucTraCuu)
            {
                if (selectedLinhVucTraCuu == "GiongVatNuoi")
                {
                    NhiemVuTraCuuComboBox.Items.Add("SanXuat");
                    NhiemVuTraCuuComboBox.Items.Add("SanXuatTinhPhoiAuTrung");
                    NhiemVuTraCuuComboBox.Items.Add("SoHuuTrauBoDuc_Lon");
                    NhiemVuTraCuuComboBox.Items.Add("MuaBan");
                    NhiemVuTraCuuComboBox.Items.Add("KhaoNghiem");
                }
                else if (selectedLinhVucTraCuu == "NguonGenGiong")
                {
                    NhiemVuTraCuuComboBox.Items.Add("ThuThap");
                    NhiemVuTraCuuComboBox.Items.Add("BaoTon");
                    NhiemVuTraCuuComboBox.Items.Add("KhaiThac_PhatTrien");
                }
                else
                {
                    NhiemVuTraCuuComboBox.Items.Add("SanXuat");
                    NhiemVuTraCuuComboBox.Items.Add("SanXuat_CoGiayChungNhan");
                    NhiemVuTraCuuComboBox.Items.Add("MuaBan");
                    NhiemVuTraCuuComboBox.Items.Add("KhaoNghiem");
                }
            }
        }

        private void NhiemVuTraCuuComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NhiemVuTraCuuComboBox.SelectedItem is string selectedNhiemVuTraCuu)
            {
                numberOfCompanyInFieldLabel.Content = $"Số công ty hoạt động trong lĩnh vực {LinhVucTraCuuComboBox.SelectedItem.ToString()} có nhiệm vụ {NhiemVuTraCuuComboBox.SelectedItem.ToString()} là: ";
                string query_1 = "SELECT COUNT(*) FROM CongTy WHERE IDLinhVuc = (SELECT IDLinhVuc FROM LinhVuc WHERE TenLinhVuc = @linhvuc) AND IDHuyen = (SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen) AND";
                string query_2;
                if (LinhVucTraCuuComboBox.SelectedItem.ToString() == "GiongVatNuoi")
                {
                    query_2 = "IDGiongVatNuoi = (SELECT IDGiongVatNuoi FROM GiongVatNuoi WHERE TenGiongVatNuoi = @nhiemvu)";
                }
                else if(LinhVucTraCuuComboBox.SelectedItem.ToString() == "NguonGenGiong")
                {
                    query_2 = "IDNguonGenGiong = (SELECT IDNguonGenGiong FROM NguonGenGiong WHERE TenNguonGenGiong = @nhiemvu)";
                }
                else
                {
                    query_2 = "IDThucAnChanNuoi = (SELECT IDThucAnChanNuoi FROM ThucAnChanNuoi WHERE TenThucAnChanNuoi = @nhiemvu)";
                }

                numberOfCompanyInField.Content = SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, query_1 + query_2, cmd =>
                {
                    cmd.Parameters.AddWithValue("@linhvuc", LinhVucTraCuuComboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@nhiemvu", NhiemVuTraCuuComboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@tenhuyen", user.TenHuyen);
                }).ToString();
            }
        }

        private void returnToMain_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            ThongKeGrid.Visibility = Visibility.Collapsed;
        }

        private void TraCuuLoaiConVat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((TraCuuLoaiConVat.SelectedItem != null) && (NhiemVuTraCuuComboBox.SelectedItem != null))
            {
                LabelSoLuongConGiong.Content = $"Tổng số lượng con giống loại {TraCuuLoaiConVat.SelectedItem.ToString()} đã được {NhiemVuTraCuuComboBox.SelectedItem.ToString()} là: ";
                numberOfBreed.Content = companies.Where(c => (c.TenLoaiConVat == TraCuuLoaiConVat.SelectedItem.ToString()) && (c.LinhVuc == LinhVucTraCuuComboBox.SelectedItem.ToString()) && (LinhVucTraCuuComboBox.SelectedItem.ToString() == "GiongVatNuoi" ? (c.GiongVatNuoi == NhiemVuTraCuuComboBox.SelectedItem.ToString()) : ((LinhVucTraCuuComboBox.SelectedItem.ToString() == "NguonGenGiong") ? (c.NguonGenGiong == NhiemVuTraCuuComboBox.SelectedItem.ToString()) : (c.ThucAnChanNuoi == NhiemVuTraCuuComboBox.SelectedItem.ToString())))).Sum(c => c.SoLuong);
            }
        }
    }
}
