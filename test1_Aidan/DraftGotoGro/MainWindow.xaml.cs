
using System.Windows;

namespace DraftGotoGro
{ 
    public partial class MainWindow : Window
    {
        HomePage home1 = new HomePage();
        SalesInput sale1 = new SalesInput();
        MemberPage member1 = new MemberPage();
        Report report1 = new Report();
        RefundPage refund1 = new RefundPage();
        SearchPage search1 = new SearchPage();

        public MainWindow()
        {
            InitializeComponent();
            EmployeeLogin employeeLoginPage = new EmployeeLogin(this);
            PageContent.Content = employeeLoginPage;
            HideNavigation();
        }

        public void HideNavigation() 
        {
            HomeBtn.Visibility = Visibility.Hidden;
            MemberBtn.Visibility = Visibility.Hidden;
            SaleBtn.Visibility = Visibility.Hidden;
            SearchBtn.Visibility = Visibility.Hidden;
            ReportBtn.Visibility = Visibility.Hidden;
            RefundBtn.Visibility = Visibility.Hidden;
            LogoutBtn.Visibility = Visibility.Hidden;
        }

        public void ShowNavigation()
        {
            HomeBtn.Visibility = Visibility.Visible;
            MemberBtn.Visibility = Visibility.Visible;
            SaleBtn.Visibility = Visibility.Visible;
            SearchBtn.Visibility = Visibility.Visible;
            ReportBtn.Visibility = Visibility.Visible;
            RefundBtn.Visibility = Visibility.Visible;
            LogoutBtn.Visibility = Visibility.Visible;
        }

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = home1;
        }

        private void MemberBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = member1;
        }

        private void SaleBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = sale1;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = search1;
        }

        private void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = report1;
        }

        private void RefundBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = refund1;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = new EmployeeLogin(this);
            HideNavigation();
        }
    }
}
