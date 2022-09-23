using MineManagementWPF.Model;
using MineManagementWPF.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for StopsPage.xaml
    /// </summary>
    public partial class StopsPage : Page
    {
        List<Stop> Stops;
        public StopsPage()
        {
            InitializeComponent();

            string json = File.ReadAllText("Settings.json");
            var dsjson = JsonConvert.DeserializeObject<SettingJsonModel>(json);

            Stops = dsjson.Stops;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategories();

            Button button = ((Button)stopCategoriesGrid.Children[0]);
            LoadButtons((Stop)button.Tag);
            button.Background = new SolidColorBrush((Color)(ColorConverter.ConvertFromString("#080a26")));
        }

        void LoadCategories()
        {
            int row = 0;
            foreach (var stop in Stops)
            {
                Button button = new Button
                {
                    Content = stop.Title,
                    Tag = stop,
                    Style = (Style)FindResource("RoundLabel1")
                };
                button.Click += (sender, e) =>
                {
                    Button bSender = (Button)sender;
                    LoadButtons((Stop)bSender.Tag);
                    foreach (Button item in stopCategoriesGrid.Children)
                    {
                        item.Background = new SolidColorBrush((Color)(ColorConverter.ConvertFromString("#0f1142")));
                    }
                    bSender.Background = new SolidColorBrush((Color)(ColorConverter.ConvertFromString("#080a26")));
                };

                Grid.SetRow(button, row);

                stopCategoriesGrid.Children.Add(button);

                row++;
            }
        }

        void LoadButtons(Stop category)
        {
            stopsGrid.Children.Clear();
            int row = 0, col = 3;
            foreach (var item in category.Items)
            {

                Button button = new Button
                {
                    Content = item.Title,
                    Tag = item,
                    Style = (Style)FindResource("RoundLabel2")
                };
                button.Click += async (sender, e) =>
                 {
                     var code = ((Item)((Button)sender).Tag).Code;
                     await HttpRequestSender.POST(Settings.Default.StatusUrl, new System.Net.Http.StringContent(code.ToString(), Encoding.UTF8, "string/json"));
                 };
                Grid.SetColumn(button, col);
                Grid.SetRow(button, row);

                stopsGrid.Children.Add(button);

                row++;
                if (row>4)
                {
                    col--;
                    row = 0;
                }
            }
        }
    }
}
