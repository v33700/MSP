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
            //Check that the item ID and Quantity boxes are pupulated and valid

            //add item ID and Quantity to the listview
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //Check there is a valid member ID 

            //Submit the order to the database
        }
    }
}
