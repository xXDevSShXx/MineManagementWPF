using MineManagementWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public ConfigPage()
        {
            InitializeComponent();
        }

        private void saveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.VehicleID = vehicleIDTextBox.Text;
            Properties.Settings.Default.Interval = int.Parse(intervalTextBox.Text);
            Properties.Settings.Default.PortName = portNameComboBox.SelectedItem.ToString();
            Properties.Settings.Default.Save();

            MainWindow newMainWindow = new MainWindow();
            MainWindow oldMainWindow = (MainWindow)App.Current.Windows[0];
            App.Current.MainWindow = newMainWindow;
            newMainWindow.Show();
            oldMainWindow.Close();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vehicleIDTextBox.FontSize = vehicleIDTextBox.ActualHeight / 2;
            intervalTextBox.FontSize = intervalTextBox.ActualHeight / 2;
            saveChangesButton.FontSize = saveChangesButton.ActualHeight / 8;
            vehicleIDTextBox.Text = Properties.Settings.Default.VehicleID;
            intervalTextBox.Text = Properties.Settings.Default.Interval.ToString();
            portNameComboBox.ItemsSource = SerialPort.GetPortNames();
            portNameComboBox.SelectedIndex = portNameComboBox.Items.IndexOf(Properties.Settings.Default.PortName);
        }
    }
}
