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

namespace DraftGotoGro
{
    /// <summary>
    /// Interaction logic for SalesInput.xaml
    /// </summary>
    public partial class SalesInput : Page
    {
        public SalesInput()
        {
            InitializeComponent();
        }

        private void Page_loaded(object sender, RoutedEventArgs e)
        {
            LoadSampData();
        }

        private void LoadSampData()
        {
            var sampData = new ObservableCollection<SalesData>
            {
            new SalesData { Name = "Pringles", Price = "$5", Quantity = "10" },
            new SalesData { Name = "Milk", Price = "$3", Quantity = "15"}
            }
            SalesDataGrid.ItemSource = sampData;
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DashboardPage());
        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parent = control.Parent as stackpanel;

            var Saveclickbutton = parent.Children[0] as button;
            var Cancelclickbutton = parent.Children[1] as button;
            var removeclickbutton = parent.Children[2] as button;

            Saveclickbutton.Visibility = Visibility.Visible;
            Cancelclickbutton.Visibility = Visibility.Visible;
        }

        private void Cancel_Button(object sender RoutedEventArgs e)
        {
            var button = sender as Button;
            var parent = control.Parent as stackpanel;

            var Saveclickbutton = parent.Children[0] as button;
            var Cancelclickbutton = parent.Children[1] as button;
            var removeclickbutton = parent.Children[2] as button;

            Saveclickbutton.Visibility = Visibility.Visible;
            Cancelclickbutton.Visibility = Visibility.Visible;
        }
        private voide Remove_Button(object sender RoutedEventArgs e)
        {
            var button = sender as Button;
            var parent = control.Parent as stackpanel;

            var Saveclickbutton = parent.Children[0] as button;
            var Cancelclickbutton = parent.Children[1] as button;
            var removeclickbutton = parent.Children[2] as button;

            Saveclickbutton.Visibility = Visibility.Visible;
            Cancelclickbutton.Visibility = Visibility.Visible;
        }
    }

    public class SalesData
    {
        public string Name { get; set; }
        public string Price { get; set; }
        pubic int Quantity { get; set; }
    }
}

