using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;

namespace SecureSoftwareDevCA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Loanr");
            Console.WriteLine("Select user to log in as:");
            Console.WriteLine("1 - Admin");
            Console.WriteLine("2 - User");

            switch (Console.ReadLine())
            {
                case "1":
                    AdminLogin();
                    break;
                case "2":
                    UserLogin();
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }

        }

        public static void UserLogin()
        {
            Console.WriteLine("please enter email");
            string email = Console.ReadLine();

            List<Customer> users = GetCustomers();
            Customer customer = users.FirstOrDefault(user => user.EmailAddress == email);

            Console.WriteLine("please enter password");
            string password = Console.ReadLine();

            if (password == customer.Password)
            {
                Console.WriteLine($"Hi {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Your loan balance is {customer.LoanRemaining}");
            }
            else
            {
                Console.WriteLine("incorrect password");
            }

        }
        public static void AdminLogin()
        {

            string menuSelection = "";
            bool loggedIn = false;
            string adminPassword = "password";
            while (menuSelection != "exit")
            {
                Console.WriteLine("Please enter admin password or type \"exit\" to quit");
                string password = Console.ReadLine();

                if (password == adminPassword)
                {
                    loggedIn = true;
                    menuSelection = "exit";
                    Console.Clear();
                }

                if (password == "exit")
                {
                    menuSelection = "exit";
                }


            }

            if (loggedIn == true)
            {
                List<Customer> users = GetCustomers();




                string customerMenuSelection = "";
                while (customerMenuSelection != "exit")
                {
                    Console.Clear();
                    users.ForEach(bank_account =>
                    {
                        Console.WriteLine("|{0,20}|{1,20}|{2,20}|", bank_account.ID, bank_account.FirstName, bank_account.LastName);
                    });

                    Console.WriteLine("\n Select an option:");
                    Console.WriteLine("1 - View a customers information");
                    Console.WriteLine("2 - Update a users loan amount");
                    Console.WriteLine("3 - Delete a user's loan");
                    Console.WriteLine("4 - Add a new users loan");
                    Console.WriteLine("Or type \"exit\" to exit");
                    customerMenuSelection = Console.ReadLine();


                    switch (customerMenuSelection)
                    {
                        case "1":
                            ViewCustomerInformation(users);
                            break;
                        case "2":
                            UpdateUsersLoanAmount(users);
                            break;
                        case "3":
                            DeleteUsersLoan(users);
                            break;
                        case "4":
                            AddNewUsersLoan(users);
                            break;
                        case "exit":
                            Console.WriteLine("Logging you out..");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Invalid selection");
                            break;
                    }
                }
            }






        }



        public static List<Customer> GetCustomers()
        {
            List<Customer> users = new List<Customer>();

            using (var reader = new StreamReader(@"bank_accounts.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    Customer tempCustomer = new Customer(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);
                    users.Add(tempCustomer);
                }
            }

            return users;
        }

        public static void ViewCustomerInformation(List<Customer> users)
        {
            Console.WriteLine("Select customer to view information of:");
            string selectedAccount = Console.ReadLine();

            Customer customer = users.FirstOrDefault(user => user.ID == selectedAccount);
            Console.WriteLine(" \n  Customer ID: {0} \n Customer Name: {1} {2} \n  Customer Address: {3} \n  Customer Loan Remaining: {4} \n", customer.ID, customer.FirstName, customer.LastName, customer.Address, customer.LoanRemaining);
            Console.ReadKey();
        }

        private static void UpdateUsersLoanAmount(List<Customer> users)
        {

            Console.WriteLine("Select customer to update loan amount of:");
            string selectedAccount = Console.ReadLine();

            Customer customer = users.FirstOrDefault(user => user.ID == selectedAccount);
            Console.WriteLine($"Current loan amount is {customer.LoanRemaining}, please enter new loan amount");
            string newLoanAmount = Console.ReadLine();
            customer.LoanRemaining = newLoanAmount;

            Console.WriteLine($"Current loan amount is {customer.LoanRemaining}");

            using (var writer = new StreamWriter("bank_accounts.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(users);
            }

            Console.ReadKey();
        }

        private static void DeleteUsersLoan(List<Customer> users)
        {
            Console.WriteLine("Select customer whose loan youd like to delete:");
            string selectedAccount = Console.ReadLine();

            Customer customer = users.FirstOrDefault(user => user.ID == selectedAccount);
            users.Remove(customer);

            using (var writer = new StreamWriter("bank_accounts.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(users);
            }

            Console.ReadKey();
        }

        public static void AddNewUsersLoan(List<Customer> users)
        {
            Console.WriteLine("Please enter users information");

            Console.WriteLine("Please enter first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please enter last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please enter IBAN:");
            string IBAN = Console.ReadLine();

            Console.WriteLine("Please enter address:");
            string address = Console.ReadLine();

            Console.WriteLine("Please enter email:");
            string email = Console.ReadLine();

            Console.WriteLine("Please enter loan balance:");
            string loanAmount = Console.ReadLine();

            Console.WriteLine("Please enter password:");
            string password = Console.ReadLine();

            string id = (Convert.ToInt32(users.LastOrDefault().ID) + 1).ToString();
            Console.WriteLine($"id: {id}");

            Customer newCustomer = new Customer(address, IBAN, loanAmount, password, id, firstName, lastName, email);

            users.Add(newCustomer);

            using (var writer = new StreamWriter("bank_accounts.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(users);
            }

            Console.ReadKey();
        }
    }

}
