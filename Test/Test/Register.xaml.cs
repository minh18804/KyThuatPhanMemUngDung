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
using System.Data;
using System.Security.Cryptography;

namespace Test
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Thêm các lựa chọn cho ComboBox chức vụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Roles_Loaded(object sender, RoutedEventArgs e)
        {
            AdministratorLevel.Items.Add("Huyen");
            AdministratorLevel.Items.Add("Xa");
        }

        /// <summary>
        /// Hàm xử lý sự kiện register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Username.Text) || String.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Không được để trống username hoặc password", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(CanBoNghiepVuName.Text))
            {
                MessageBox.Show("Không được để trống tên công ty", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (AdministratorLevel.SelectedItem == null || AdministratorName.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị hành chính", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Username.Text.Length < 8 || Password.Password.Length < 8)
            {
                MessageBox.Show("Username hoặc password quá ngắn", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, "SELECT COUNT(*) FROM CanBoNghiepVu WHERE Username = @username",
                cmd => cmd.Parameters.AddWithValue("@username", Username.Text)) > 0)
            {
                MessageBox.Show("Username đã tồn tại. Vui lòng chọn username khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CanBoNghiepVu tempCanBoNghiepVu = new CanBoNghiepVu();
            tempCanBoNghiepVu.Username = Username.Text;
            tempCanBoNghiepVu.Password = Password.Password;
            tempCanBoNghiepVu.Name = CanBoNghiepVuName.Text;
            tempCanBoNghiepVu.CapTrucThuoc = AdministratorLevel.SelectedItem.ToString();

            if(tempCanBoNghiepVu.CapTrucThuoc == "Huyen")
            {
                tempCanBoNghiepVu.TenHuyen = AdministratorName.SelectedItem.ToString();
                tempCanBoNghiepVu.TenXa = "Khong thuoc xa";
            }
            else
            {
                tempCanBoNghiepVu.TenXa = AdministratorName.SelectedItem.ToString();
                tempCanBoNghiepVu.TenHuyen = "Khong thuoc huyen";
            }

            tempCanBoNghiepVu.SDT = SDT.Text;

            string recoveryCode1 = Provider.GenerateRecoveryCode(9);
            string recoveryCode2 = Provider.GenerateRecoveryCode(9);
            string recoveryCode3 = Provider.GenerateRecoveryCode(9);
            string recoveryCode  = $"{recoveryCode1} {recoveryCode2} {recoveryCode3}";

            int idXa = 0;
            int idHuyen = 0;


            if (Provider.ValidateUser(tempCanBoNghiepVu))
            {
                MessageBox.Show("Username đã tồn tại. Vui lòng chọn username khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (tempCanBoNghiepVu.CapTrucThuoc == "Xa")
            {
                idXa = SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, "SELECT IDXa FROM Xa WHERE TenXa = @tenxa",
                    cmd => cmd.Parameters.AddWithValue("@tenxa", AdministratorName.SelectedItem.ToString()));
            }
            else
            {
                idHuyen = SqlHelper.ExecuteScalar<int>(SqlHelper.connectionString, "SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen",
                    cmd => cmd.Parameters.AddWithValue("@tenHuyen", AdministratorName.SelectedItem.ToString()));
            }
            tempCanBoNghiepVu.ID = Provider.GenerateID(tempCanBoNghiepVu, idHuyen, idXa);
            Clipboard.SetText(recoveryCode);

            if (Provider.SetUserData(tempCanBoNghiepVu, recoveryCode1, recoveryCode2, recoveryCode3))
            {
                MessageBox.Show("Đăng ký thành công, mã khôi phục tài khoản khi quên mật khẩu của bạn \n đã được copy", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);
            }
            else
            {
                MessageBox.Show("Lỗi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
        }

        /// <summary>
        /// Hàm xử lý sự kiện back
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow     = new LoginWindow();
            loginWindow.Show();
            Close();
        }
        private void Roles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AdministratorLevel.SelectedItem != null)
            {
                string selectedRole = AdministratorLevel.SelectedItem.ToString();
                AdministratorName.Items.Clear();

                string query;
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
                                AdministratorName.Items.Add(reader["TenXa"].ToString());
                            }
                            else
                            {
                                AdministratorName.Items.Add(reader["TenHuyen"].ToString());
                            }
                        }
                    });
            }
        }
    }
}
