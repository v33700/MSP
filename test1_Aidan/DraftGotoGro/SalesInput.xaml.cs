using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    ///  <summary>
    /// Interaction logic for SalesInput.xaml
    ///  </summary>
    public partial class SalesInput : Page
    {
        public SalesInput()
        {
            InitializeComponent();
        }

        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (memberIDBox.Text == "" || orderNumber.Text == "" || itemID.Text == "" || itemQuantity.Text == "") ;
            {
                saleErrorLabel.Content = "Please fill all the data.";
                saleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            string pattern = "^[A-Z]{4}d{2}"; //Four capital letters and 2 digit numbers
            bool isMatch = Regex.IsMatch(MemberIDBox.Text, pattern);
            if (!isMatch)
            {
                saleErrorLabel.Content = "Member ID is not of a valid format. Please try again.";
                saleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{5}"; //
            isMatch = Regex.IsMatch(orderNumber.Text, pattern);
            if (!isMatch)
            {
                saleErrorLabel.Content = "Order number is not of a valid format. Please try again.";
                saleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{3}"; //
            isMatch = Regex.IsMatch(itemID.Text, pattern);
            if (!isMatch)
            {
                saleErrorLabel.Content = "Item ID is not of a valid format. Please try again.";
                saleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{2}"; //
            isMatch = Regex.IsMatch(itemQuantity.Text, pattern);
            if (!isMatch)
            {
                saleErrorLabel.Content = "Item Quantity is not of a valid format. Please try again.";
                saleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
        }

            //Submit the order to the database
        }
    }
}
