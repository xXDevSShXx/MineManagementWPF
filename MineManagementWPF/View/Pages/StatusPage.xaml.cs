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

        bool working;
        private void ToggleButtons_Checked(object sender, RoutedEventArgs e)
        {
            working = true;
            ToggleButton toggleButton = sender as ToggleButton;
            togglesButtonsGrid.Children.OfType<ToggleButton>().Where(i => !i.Equals(toggleButton)).ToList().ForEach(toggle =>
              {
                  toggle.IsChecked = false;
              });
            toggleButton.IsChecked = true;
            working = false;
        }

        private void ToggleButtons_UnChecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            if (!working)
            {
                toggleButton.IsChecked = true;
            }
        }
    }
}
