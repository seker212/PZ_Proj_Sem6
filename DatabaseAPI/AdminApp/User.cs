using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp
{
    public enum UserType
    {
        Admin,
        Manager
    }
    public class User
    {
        private string Name { get; set; }
        private UserType Type { get; set; }

        public User(string name, UserType type)
        {
            Name = name;
            Type = type;
        }

        public User()
        {
        }

        public void Login()
        {
            Console.Write("Login: ");
            Name = Console.ReadLine();
            Console.Write("Password: ");
            string password = null;
            while(true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
            }
        }
    }
}
