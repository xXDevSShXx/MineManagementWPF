using MineManagementWPF.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for StatusPage.xaml
    /// </summary>
    public partial class StatusPage : Page
    {
        public StatusPage()
        {
            InitializeComponent();

            motevafeghStatusButton.IsChecked = true;
        }
        private async void StatusButtons_Checked(object sender, RoutedEventArgs e)
        {
            var code = ((string)((RadioButton)sender).Tag);
            await HttpRequestSender.POST(Settings.Default.StatusUrl, new System.Net.Http.StringContent(code.ToString(), Encoding.UTF8, "string/json"));
        }
    }
}
