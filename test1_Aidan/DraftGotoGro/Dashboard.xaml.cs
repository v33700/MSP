using System.Windows;
using System.Windows.Controls;

namespace DraftGotoGro
{
    public partial class DashboardPage : Page
    {
        MainWindow myParent;

        public DashboardPage(MainWindow win)
        {
            InitializeComponent();
            myParent = win;
        }

        private void NavigateToMemberPage(object sender, RoutedEventArgs e)
        {
            MemberPage addmember = new MemberPage(myParent);
            myParent.Content = addmember;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeLogin employeeLoginPage = new EmployeeLogin(myParent);
            myParent.Content = employeeLoginPage;
        }

        private void NavigateToSalesInputPage(object sender, RoutedEventArgs e)
        {
            SalesInput salesInput = new SalesInput(myParent);
            myParent.Content = salesInput;
        }
    }
}
