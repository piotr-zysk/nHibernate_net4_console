using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using nHibernate.entities;

namespace nHibernate.net4.console
{
    class Program
    {
        static void Main(string[] args)
        {
            new NHibernateTest().RunTest();
        }
    }

    class NHibernateTest
    {
        public void RunTest()
        {
            ISessionFactory sessionFactory = new Configuration().Configure().BuildSessionFactory();

            var session = sessionFactory.OpenSession();

            using (ITransaction tx = session.BeginTransaction())
            {

                // insert to db
                var newUser = new User
                {
                    //Name = "Zdzisława J.",
                    //Age = 58
                };

                session.Save(newUser);
                tx.Commit();

                // select from db

                /*
                var adultUsers = session.Query<User>().Where(u => u.Age >= 18).ToList();
                
                foreach (var user in adultUsers)
                {
                    Console.WriteLine($"User: {user.Name}, age: {user.Age}.");
                }
                */

            }

            sessionFactory.Close();
        }
    }
}
