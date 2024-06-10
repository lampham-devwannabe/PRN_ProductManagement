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
using WPFApp.Model;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly MyStoreContext _context;
        public LoginWindow()
        {
            InitializeComponent();
            _context = new MyStoreContext();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            AccountMember account = _context.AccountMembers.FirstOrDefault(x => x.MemberId == txtUser.Text);
            if (account != null && account.MemberPassword.Equals(txtPass.Password)
                && account.MemberRole == 1)
            {
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("You are not permitted!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
