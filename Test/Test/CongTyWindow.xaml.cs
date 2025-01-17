using System;
using System.Windows;

namespace Test
{
    public partial class CongTyWindow : Window
    {
        public CongTy CongTyUser { get; set; }



        public CongTyWindow(CongTy congTyUser)
        {
            InitializeComponent();
            CongTyUser = congTyUser;
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            string userUsername = CongTyUser.Username;
            string query = "SELECT Ten, Username, Password, SoLuong FROM CongTy WHERE Username = @Username";

            SqlHelper.ExecuteReader(SqlHelper.connectionString, query, cmd =>
            {
                cmd.Parameters.AddWithValue("@Username", userUsername);
            }, reader =>
            {
                if (reader.Read())
                {

                    TenTextBox.Text = reader["Ten"].ToString();
                    UsernameTextBox.Text = reader["Username"].ToString();
                    PasswordTextBox.Text = reader["Password"].ToString();
                    SoLuongTextBox.Text = reader["SoLuong"].ToString();
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu trong bảng CongTy!", "Thông báo");
                }
            });

            SqlHelper.CloseConnection();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (int.TryParse(SoLuongTextBox.Text, out int newSoLuong))
            {
                
                string query = "UPDATE CongTy SET SoLuong = @SoLuong WHERE Username = @Username";

                
                SqlHelper.ExecuteNonQuery(SqlHelper.connectionString, query, cmd =>
                {
                    cmd.Parameters.AddWithValue("@SoLuong", newSoLuong);
                    cmd.Parameters.AddWithValue("@Username", CongTyUser.Username); 
                });

                
                MessageBox.Show("Cập nhật số lượng thành công!", "Thông báo");
            }
            else
            {
                
                MessageBox.Show("Vui lòng nhập một số hợp lệ!", "Lỗi");
            }
        }
    }

}