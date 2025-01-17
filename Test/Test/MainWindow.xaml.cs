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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
namespace Test
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Hàm xử lý sự kiện login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_Click(object sender, RoutedEventArgs e)
        {
            CanBoNghiepVu user = new CanBoNghiepVu();
            user.IsAdmin = false;
            user.Username = Username.Text;
            user.Password = Password.Password;
            bool? isAdmin = admin.IsChecked;
            bool? isCapXa = capXa.IsChecked;
            bool? isCapHuyen = capHuyen.IsChecked;

            if (isAdmin == true) user.IsAdmin = true;
            else if (isCapXa == true) user.CapTrucThuoc = "Xa";
            else if (isCapHuyen == true) user.CapTrucThuoc = "Huyen";

            if (String.IsNullOrEmpty(user.Username) || String.IsNullOrEmpty(user.Password))
            {
                MessageBox.Show("Không được để trống Username hoặc Password", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Provider.ValidateUser(user))
            {
                Provider.SetLoginTime(user);
                user = Provider.LoadUserData(user);
                if (user.IsAdmin == true)
                {
                    AdminWindow adminWindow = new AdminWindow(user);
                    adminWindow.Show();
                    Close();
                }
                else if (user.CapTrucThuoc == "Huyen")
                {
                    CapHuyenWindow capHuyenWindow = new CapHuyenWindow(user);
                    capHuyenWindow.Show();
                    Close();
                }  
                else
                {
                    CapXaWindow capXaWindow = new CapXaWindow(user);
                    capXaWindow.Show();
                    Close();
                }
            }

            else MessageBox.Show("Username hoặc password không đúng", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        /// <summary>
        /// Hàm xử lý sự kiện register: chuyển sang màn hình register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            Hide();
        }
        /// <summary>
        /// Hàm xử lý sự kiện bấm Enter là tự động đăng nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                login_Click(sender, e);
            }
        }
        /// <summary>
        /// Hàm xử lý sự kiện forgotPassword: chuyển sang màn hình quên mật khẩu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void forgotPassword_Click(object sender, RoutedEventArgs e)
        {
            ForgotPasswordWindow ForgotPasswordWindow = new ForgotPasswordWindow(this);
            ForgotPasswordWindow.Show();
            Hide();
        }
    }
}
