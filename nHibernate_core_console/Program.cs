﻿using nHibernate_entities;
using System;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace nHibernate_core_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var N = new NHibernateTest();

            N.ShowSQLMigrationCode();

            Console.WriteLine("\r\n\r\n");

            N.RunTest();
        }
    }

    class NHibernateTest
    {
        public void ShowSQLMigrationCode()
        {
            Configuration cfg = new Configuration().Configure();
            new SchemaExport(cfg).Create(true, false);
        }

        public void RunTest()
        {
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

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
