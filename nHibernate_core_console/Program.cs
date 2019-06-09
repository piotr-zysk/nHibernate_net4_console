using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using nHibernate_core_console.Entities;
using nHibernate_core_console.SessionFactories;
using System;
using System.Linq;


namespace nHibernate_core_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            new NHibernateTest().RunTest();
        }
    }

    class NHibernateTest
    {
        public void RunTest()
        {
            var connStr = "Server =.; initial catalog = nh1; Integrated Security = True";

            var sessionFactory = SessionFactoryBuilder.BuildSessionFactory(connStr);

            using (var session = sessionFactory.OpenSession())
            {
                // populate the database  
                using (var transaction = session.BeginTransaction())
                {
                    // create a couple of Persons  
                    var person1 = new User
                    {
                        Name = "Rayen Trabelsi",
                        Age = 22
                    };

                    var person2 = new User
                    {
                        Name = "Umar Kakadu",
                        Age = 85
                    };

                    session.SaveOrUpdate(person1);
                    session.SaveOrUpdate(person2);

                    transaction.Commit();
                }

                using (var session2 = sessionFactory.OpenSession())
                {
                    using (session2.BeginTransaction())
                    {
                        var adultUsers = session2.Query<User>().Where(u => u.Age >= 18).ToList();

                        foreach (var user in adultUsers)
                        {
                            Console.WriteLine($"User: {user.Name}, age: {user.Age}.");
                        }
                    }
                }
            }
        }
    }
}
