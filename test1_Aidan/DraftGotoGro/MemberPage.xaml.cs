using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Diagnostics;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace DraftGotoGro
{
    public partial class MemberPage : Page
    {
        private IMongoDatabase _database;
        private IMongoCollection<Member> _collection;

        public MemberPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new MongoClient("mongodb+srv://SWECLASS:IXo4LdFQqKUdJXIr@tomstestcluster.unrd1c2.mongodb.net/"); // MongoDB connection string will add to ppk or pem style key once we know its working
            _database = client.GetDatabase("SWE"); // database name
            _collection = _database.GetCollection<Member>("Members");
            
            LoadMembersFromDb();
        }

        private void LoadMembersFromDb()
        {
            var members = _collection.Find(_ => true).ToList(); // Retrieve all members from the database
            MemberDataGrid.ItemsSource = new ObservableCollection<Member>(members);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //get collection and set ID to whatever the highest current ID + 1 is
            var membersCollection = MemberDataGrid.ItemsSource as ObservableCollection<Member>;
            if (membersCollection != null)
            {
                int maxId = membersCollection.Max(member => member.Id);
                membersCollection.Add(new Member() { Id = maxId + 1 });
            }
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

            updateButton.Visibility = Visibility.Visible;
            removeButton.Visibility = Visibility.Visible;
            cancelButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;

            if (member != null)
            {
                SaveOrUpdateMember(member);
            }
            else
            {
                MessageBox.Show("No member information provided.");
            }

        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("No member selected!");
                return;
            }

            var selectedMember = MemberDataGrid.SelectedItem as Member;

            if (selectedMember != null)
            {
                var filter = Builders<Member>.Filter.Eq(m => m.Id, selectedMember.Id);
                _collection.DeleteOne(filter);
                (MemberDataGrid.ItemsSource as ObservableCollection<Member>).Remove(selectedMember);
            }
        }

        public void SaveOrUpdateMember(Member member)
        {
            var filter = Builders<Member>.Filter.Eq(m => m.Id, member.Id);

            // Check if a member with the given ID already exists
            var existingMember = _collection.Find(filter).FirstOrDefault();

            if (existingMember != null)
            {
                var update = Builders<Member>.Update
                    .Set(m => m.Name, member.Name)
                    .Set(m => m.PhoneNumber, member.PhoneNumber)
                    .Set(m => m.Address, member.Address);

                _collection.UpdateOne(filter, update);
            }
            else
            {
                _collection.InsertOne(member);
            }
        }

    }
}
