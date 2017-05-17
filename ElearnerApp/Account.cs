using System;

namespace ElearnerApp
{
    public class Account
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public Account (string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            {
                UserName = username;
                Password = password;
            }
            else
                throw new ArgumentException("Wrong Arguments, Username or Password cannot be null or empty!");
        }

        public override string ToString ()
        {
            return $"{UserName} {Password}";
        }
    }
}