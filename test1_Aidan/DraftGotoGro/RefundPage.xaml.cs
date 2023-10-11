using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using MongoDB.Driver; 

namespace DraftGotoGro
{
    public partial class RefundPage : Page
    {
        private IMongoCollection<Sale> _saleCollection;

        public RefundPage(IMongoCollection<Sale> saleCollection)
        {
            InitializeComponent();
            _saleCollection = saleCollection;
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
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();
            string searchType = ((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString();

            List<Sale> sales = GetData(searchType, searchText);

            if (sales.Count > 0)
            {
                SalesDataGrid.ItemsSource = sales;
            }
            else
            {
                SalesDataGrid.ItemsSource = null;
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

        private List<Sale> GetData(string searchType, string searchText)
        {
            var filter = Builders<Sale>.Filter.Eq(searchType, searchText); 

            List<Sale> sales = _saleCollection.Find(filter).ToList();
            return sales;
        }
    }
}
