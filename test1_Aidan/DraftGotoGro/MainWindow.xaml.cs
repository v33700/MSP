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

namespace DraftGotoGro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox usernameBox;
        PasswordBox passwordBox;
        Button submitButton;

        Label usernameErrorLabel;
        Label noPasswordLabel;

        List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
            usernameBox = (TextBox)MainWin.FindName("UsernameBox");
            passwordBox = (PasswordBox)MainWin.FindName("PasswordBox");
            submitButton = (Button)MainWin.FindName("SubmitButton");
            usernameErrorLabel = (Label)MainWin.FindName("UsernameErrorLabel");
            noPasswordLabel = (Label)MainWin.FindName("NoPasswordLabel");


            employees.Add(new Employee(1, "AGrimmett", "Aidan", "Grimmett", "Password1234", "103606838@student.swin.edu.au", "1234567890", new DateTime(2002, 07, 21)));
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            bool usernameFound = false;
            bool passwordCorrect = false;

            if (usernameBox.Text == "" || passwordBox.Password.ToString() == "")
                return;

            //Check types / sanitise inputs


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
                usernameErrorLabel.Visibility = Visibility.Visible;
            }
            else if (!passwordCorrect)
            {
                NoPasswordLabel.Visibility = Visibility.Visible;
            }

            if (usernameFound && passwordCorrect)
            {
                //very good, do whatever needs to happen to log in.
                Debug.WriteLine("login good");
            }
            else
            {
                Debug.WriteLine("Login bad");
            }
        }
    }
}
