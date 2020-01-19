using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

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

            int selection;
            Int32.TryParse(Console.ReadLine(), out selection);
            switch (selection)
            {
                case 1:
                    AdminLogin();
                    break;
                case 2:
                    UserLogin();
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }



        }



        private static void UserLogin()
        {
            Console.WriteLine("please enter email");
            string email = Console.ReadLine();

            CustomersList users = new CustomersList(GetCustomers());
            Customer customer = users.customers.FirstOrDefault(user => user.EmailAddress == email);

            Console.WriteLine("please enter password");
            string password = Console.ReadLine();

            if (password == customer.Password)
            {
                password = null;
                Console.WriteLine($"Hi {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Your loan balance is {customer.LoanRemaining}");
                customer = null;
                GC.Collect();
            }
            else
            {
                Console.WriteLine("incorrect password");
            }

            password = null;
            customer = null;
            users = null;
            GC.Collect();
        }
        private static void AdminLogin()
        {


            string menuSelection = "";
            bool loggedIn = false;
            string adminPassword = "admin_password123";
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
                adminPassword = null;

            }

            if (loggedIn == true)
            {
                adminPassword = null;

                GC.Collect();

                string customerMenuSelection = "";
                while (customerMenuSelection != "exit")
                {
                    CustomersList customerList = new CustomersList(GetCustomers());

                    byte[] encryptedData = EncryptCustomerList(customerList);
                    customerList = null;
                    customerList = DecryptCustomerList(encryptedData);

                    customerList.customers.ForEach(bank_account =>
                    {
                        Console.WriteLine("|{0,20}|{1,20}|{2,20}|", bank_account.ID, bank_account.FirstName, bank_account.LastName);
                    });

                    customerList = null;
                    GC.Collect();
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
                            ViewCustomerInformation();
                            break;
                        case "2":
                            UpdateUsersLoanAmount();
                            break;
                        case "3":
                            DeleteUsersLoan();
                            break;
                        case "4":
                            AddNewUsersLoan();
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


        private static List<Customer> GetCustomers()
        {
            List<Customer> users = new List<Customer>();

            using (var reader = new StreamReader("loans.csv"))
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

        private static void ViewCustomerInformation()
        {


            Console.WriteLine("Select customer to view information of:");
            string selectedAccount = Console.ReadLine();

            CustomersList customerList = new CustomersList(GetCustomers());

            byte[] encryptedData = EncryptCustomerList(customerList);
            customerList = null;
            customerList = DecryptCustomerList(encryptedData);
            encryptedData = null;
            Customer customer = customerList.customers.FirstOrDefault(user => user.ID == selectedAccount);

            if (customer != null)
            {
                Console.WriteLine(" \n  Customer ID: {0} \n Customer Name: {1} {2} \n  Customer Address: {3} \n  Customer Loan Remaining: {4} \n", customer.ID, customer.FirstName, customer.LastName, customer.Address, customer.LoanRemaining);
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Customer account does not exist");
                Console.ReadKey();
            }

            customerList = null;
            customer = null;
            GC.Collect();

        }

        private static void UpdateUsersLoanAmount()
        {
            Console.WriteLine("Select customer to update loan amount of:");
            string selectedAccount = Console.ReadLine();

            CustomersList customerList = new CustomersList(GetCustomers());

            byte[] encryptedData = EncryptCustomerList(customerList);
            customerList = null;
            customerList = DecryptCustomerList(encryptedData);
            encryptedData = null;

            Customer customer = customerList.customers.FirstOrDefault(user => user.ID == selectedAccount);
            selectedAccount = null;
            if (customer != null)
            {
                Console.WriteLine($"Current loan amount is {customer.LoanRemaining}, please enter new loan amount");
                string newLoanAmount = Console.ReadLine();

                CustomersList customersListClone = customerList.Clone() as CustomersList;
                List<Customer> newCustomers = customersListClone.customers;

                customerList = null;
                Customer updatedCustomer = new Customer(customer.Address, customer.IBAN, newLoanAmount, customer.Password, customer.ID, customer.FirstName, customer.LastName, customer.EmailAddress);
                newLoanAmount = null;
                int indexOfTempCustomer = 0;
                newCustomers.ForEach(tempCustomer =>
                {
                    if (tempCustomer.ID == customer.ID)
                    {
                        indexOfTempCustomer = newCustomers.IndexOf(tempCustomer);
                    }
                    else
                    {

                    }
                });
                customer = null;

                newCustomers[indexOfTempCustomer] = updatedCustomer;
                indexOfTempCustomer = 0;
                Console.WriteLine($"Current loan amount is {updatedCustomer.LoanRemaining}");
                updatedCustomer = null;
                CustomersList updatedCustomers = new CustomersList(newCustomers);
                newCustomers = null;


                updateCSV(updatedCustomers.customers);
                updatedCustomers = null;

                GC.Collect();
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Customer account does not exist");
                Console.ReadKey();
            }

        }

        private static void DeleteUsersLoan()
        {

            CustomersList customerList = new CustomersList(GetCustomers());


            byte[] encryptedData = EncryptCustomerList(customerList);
            customerList = null;
            customerList = DecryptCustomerList(encryptedData);
            encryptedData = null;

            CustomersList customersListClone = customerList.Clone() as CustomersList;
            customerList = null;
            List<Customer> newCustomers = customersListClone.customers;

            Console.WriteLine("Select customer whose loan youd like to delete:");
            string selectedAccount = Console.ReadLine();
            Customer customer = customersListClone.customers.FirstOrDefault(user => user.ID == selectedAccount);
            selectedAccount = null;
            if (customer != null)
            {
                newCustomers.Remove(customer);
                customer = null;
                CustomersList newCustomerList = new CustomersList(newCustomers);
                updateCSV(newCustomerList.customers);
                newCustomers = null;
                customersListClone = null;

                GC.Collect();
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Customer account does not exist");
                Console.ReadKey();
            }

            customer = null;
            newCustomers = null;
            customersListClone = null;
            GC.Collect();

        }

        private static void AddNewUsersLoan()
        {
            CustomersList customerList = new CustomersList(GetCustomers());


            byte[] encryptedData = EncryptCustomerList(customerList);
            customerList = null;
            customerList = DecryptCustomerList(encryptedData);
            encryptedData = null;

            CustomersList customersListClone = customerList.Clone() as CustomersList;
            customerList = null;
            List<Customer> newCustomers = customersListClone.customers;

            Console.WriteLine("Please enter users information");

            string firstName = null;
            bool firstnameRegexMatch = false;
            while (firstnameRegexMatch == false)
            {
                Console.WriteLine("Please enter first name:");
                firstName = removeCharacter(Console.ReadLine(), ",");

                var regex = new Regex(@"[a - zA - Z]{1,25}");
                Match match = regex.Match(firstName);
                if (match.Success)
                {
                    firstnameRegexMatch = true;
                }
                else
                {
                    Console.WriteLine("Invalid first name");
                    firstnameRegexMatch = false;
                }

            }


            string lastName = null;

            bool lastNameRegexMatch = false;
            while (lastNameRegexMatch == false)
            {
                Console.WriteLine("Please enter last name:");
                lastName = removeCharacter(Console.ReadLine(), ",");

                var regex = new Regex(@"[a-zA-Z]{1,25}");
                Match match = regex.Match(lastName);
                if (match.Success)
                {
                    lastNameRegexMatch = true;
                }
                else
                {
                    Console.WriteLine("Invalid last name");
                    lastNameRegexMatch = false;
                }

            }

            string IBAN = null;
            bool IBANregexMatches = false;
            while (IBANregexMatches == false)
            {
                Console.WriteLine("Please enter IBAN:");
                IBAN = removeCharacter(Console.ReadLine(), ",");

                var regex = new Regex(@"[a-zA-Z0-9]{18,34}");
                Match match = regex.Match(IBAN);
                if (match.Success)
                {
                    IBANregexMatches = true;
                }
                else
                {
                    Console.WriteLine("Invalid IBAN");
                    IBANregexMatches = false;
                }

            }


            Console.WriteLine("Please enter address:");
            string address = removeCharacter(Console.ReadLine(), ",");

            // \b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}\b
            string email = null;

            bool emailRegexMatches = false;
            while (emailRegexMatches == false)
            {
                Console.WriteLine("Please enter email:");
                email = removeCharacter(Console.ReadLine(), ",");

                var regex = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    emailRegexMatches = true;
                }
                else
                {
                    Console.WriteLine("Invalid email");
                    emailRegexMatches = false;
                }

            }
            Console.WriteLine("Please enter loan balance:");
            string loanAmount = removeCharacter(Console.ReadLine(), ",");

            Console.WriteLine("Please enter password:");
            string password = removeCharacter(Console.ReadLine(), ",");

            string id = (Convert.ToInt32(customersListClone.customers.LastOrDefault().ID) + 1).ToString();
            Console.WriteLine($"id: {id}");
            customersListClone = null;
            Customer newCustomer = new Customer(address, IBAN, loanAmount, password, id, firstName, lastName, email);
            newCustomers.Add(newCustomer);
            newCustomer = null;
            CustomersList newCustomerList = new CustomersList(newCustomers);
            newCustomers = null;
            updateCSV(newCustomerList.customers);
            newCustomerList = null;
            GC.Collect();
            Console.ReadKey();


        }

        private static string removeCharacter(string text, string characterToRemove)
        {
            if (text.GetType() == typeof(string) && characterToRemove.GetType() == typeof(string))
            {
                text = text.Replace(characterToRemove, "");
                return text;
            }
            else
            {
                return "";
            }
        }
        private static void updateCSV(List<Customer> customers)
        {
            if (customers.GetType() == typeof(List<Customer>))
            {
                using (StreamWriter bank_accounts = new StreamWriter("loans.csv"))
                {
                    foreach (Customer customer in customers)
                        bank_accounts.WriteLine(customer.Address + "," + customer.IBAN + "," + customer.LoanRemaining + "," + customer.Password + "," + customer.ID + "," + customer.FirstName + "," + customer.LastName + "," + customer.EmailAddress);
                }
            }
        }

        public static byte[] Protect(byte[] data)
        {
            if (data.GetType() == typeof(byte[]))
            {
                try
                {
                    return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static byte[] Unprotect(byte[] data)
        {
            if (data.GetType() == typeof(byte[]))
            {
                try
                {
                    //Decrypt the data using DataProtectionScope.CurrentUser.
                    return ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.ToString());
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private static byte[] EncryptCustomerList(CustomersList customerList)
        {
            if (customerList.GetType() == typeof(CustomersList))
            {
                customerList = new CustomersList(GetCustomers());

                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, customerList.customers);
                bf = null;
                byte[] usersByteArray = ms.ToArray();
                ms.Close();
                ms = null;
                customerList = null;
                GC.Collect();
                byte[] encryptedSecret = Protect(usersByteArray);
                return encryptedSecret;
            }
            else
            {
                return null;
            }

        }
        private static CustomersList DecryptCustomerList(byte[] encryptedSecret)
        {
            if (encryptedSecret.GetType() == typeof(byte[]))
            {
                byte[] originalData = Unprotect(encryptedSecret);
                CustomersList originalCustomers;

                using (MemoryStream memorystream = new MemoryStream(originalData))
                {
                    IFormatter br = new BinaryFormatter();
                    List<Customer> customers = (List<Customer>)br.Deserialize(memorystream);
                    originalCustomers = new CustomersList(customers);
                }

                return originalCustomers;
            }
            else
            {
                return null;
            }

        }
    }





}
