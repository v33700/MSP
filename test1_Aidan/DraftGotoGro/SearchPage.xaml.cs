using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DraftGotoGro
{
    public partial class SearchPage : Page
    {
        private ObservableCollection<Member> MemberData = new ObservableCollection<Member>();
        private ObservableCollection<Sale> SaleData = new ObservableCollection<Sale>();

        public SearchPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetupDataGridColumns();
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = Visibility.Collapsed;
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTextBox.Text))
            {
                PlaceholderTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void SearchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetupDataGridColumns();
            var selectedSearchType = (ComboBoxItem)SearchTypeComboBox.SelectedItem;
            PlaceholderTextBlock.Text = selectedSearchType.Content.ToString() == "Member Search" ? "Member ID" : "Order Number";
        }

        private void SetupDataGridColumns()
        {
            SearchResultsDataGrid.Columns.Clear();

            var selectedSearchType = (ComboBoxItem)SearchTypeComboBox.SelectedItem;
            if (selectedSearchType.Content.ToString() == "Member Search")
            {
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Member ID", Binding = new Binding("Id") });
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Name", Binding = new Binding("Name") });
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Phone Number", Binding = new Binding("PhoneNumber") });
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Address", Binding = new Binding("Address") });
            }
            else
            {
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Order Number", Binding = new Binding("OrderNumber") });
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Member ID", Binding = new Binding("MemberId") });
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Item Count", Binding = new Binding("ItemCount") });
                SearchResultsDataGrid.Columns.Add(new DataGridTextColumn { Header = "Date", Binding = new Binding("Date") });
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // This is where you implement the search logic.
            // Update the SearchResultsDataGrid's ItemsSource based on search results.
            if (((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString() == "Member Search")
            {
                // Suppose MemberData is your ObservableCollection of Members.
                // Implement your logic to fill MemberData based on SearchTextBox's text.
                SearchResultsDataGrid.ItemsSource = MemberData;
            }
            else
            {
                // Suppose SaleData is your ObservableCollection of Sales.
                // Implement your logic to fill SaleData based on SearchTextBox's text.
                SearchResultsDataGrid.ItemsSource = SaleData;
            }
        }

        private void SearchResultsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString() == "Sale Search")
            {
                var selectedSale = (Sale)SearchResultsDataGrid.SelectedItem;
                if (selectedSale != null)
                {
                    var saleDetailsWindow = new SaleDetailsWindow(selectedSale);
                    saleDetailsWindow.Show();
                }
            }
        }
    }

    // Temporary classes to serve as an example, you would need to replace them with your actual model classes.
    // public class Member_temp
    //{
    //   public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public string Address { get; set; }
    //}

    //public class Sale
    //{
    //    public int SaleId { get; set; }
    //    public int MemberId { get; set; }
    //    public int ItemCount { get; set; }
    //}

    // Temporary Window class to serve as an example, you would need to create and implement it.
}
