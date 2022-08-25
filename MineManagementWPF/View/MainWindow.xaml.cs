using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MineManagementWPF.View.Pages;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MineManagementWPF.ViewModels;

namespace MineManagementWPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer UITimer;
        public MainWindow()
        {
            InitializeComponent();

            MainViewModel.Instance = new MainViewModel();

            DataContext = MainViewModel.Instance;

            Binding binding = new Binding("CurrentPage");
            binding.Source = MainViewModel.Instance;

            contentFrame.SetBinding(Frame.ContentProperty, binding);

            UITimer = new Timer(new TimerCallback(UITimer_Tick), null, Timeout.Infinite, Timeout.Infinite);
        }

        private void UITimer_Tick(object state)
        {
            Dispatcher.Invoke(() =>
            {
                timeTextBlock.Text = DateTime.Now.ToString("hh:mm:ss tt");
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UITimer.Change(0, 1000);
            MainViewModel.Instance.CurrentPage = new LoginPage();
        }
    }
}
