using System;

namespace ElearnerApp
{
    public class Course
    {
        private static int _id = 1;

        public int Id { get { return _id; } }
        public string Name { get; private set; }
        public int Duration { get; private set; }
        public double Price { get; private set; }
        //TODO create Teacher class
        public int Instructor { get; private set; }

        public Course (string name, int duration, double price, int instructorID)
        {
            // ayrio!
        }
    }
}