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
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DraftGotoGro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //create all the pages
        EmployeeLogin employeeLoginPage = new EmployeeLogin();
        SalesInput salesInputPage = new SalesInput();

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = employeeLoginPage; //Change this to a homepage when we make it
        }
    }
}
