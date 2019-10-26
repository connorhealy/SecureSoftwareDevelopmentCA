using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SecureSoftwareDevCA
{
    class Program
    {
        static void Main(string[] args)
        {
            int num1 = 0; int num2 = 0;

            // Ask the user to choose an option.
            Console.WriteLine("Select user to log in as:");
            Console.WriteLine("1 - Admin");
            Console.WriteLine("2 - User");

            switch (Console.ReadLine())
            {
                case "1":
                    AdminLogin();
                    break;
                case "2":
                    Console.WriteLine($"Enter user password:");
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }

        }

        public static void AdminLogin()
        {
            //string menuSelection = "";
            //while (menuSelection != "exit")
            //{

            //}
            List<BankCustomer> users = new List<BankCustomer>();

            using (var reader = new StreamReader(@"bank_accounts.csv"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    BankCustomer tempCustomer = new BankCustomer(values[0], values[1], values[2], values[3], values[4], values[5], values[6], values[7]);
                    users.Add(tempCustomer);
                }
            }

            users.ForEach(bank_account =>
            {
                Console.WriteLine(bank_account.FirstName);
            });



        }




    }

}
