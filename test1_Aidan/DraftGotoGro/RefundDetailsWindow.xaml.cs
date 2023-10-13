﻿using System.Windows;
using MongoDB.Driver;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DraftGotoGro
{
    public partial class RefundDetailsWindow : Window
    {
        private Sale _selectedSale;
        private IMongoCollection<Sale> _saleCollection;

        public RefundDetailsWindow(Sale selectedSale, IMongoCollection<Sale> saleCollection)
        {
            InitializeComponent();
            _selectedSale = selectedSale;
            _saleCollection = saleCollection;
            ItemsListView.ItemsSource = selectedSale.Items;
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
                    // Remove the item from the selectedSale and refresh the ListView
                    _selectedSale.Items.Remove(selectedItem);
                    ItemsListView.Items.Refresh();
                    MessageBox.Show("Item refunded successfully.");
                    this.Close(); //close the window
                }
                else
                {
                    MessageBox.Show("Failed to refund the item.");
                }
            }
        }
    }
}
