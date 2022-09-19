using MineManagementWPF.ViewModel;
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

namespace MineManagementWPF.View.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (((string)userNameLabel.Content) == "کد پرسنلی را وارد کنید")
            {
                userNameLabel.Content = string.Empty;
            }
            userNameLabel.Content = (string)userNameLabel.Content + (string)button.Content;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string)userNameLabel.Content == "000000")
            {
                MainViewModel.Instance.CurrentPage = new ConfigPage();
            }
            userNameLabelBorder.BorderBrush = new SolidColorBrush(Colors.Snow);
            bool condition = true;
            if (condition)
            {
                MainViewModel.Instance.CurrentPage = new ManagePage();
            }
            else
            {
                userNameLabelBorder.BorderBrush = new SolidColorBrush(Colors.Red);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            userNameLabel.Content = "کد پرسنلی را وارد کنید";
        }

    }
}
