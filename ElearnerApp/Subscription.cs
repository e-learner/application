using System.Collections.Generic;

namespace ElearnerApp
{
    public class Subscription
    {
        private Dictionary<Course, int> _subscriptedCourses;

        public Subscription ()
        {
            _subscriptedCourses = new Dictionary<Course, int>();
        }
    }
}
