using System;

namespace ElearnerApp
{
    public class Student : User
    {

        public Student (string name, string lastName, DateTime birthDate, string username, string password) : base(name, lastName, birthDate, username, password)
        {
        }

        public override void Login ()
        {
            throw new NotImplementedException();
        }
    }
}
