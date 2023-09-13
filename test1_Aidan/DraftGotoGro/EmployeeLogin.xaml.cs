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

        public EmployeeLogin()
        {
            InitializeComponent();
            usernameBox = (TextBox)MainGrid.FindName("UsernameBox");
            passwordBox = (PasswordBox)MainGrid.FindName("PasswordBox");
            submitButton = (Button)MainGrid.FindName("SubmitButton");
            usernameErrorLabel = (Label)MainGrid.FindName("UsernameErrorLabel");
            noPasswordLabel = (Label)MainGrid.FindName("NoPasswordLabel");


            employees.Add(new Employee(1, "AGrimmett", "Aidan", "Grimmett", "Password1234", "103606838@student.swin.edu.au", "1234567890", new DateTime(2002, 07, 21)));
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

            //Check types / sanitise inputs
            string pattern = "^[A-Z]{2}[a-z]{0,18}(?:\\d{2})?$"; //Two capital letters, 18 lowercase letters, optional 2-digit number
            bool isMatch = Regex.IsMatch(usernameBox.Text, pattern);

            if (!isMatch)
            {
                usernameErrorLabel.Content = "Username is not of a valid format. Please try again.";
                usernameErrorLabel.Visibility = Visibility.Visible;
                return;
            }

            //compare inputs with stored data
            foreach (Employee em in employees)
            {
                usernameFound = (usernameBox.Text == em.username);

                if (usernameFound)
                {
                    passwordCorrect = (passwordBox.Password.ToString() == em.password);
                    continue;
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
                SubmitButton.Content = "Login successful";
            }
            else
            {
                SubmitButton.Content = "Login failed";
            }
        }
    }
}
