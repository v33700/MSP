using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MemberIDBox.Text == "" || OrderNumber.Text == "" || ItemID.Text == "" || ItemQuantity.Text == "") ;
            {
                SaleErrorLabel.Content = "Please fill all the data.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            string pattern = "^[A-Z]{4}d{2}"; //Four capital letters and 2 digit numbers
            bool isMatch = Regex.IsMatch(MemberIDBox.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Member ID is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{5}"; //
            isMatch = Regex.IsMatch(OrderNumber.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Order number is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{3}"; //
            isMatch = Regex.IsMatch(ItemID.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Item ID is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{2}"; //
            isMatch = Regex.IsMatch(ItemQuantity.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Item Quantity is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
        }
    }

}
