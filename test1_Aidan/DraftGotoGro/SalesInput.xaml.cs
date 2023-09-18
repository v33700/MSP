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
            if (MemberIDBox.Text == "" || OrderNumber.Text == "" || ItemID.Text == "" || ItemQuantity.Text == "");
            {
                SaleErrorLabel.Content = "Please fill all the data.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
        }

}
