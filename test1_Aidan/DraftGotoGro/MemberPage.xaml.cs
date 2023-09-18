using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;

namespace DraftGotoGro
{
    public partial class MemberPage : Page
    {
        public MemberPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Sample Data Loading
            LoadSampleData();
        }

        private void LoadSampleData()
        {
            var sampleData = new ObservableCollection<Member>
            {
                new Member { Id = 1, Name = "John Doe", PhoneNumber = "123-456-7890", Address = "123 Main St" },
                new Member { Id = 2, Name = "Jane Smith", PhoneNumber = "987-654-3210", Address = "456 Elm St" }
            };

            MemberDataGrid.ItemsSource = sampleData;
        }

        private void NavigateToDashboard(object sender, RoutedEventArgs e)
        {
            // Navigate to the DashboardPage
            NavigationService.Navigate(new DashboardPage());
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Your logic for adding a new member goes here.
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parent = button.Parent as StackPanel;

            var updateButton = parent.Children[0] as Button;
            var removeButton = parent.Children[1] as Button;
            var cancelButton = parent.Children[2] as Button;
            var saveButton = parent.Children[3] as Button;

            updateButton.Visibility = Visibility.Collapsed;
            removeButton.Visibility = Visibility.Collapsed;
            cancelButton.Visibility = Visibility.Visible;
            saveButton.Visibility = Visibility.Visible;

            // Update Button Logic
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parent = button.Parent as StackPanel;

            var updateButton = parent.Children[0] as Button;
            var removeButton = parent.Children[1] as Button;
            var cancelButton = parent.Children[2] as Button;
            var saveButton = parent.Children[3] as Button;

            updateButton.Visibility = Visibility.Visible;
            removeButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;

            // Cancel button logic
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var parent = button.Parent as StackPanel;

            var updateButton = parent.Children[0] as Button;
            var removeButton = parent.Children[1] as Button;
            var cancelButton = parent.Children[2] as Button;
            var saveButton = parent.Children[3] as Button;

            var member = button.Tag as Member; // This is your member object

            // Save logic here

            updateButton.Visibility = Visibility.Visible;
            removeButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;
        }

        // Continue with other methods like RemoveButton_Click

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Remove logic here
        }
    }

    public class Member // This is your member model class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
