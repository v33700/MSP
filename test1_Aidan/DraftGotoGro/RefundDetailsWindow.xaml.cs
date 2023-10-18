using System.Windows;
using System.Windows.Automation;
using MongoDB.Driver;

namespace DraftGotoGro
{
    public partial class RefundDetailsWindow : Window
    {
        private Sale _selectedSale;
        private IMongoCollection<Sale> _saleCollection;
        private RefundPage _page;

        public RefundDetailsWindow(Sale selectedSale, IMongoCollection<Sale> saleCollection, RefundPage parent)
        {
            InitializeComponent();
            _selectedSale = selectedSale;
            _saleCollection = saleCollection;
            ItemsListView.ItemsSource = selectedSale.Items;
            _page = parent;
        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsListView.SelectedItem is Item selectedItem)
            {
                var filter = Builders<Sale>.Filter.Eq("OrderNumber", _selectedSale.OrderNumber);
                var update = Builders<Sale>.Update.PullFilter("Items", Builders<Item>.Filter.Eq("ItemName", selectedItem.ItemName));

                var result = _saleCollection.UpdateOne(filter, update);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    _selectedSale.Items.Remove(selectedItem);
                    _page.SalesDataGrid.Items.Refresh();


                    MessageBox.Show("Item refunded successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to refund the item.");
                }
            }
        }
    }
}
