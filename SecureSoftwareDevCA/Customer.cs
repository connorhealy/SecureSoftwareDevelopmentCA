using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSoftwareDevCA
{
    [Serializable]
    class Customer : User
    {
        public string Address { get; }
        public string IBAN { get; }
        public string LoanRemaining { get; }
        public string Password { get; }
        public new string FirstName { get; }
        public new string ID { get; }
        public new string LastName { get; }
        public new string EmailAddress { get; }
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


    }
}
