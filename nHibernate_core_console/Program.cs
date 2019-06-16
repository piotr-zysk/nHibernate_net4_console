using nHibernate.entities;
using System;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Linq;
using static System.Console;
using System.Collections.Generic;

namespace nHibernate.core.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var N = new NHibernateTest();

            bool ExecuteDDL = true;
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
                
                var p = new Parent() { Name = "test", Children = new HashSet<Child>() };
                WriteLine("*** Adding new Parent:");
                session.Save(p);
                
                using (var transaction = session.BeginTransaction())
                {
                    //var p = session.Query<Parent>().FirstOrDefault();

                    var c = new Child("child2");
                    c.Parent = p;
                    p.Children.Add(c);

                    transaction.Commit();

                    WriteLine("*** Adding Child:");
                    
                    //session.Save(c);

                    session.Flush();


                    var ChildrenQuery = session.Query<Parent>().Where(x => x.Id == 1).Select(x => x.Children);
                    var ChildrenCount = ChildrenQuery.Select(x => x.Count());

                    WriteLine(ChildrenCount);

                    var ChildrenConcat = ChildrenQuery;
                        
                    //Console.WriteLine($"{ChildrenCount}:{ChildrenConcat}");
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
