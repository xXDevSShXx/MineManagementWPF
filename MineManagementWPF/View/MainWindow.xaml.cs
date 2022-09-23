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
using MineManagementWPF.ViewModel;
using DotSpatial.Positioning;
using System.IO.Ports;
using MineManagementWPF.Properties;
using MineManagementApi.Models;
using LiteDB;

namespace MineManagementWPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer UITimer;
        private Timer DataSenderTimer;
        private GPS GPS;

        public MainWindow()
        {
            InitializeComponent();

            MainViewModel.Instance = new MainViewModel();

            DataContext = MainViewModel.Instance;

            Binding binding = new Binding("CurrentPage");
            binding.Source = MainViewModel.Instance;

            contentFrame.SetBinding(Frame.ContentProperty, binding);

            UITimer = new Timer(new TimerCallback(UITimer_Tick), null, Timeout.Infinite, Timeout.Infinite);
            DataSenderTimer = new Timer(new TimerCallback(DataSenderTimer_Tick), null, Timeout.Infinite, Timeout.Infinite);

            Devices.AllowBluetoothConnections = false;
            Devices.AllowSerialConnections = true;
            Devices.BeginDetection();
            Devices.DeviceDetectionCompleted += Devices_DeviceDetectionCompleted;

            GPS = new GPS();

            VIdTextBlock.Text = Settings.Default.VehicleID;
        }

        private void Devices_DeviceDetectionCompleted(object sender, EventArgs e)
        {
            SerialDevice gpsDevic;
            if(Devices.SerialDevices.Count == 0)
            {
                MessageBox.Show("No GPS Device Was Found. Please Resolve The Issue And Try Again.");
                return;
            }
            if (string.IsNullOrEmpty(Settings.Default.PortName))
            {
                gpsDevic = (SerialDevice)Devices.GpsDevices[0];
                Settings.Default.PortName = gpsDevic.Port;
                Settings.Default.Save();
            }
            else
            {
                gpsDevic = Devices.SerialDevices.First(device => device.Port == Settings.Default.PortName);
                if(gpsDevic == null)
                {
                    MessageBox.Show("Problem Finding the Configured GPS Device. Please Resolve The Issue And Try Again");
                    return;
                }
            }
            gpsDevic.Open();
            GPS.Start(gpsDevic);
        }

        private async void DataSenderTimer_Tick(object state)
        {
            if (GlobalVM.isLoggedIn)
            {
                var locations = DeQueue();
                locations.Add(new LocationVM
                {
                    UserId = GlobalVM.CurrentUser.userID,
                    VehicleID = Settings.Default.VehicleID,
                    Latitude = GPS.Position.Latitude.DecimalDegrees,
                    Longitude = GPS.Position.Longitude.DecimalDegrees,
                    Speed = GPS.Speed.Value,
                    Direction = GPS.Direction.DecimalDegrees,
                    DeviceTime = DateTime.Now
                });

                foreach (var location in locations)
                {
                    try
                    {
                        var json = JsonConvert.SerializeObject<LocationVM>(location);
                        var response = await HttpRequestSender.POST(Settings.Default.LocationUrl + "Update", new System.Net.Http.StringContent(json, Encoding.UTF8, "text/json"));

                        if (!response.IsSuccessStatusCode)
                        {
                            Queue(location);
                        }
                    }
                    catch
                    {
                        Queue(location);
                        return;
                    }
                }

                
            }
        }

        void Queue(LocationVM entity)
        {
            using (var db = new LiteDatabase($@"{Environment.CurrentDirectory}\Queue.db"))
            {
                var col = db.GetCollection<LocationVM>("Locations");

                col.Insert(entity);
            }
        }

        List<LocationVM> DeQueue()
        {
            using (var db = new LiteDatabase($@"{Environment.CurrentDirectory}\Queue.db"))
            {
                var col = db.GetCollection<LocationVM>("Locations");

                var locations = col.Query()
                    .ToList();
                col.DeleteAll();

                return locations;
            }
        }

        private void UITimer_Tick(object state)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    timeTextBlock.Text = DateTime.Now.ToString("hh:mm:ss tt");
                });
            }
            catch
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UITimer.Change(0, 1000);
            DataSenderTimer.Change(0, Settings.Default.Interval);
            MainViewModel.Instance.CurrentPage = new LoginPage();
        }
    }
}
