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
            // Logic Here
            //NavigationService.Navigate(new MemberPage(myParent));
            MemberPage addmember = new MemberPage(myParent);
            myParent.Content = addmember;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic Here
            EmployeeLogin employeeLoginPage = new EmployeeLogin(myParent);
            myParent.Content = employeeLoginPage;
        }
    }
}
