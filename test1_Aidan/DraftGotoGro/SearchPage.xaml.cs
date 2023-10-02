using MongoDB.Driver;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace DraftGotoGro
{
    public partial class SearchPage : Page
    {
        private ObservableCollection<Member> MemberResult = new ObservableCollection<Member>();
        private ObservableCollection<Sale> SaleResult = new ObservableCollection<Sale>();

        private IMongoDatabase _database;
        private IMongoCollection<Member> _memberCollection;
       

        public SearchPage()
        {
            InitializeComponent();

            var client = new MongoClient("mongodb+srv://SWECLASS:IXo4LdFQqKUdJXIr@tomstestcluster.unrd1c2.mongodb.net/"); // MongoDB connection string will add to ppk or pem style key once we know its working
            _database = client.GetDatabase("SWE"); // database name
            _memberCollection = _database.GetCollection<Member>("Members");
          
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
            if (((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString() == "Member Search")
            {
                var members = _memberCollection.Find(_ => true).ToList();
                

                foreach (Member m in members) 
                {
                    if (m.Id.ToString() == SearchTextBox.Text) 
                    {
                        SearchResultsDataGrid.ItemsSource = new ObservableCollection<Member>();
                        (SearchResultsDataGrid.ItemsSource as ObservableCollection<Member>).Add(m);
                        break;
                    }
                }
                ErrorLabel.Visibility = Visibility.Visible;
                

            }
            else if ((((ComboBoxItem)SearchTypeComboBox.SelectedItem).Content.ToString() == "Sale Search"))
            {
                SearchResultsDataGrid.ItemsSource = SaleResult;
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


}
