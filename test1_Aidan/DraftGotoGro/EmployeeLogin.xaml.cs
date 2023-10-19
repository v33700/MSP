using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using MongoDB.Driver;
using MongoDB.Bson;


namespace DraftGotoGro
{
    /// <summary>
    /// Interaction logic for EmployeeLogin.xaml
    /// </summary>
    public partial class EmployeeLogin : Page
    {
        TextBox usernameBox;
        PasswordBox passwordBox;
        Button submitButton;

        Label usernameErrorLabel;
        Label noPasswordLabel;

        List<Employee> employees = new List<Employee>();

        MainWindow myParent;

        private IMongoDatabase _database;
        private IMongoCollection<Employee> _collectionEmployee;

        public EmployeeLogin(MainWindow win)
        {
            InitializeComponent();
            usernameBox = (TextBox)MainGrid.FindName("UsernameBox");
            passwordBox = (PasswordBox)MainGrid.FindName("PasswordBox");
            submitButton = (Button)MainGrid.FindName("SubmitButton");
            usernameErrorLabel = (Label)MainGrid.FindName("UsernameErrorLabel");
            noPasswordLabel = (Label)MainGrid.FindName("NoPasswordLabel");

            var client = new MongoClient("mongodb+srv://SWECLASS:IXo4LdFQqKUdJXIr@tomstestcluster.unrd1c2.mongodb.net/"); // MongoDB connection string will add to ppk or pem style key once we know its working
            _database = client.GetDatabase("SWE"); // database name
            _collectionEmployee = _database.GetCollection<Employee>("Employees"); // db collection reference

            var filter = Builders<Employee>.Filter.Empty; // retrieve all documents in the collection
            var projection = Builders<Employee>.Projection.Exclude("_id"); // exclude the _id field
            employees = _collectionEmployee.Find(filter).Project<Employee>(projection).ToList();

            myParent = win; 
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            bool usernameFound = false;
            bool passwordCorrect = false;

            if (usernameBox.Text == "" || passwordBox.Password.ToString() == "")
            {
                usernameErrorLabel.Content = "Please enter a username and a password.";
                usernameErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                usernameErrorLabel.Visibility = Visibility.Hidden;
            }

            //Check types / sanitise inputs
            string pattern = "^[A-Z]{2}[a-z]{0,18}(?:\\d{2})?$"; //Two capital letters, 18 lowercase letters, optional 2-digit number
            bool isMatch = Regex.IsMatch(usernameBox.Text, pattern);

            if (!isMatch)
            {
                usernameErrorLabel.Content = "Username is not of a valid format. Please try again.";
                usernameErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                usernameErrorLabel.Visibility = Visibility.Hidden;
            }

            //compare inputs with stored data
            foreach (Employee em in employees)
            {
                usernameFound = (usernameBox.Text == em.username);

                if (usernameFound)
                {
                    passwordCorrect = (passwordBox.Password.ToString() == em.password);
                    break;
                }
            }

            //missing or incorrect data
            if (!usernameFound)
            {
                usernameErrorLabel.Content = "No matching username found.";
                usernameErrorLabel.Visibility = Visibility.Visible;
            }
            else if (!passwordCorrect)
            {
                NoPasswordLabel.Visibility = Visibility.Visible;
            }

            if (usernameFound && passwordCorrect)
            {
                //very good, do whatever needs to happen to log in.
                myParent.PageContent.Content = new HomePage();
                myParent.ShowNavigation();
            }
            else
            {
                SubmitButton.Content = "Login failed";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //myParent.PageContent.Content = new HomePage(); 
            //myParent.ShowNavigation();
        }
    }
}
