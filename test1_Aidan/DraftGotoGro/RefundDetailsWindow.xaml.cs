using System.Windows;

namespace DraftGotoGro
{
    public partial class RefundDetailsWindow : Window
    {
        public RefundDetailsWindow(Sale selectedSale)
        {
            InitializeComponent();
            ItemsListView.ItemsSource = selectedSale.Items;
            // Additional setup if necessary
        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to handle refund here.
        }
    }
    
}
