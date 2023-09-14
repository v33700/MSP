using System.Windows;
using System.Windows.Controls;

namespace DraftGotoGro
{
    public partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        private void NavigateToMemberPage(object sender, RoutedEventArgs e)
        {
            // Logic Here
            NavigationService.Navigate(new MemberPage());
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Logic Here
        }
    }
}
