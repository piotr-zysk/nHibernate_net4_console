using nHibernate_core_console.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace nHibernate_core_console.Services
{
    class UserService
    {
        public class PersonService
        {
            public static void GetPerson(User user)
            {
                Console.WriteLine(user.Name);
                Console.WriteLine();
            }
        }
    }
}
