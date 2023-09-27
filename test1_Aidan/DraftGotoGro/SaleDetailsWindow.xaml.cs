using DraftGotoGro;
using System.Windows;

namespace DraftGotoGro
{
    public partial class SaleDetailsWindow : Window
    {
        public SaleDetailsWindow(Sale sale)
        {
            InitializeComponent();

            // Set the Title and TextBlocks with Sale Details
            Title = $"Order Number: {sale.OrderNumber}";
            OrderNumberTextBlock.Text = $"Order Number: {sale.OrderNumber}";
            MemberIdTextBlock.Text = $"Member ID: {sale.MemberID}";

            // Set ItemsListView's ItemsSource to the Items of the Sale.
            ItemsListView.ItemsSource = sale.Items;
        }
    }
}
