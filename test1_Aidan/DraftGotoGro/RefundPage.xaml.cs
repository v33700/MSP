using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Collections.ObjectModel;

namespace DraftGotoGro
{
    public partial class RefundPage : Page
    {
        private IMongoDatabase _database;
        private IMongoCollection<Sale> _saleCollection;

        public RefundPage()
        {
            InitializeComponent();

            var client = new MongoClient("mongodb+srv://SWECLASS:IXo4LdFQqKUdJXIr@tomstestcluster.unrd1c2.mongodb.net/"); // MongoDB connection string will add to ppk or pem style key once we know its working
            _database = client.GetDatabase("SWE"); // database name
            _saleCollection = _database.GetCollection<Sale>("Sales"); // db collection reference
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetupDataGridColumns();
            PlaceholderTextBlock.Text = "Member ID";
        }

        private void SetupDataGridColumns()
        {
            SalesDataGrid.Columns.Clear();
            SalesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Order Number", Binding = new Binding("OrderNumber") });
            SalesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Member ID", Binding = new Binding("MemberID") });
            SalesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Item Count", Binding = new Binding("ItemCount") });
            SalesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("SaleDate") });
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<Sale> sales = _saleCollection.Find(_ => true).ToList();
            SalesDataGrid.ItemsSource = new ObservableCollection<Sale>();

            foreach (Sale s in sales)
            {
                if (s.OrderNumber.ToString() == SearchTextBox.Text)
                {
                    (SalesDataGrid.ItemsSource as ObservableCollection<Sale>).Add(s);
                    break;
                }
                else if (s.MemberID.ToString() == SearchTextBox.Text)
                {
                    (SalesDataGrid.ItemsSource as ObservableCollection<Sale>).Add(s);
                }
            }

            if (SalesDataGrid.Items.Count == 1)
            {
                MessageBox.Show("No results found.");
            }

        }

        private void SalesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalesDataGrid.SelectedItem is Sale selectedSale)
            {
                RefundDetailsWindow refundDetailsWindow = new RefundDetailsWindow(selectedSale, _saleCollection);
                refundDetailsWindow.ShowDialog();
            }
        }

        private void SearchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                SetupDataGridColumns();
                SalesDataGrid.ItemsSource = null;
                PlaceholderTextBlock.Text = ((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString() == "Member ID" ? "Member ID" : "Order Number";
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(SearchTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
