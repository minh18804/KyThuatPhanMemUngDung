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
        /// Chi tiết ở MainWindow.xaml.cs
        /// </summary>
        private string connectionString = "Data Source=localhost;Initial Catalog=contact;Integrated Security=True";

        /// <summary>
        /// Thêm các lựa chọn cho ComboBox chức vụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Roles_Loaded(object sender, RoutedEventArgs e)
        {
            AdministratorLevel.Items.Add("Cấp huyện");
            AdministratorLevel.Items.Add("Cấp xã");
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
            if (string.IsNullOrWhiteSpace(CompanyName.Text))
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
            string username = Username.Text;
            string password = Password.Password;
            string companyName = CompanyName.Text;
            string string_administratorLevel = AdministratorLevel.SelectedItem.ToString();
            string companyIDString;
            string recoveryCode1;
            string recoveryCode2;
            string recoveryCode3;
            string recoveryCode;
            int administratorLevel;
            int idXa = 0;
            int idHuyen = 0;
            int companyID = 0;
            int soThuTu = 0;

            if (string_administratorLevel == "Cấp huyện")
            {
                administratorLevel = 1;
            }
            else
            {
                administratorLevel = 2;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(*) FROM Company WHERE Username = @username";
                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@username", username);
                int userCount = (int)checkCmd.ExecuteScalar();
                if (userCount > 0)
                {
                    MessageBox.Show("Username đã tồn tại. Vui lòng chọn username khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (administratorLevel == 2)
                {
                    checkQuery          = "SELECT IDXa FROM Xa WHERE TenXa = @tenxa";
                    checkCmd            = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@tenxa", AdministratorName.SelectedItem.ToString());
                    idXa                = Convert.ToInt32(checkCmd.ExecuteScalar());
                }
                else
                {
                    checkQuery          = "SELECT IDHuyen FROM Huyen WHERE TenHuyen = @tenhuyen";
                    checkCmd            = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@tenhuyen", AdministratorName.SelectedItem.ToString());
                    idHuyen             = Convert.ToInt32(checkCmd.ExecuteScalar());
                }

                checkQuery              = "SELECT COUNT(*) FROM Company WHERE isAdmin = 0";
                checkCmd                = new SqlCommand(checkQuery, conn);
                soThuTu                 = Convert.ToInt32(checkCmd.ExecuteScalar());
                string formattedSoThuTu = soThuTu.ToString("D4");
                string formattedIDHuyen = idHuyen.ToString("D2");
                string formattedIDXa    = idXa.ToString("D2");

                companyIDString         = $"{administratorLevel}{formattedIDHuyen}{formattedIDXa}{formattedSoThuTu}";
                companyID               = int.Parse(companyIDString);

                recoveryCode1           = GenerateRecoveryCode(9);
                recoveryCode2           = GenerateRecoveryCode(9);
                recoveryCode3           = GenerateRecoveryCode(9);
                recoveryCode            = $"{recoveryCode1} {recoveryCode2} {recoveryCode3}";
                Clipboard.SetText(recoveryCode);

                string query            = "INSERT INTO Company (Username, Password, CompanyName, AdministratorLevel, IDXa, IDHuyen, CompanyID, RecoveryCode1, RecoveryCode2, RecoveryCode3, isAdmin) VALUES (@username, @password, @companyname, @administratorlevel, @idxa, @idhuyen, @companyid, @recoverycode1, @recoverycode2, @recoverycode3, 0)";
                SqlCommand cmd          = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@companyname", companyName);
                cmd.Parameters.AddWithValue("@administratorlevel", administratorLevel);
                cmd.Parameters.AddWithValue("@idxa", idXa);
                cmd.Parameters.AddWithValue("@idhuyen", idHuyen);
                cmd.Parameters.AddWithValue("@companyid", companyID);
                cmd.Parameters.AddWithValue("@recoverycode1", recoveryCode1);
                cmd.Parameters.AddWithValue("@recoverycode2", recoveryCode2);
                cmd.Parameters.AddWithValue("@recoverycode3", recoveryCode3);
                cmd.ExecuteNonQuery();

                MessageBox.Show($"Đăng ký thành công, mã khôi phục tài khoản khi quên mật khẩu của bạn là: \n{recoveryCode1} \n{recoveryCode2} \n{recoveryCode3} \nĐã được copy", "Thông báo", MessageBoxButton.OK, MessageBoxImage.None);

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
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
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "";
                    if (selectedRole == "Cấp xã")
                    {
                        query = "SELECT TenXa FROM Xa WHERE IDXa != 0";
                    }
                    else
                    {
                        query = "SELECT TenHuyen FROM Huyen WHERE IDHuyen != 0";
                    }
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (selectedRole == "Cấp xã")
                        {
                            AdministratorName.Items.Add(reader["TenXa"].ToString());
                        }
                        else
                        {
                            AdministratorName.Items.Add(reader["TenHuyen"].ToString());
                        }
                    }
                }
            }
        }
        private string GenerateRecoveryCode(int length) 
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; 
            using (var crypto = new RNGCryptoServiceProvider()) 
            { 
                var data = new byte[length]; 
                crypto.GetBytes(data); 
                var result = new StringBuilder(length); 
                foreach (var byteValue in data) 
                { 
                    result.Append(chars[byteValue % chars.Length]); 
                } 
                return result.ToString(); 
            }
        }
    }
}
