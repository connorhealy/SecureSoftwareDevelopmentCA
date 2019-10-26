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
                    Console.WriteLine($"Enter admin password:");
                    break;
                case "2":
                    Console.WriteLine($"Enter user password:");
                    break;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }
           
        }

        public void AdminLogin()
        {
            string password = 
            while()
        }
        //string username;
        //Console.WriteLine("log in:");
        //Console.WriteLine("username:");
        //username = Console.ReadLine();

        //Console.WriteLine(username);

        //List<string> usernames = new List<string>();
        //List<string> passwords = new List<string>();

        //using (var reader = new StreamReader(@"users.csv"))
        //{

        //    while (!reader.EndOfStream)
        //    {
        //        var line = reader.ReadLine();
        //        var values = line.Split(',');

        //        usernames.Add(values[0]);
        //        passwords.Add(values[1]);
        //    }
        //}

        //usernames.ForEach(single_username =>
        //{
        //    Console.WriteLine(single_username);
        //});

        //passwords.ForEach(single_password =>
        //{  
        //    Console.WriteLine(single_password);
        //});



    }

}
