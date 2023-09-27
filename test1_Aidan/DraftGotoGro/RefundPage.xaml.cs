using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DraftGotoGro
{
    public partial class RefundPage : Page
    {

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Setup initial state, for example, configuring columns of DataGrid based on the ComboBox selection
            SetupDataGridColumns();
            PlaceholderTextBlock.Text = "Member ID"; // Default placeholder text
        }

        private void SetupDataGridColumns()
        {
            // Clear existing columns
            SalesDataGrid.Columns.Clear();

            // Add columns to the DataGrid based on the selected search type
            SalesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Order Number", Binding = new Binding("OrderNumber") });
            SalesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Member ID", Binding = new Binding("MemberID") });
            SalesDataGrid.Columns.Add(new DataGridTextColumn { Header = "Item Count", Binding = new Binding("ItemCount") });
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement the logic for searching the sales based on the input and populate the DataGrid.
            string searchText = SearchTextBox.Text.Trim();
            string searchType = ((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString();

            // You need to replace the following line with your actual search and data population logic.
            // SalesDataGrid.ItemsSource = GetData(searchType, searchText);
        }

        private void SalesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalesDataGrid.SelectedItem is Sale selectedSale)
            {
                RefundDetailsWindow refundDetailsWindow = new RefundDetailsWindow(selectedSale);
                refundDetailsWindow.ShowDialog();
            }
        }


        private void SearchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded) // Check whether the page is loaded to avoid exceptions during the initialization.
            {
                SetupDataGridColumns(); // Adjust DataGrid columns if needed, based on search type
                // Clear the previous search results, if any
                SalesDataGrid.ItemsSource = null;
                // Update placeholder text based on the selected search type
                PlaceholderTextBlock.Text = ((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString() == "Member ID" ? "Member ID" : "Order Number";
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderTextBlock.Visibility = string.IsNullOrWhiteSpace(SearchTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        // Assuming you have a method like this to fetch the appropriate data based on the search type and search text.
        // Please replace it with your actual implementation.
        // private List<Sale> GetData(string searchType, string searchText)
        // {
        //     List<Sale> result = new List<Sale>();
        //     // Your actual logic to fetch data goes here.
        //     return result;
        // }
    }
}

