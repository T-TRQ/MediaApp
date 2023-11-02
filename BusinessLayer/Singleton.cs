using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Singleton
    {
        private static Singleton _singleton;

        public User _loggedInUser;
        public User _selectedUser;

        private Singleton() { }


        public static Singleton SetLoggedIn(User u)
        {
            if (_singleton == null)
            {
                _singleton = new Singleton();
                _singleton._loggedInUser = u;
            }
            return _singleton;
        }

        public static Singleton GetLoggedIn()
        {
            if (_singleton == null)
            {
                _singleton = new Singleton();
            }
            return _singleton;
        }

    }
}
