using nHibernate.entities;
using System;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Linq;
using static System.Console;

namespace nHibernate.core.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var N = new NHibernateTest();

            bool ExecuteDDL = false;
            N.ShowSQLMigrationCode(ExecuteDDL);

            //Console.WriteLine("\r\n\r\n");

            N.RunTest();
        }
    }

    class NHibernateTest
    {
        protected void WriteSeparator(int Length = 0)
        {
            if (Length == 0) Length = Console.WindowWidth;
            Console.WriteLine("".PadRight(Length, '_'));
        }

        public void ShowSQLMigrationCode(bool ExecuteDDL = false)
        {
            Configuration cfg = new Configuration().Configure();

            WriteSeparator();
            WriteLine("DDL");
            WriteLine();            

            new SchemaExport(cfg).Create(true, ExecuteDDL);

            WriteLine();
            WriteSeparator();
        }

        public void RunTest()
        {
            WriteSeparator();
            WriteLine("SQL");
            WriteLine();

            var sessionFactory = new Configuration().Configure().BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                // populate the database  
                using (var transaction = session.BeginTransaction())
                {
                    
                    //transaction.Commit();
                }


            }

            WriteLine();
            WriteSeparator();
        }


        public void RunTestOld()
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
                    

                    using (var transaction = session2.BeginTransaction())
                    {
                        
                        var deleteResult = session2.Query<User>().Where(u => u.Age == 85).Delete();
                        transaction.Commit(); //without Commit objects are deleted in memory, not in database

                        Console.WriteLine("Deleted: " + deleteResult);
                        
                        var adultUsers = session2.Query<User>().Where(u => u.Age > 18 && u.Name == "Zenek").Take(3).ToList();

                        foreach (var user in adultUsers)
                        {
                            Console.WriteLine($"User: {user.Name}, age: {user.Age}.");
                        }

                        var testCounter = session2.Query<User>().Where(u => u.Name.StartsWith("R")).Select(u => u.Age).ToFutureValue(q => q.Count());
                        var minAge = 99;
                            
                        if (testCounter.Value>0) minAge = session2.Query<User>().Where(u => u.Name.StartsWith("R")).Select(u => u.Age).Min();

                        Console.WriteLine($"Minimal age: {minAge}.");
                    }
                }
            }
        }
    }
}
