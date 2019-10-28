using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSoftwareDevCA
{
    class Customer : User
    {

        public string Address { get; set; }
        public string IBAN { get; set; }
        public string LoanRemaining { get; set; }
        public string Password { get; set; }

        public Customer(string Address, string IBAN, string LoanRemaining, string Password, string ID, string FirstName, string LastName, string EmailAddress)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.EmailAddress = EmailAddress;
            this.Address = Address;
            this.IBAN = IBAN;
            this.LoanRemaining = LoanRemaining;
            this.Password = Password;
        }
        public Customer()
        {

        }

    }
}
