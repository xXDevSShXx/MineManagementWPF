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
    /// Interaction logic for ManagePage.xaml
    /// </summary>
    public partial class ManagePage : Page
    {
        public ManagePage()
        {
            InitializeComponent();

            ManagePageVM.Instance = new ManagePageVM(new StatusPage());

            DataContext = ManagePageVM.Instance;

            Binding binding = new Binding("CurrentPage");
            binding.Source = ManagePageVM.Instance;

            contentFrame.SetBinding(Frame.ContentProperty, binding);
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            ManagePageVM.Instance.CurrentPage = new StopsPage();
        }
    }
}
