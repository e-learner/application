using System;

namespace ElearnerApp
{
    public abstract class User
    {
        private Account _userAccount;
        private DateTime _birthDate;
        private byte _age;

        public string Name { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }

        public byte Age { get { return _age; } }

        public User (string name, string lastName, DateTime birthDate, string username, string password)
        {
            try
            {
                _userAccount = new Account(username, password);
            }
            catch (Exception e)
            {
                //TODO  check how to return exception message
                Console.WriteLine(e.Message);
            }

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName))
            {
                Name = name;
                Lastname = lastName;
            }
            else
                throw new ArgumentException("Wrong Arguments, user Name or Last Name cannot be null or empty!");

            if (birthDate != null && (birthDate.Year > 1920 && birthDate.Year <= DateTime.Now.Year))
            {
                _birthDate = birthDate;
                _age = Convert.ToByte((DateTime.Now.Year - birthDate.Year));
            }
            else
                throw new ArgumentException("Wrong Arguments, uses Birth Date cannot be null or less than 1/1/1920 and greater than today!");
        }

        public override string ToString ()
        {
            return $"The user {Name} {Lastname} with account {_userAccount.ToString()}, email {Email} age {_age}";
        }

        // Every inherited user must descibe how a user can LogIn to the system.
        public abstract void Login ();
    }
}
