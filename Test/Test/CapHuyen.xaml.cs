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

namespace Test
{
    /// <summary>
    /// Interaction logic for NhanVienWindow.xaml
    /// </summary>
    public partial class CapHuyenWindow : Window
    {
        User user;
        public CapHuyenWindow(User user)
        {
            this.user = user;
            InitializeComponent();
            Greeting.Content = $"Xin chào, {user.CompanyName}";
        }
    }
}
