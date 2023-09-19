using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftGotoGro
{
    public class Employee
    {
        // Properties
        public int employeeID { get; set; }
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public DateTime birthday { get; set; }

        // Constructor
        public Employee(int ID, string Username, string FirstName, string LastName, string Password, string Email, int Phone, DateTime Birthday)
        {
            employeeID = ID;
            username = Username;
            firstName = FirstName;
            lastName = LastName;
            password = Password;
            email = Email;
            phone = Phone;
            birthday = Birthday;
        }
    }
}
