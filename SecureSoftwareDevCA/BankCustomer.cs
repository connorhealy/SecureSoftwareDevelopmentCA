using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureSoftwareDevCA
{
    class BankCustomer : User
    {

        public string Address { get; set; }
        public string IBAN { get; set; }
        public string AccountBalance { get; set; }
        public string Password { get; set; }

        public BankCustomer(string ID, string FirstName, string LastName, string EmailAddress, string Address, string IBAN, string AccountBalance, string Password)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.EmailAddress = EmailAddress;
            this.Address = Address;
            this.IBAN = IBAN;
            this.AccountBalance = AccountBalance;
            this.Password = Password;
        }
        public BankCustomer()
        {

        }

    }
}
