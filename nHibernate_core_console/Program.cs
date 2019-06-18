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

            bool ExecuteDDL = false;
            N.ShowSQLMigrationCode(ExecuteDDL);
            
            //N.RunTest();
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
                
                var user = new User()
                {
                    FirstName = "Zenon",
                    LastName = "Łopata",
                    Messages = new HashSet<MessageRecipient>()
                };

                var message = new Message()
                {
                    //Author = user,                    
                    Title = "tytuł",
                    Content = "jakaś wiadomość",
                    Priority = MessagePriority.Low,
                    Recipients = new HashSet<MessageRecipient>()
                };

                var mr = new MessageRecipient()
                {
                    //Message = message,
                    User = user,
                    Status = ReadStatus.New
                };

                message.Recipients.Add(mr);
                user.Messages.Add(mr);

                var x = message.Recipients.FirstOrDefault().User.LastName;
                //var y = message.Author.LastName;
                //WriteLine($"{x} {y}");

                /*
                var p = new Parent() { Name = "test", Children = new HashSet<Child>() };
                WriteLine("*** Adding new Parent:");
                session.Save(p);
                */

                /*
                using (var transaction = session.BeginTransaction())
                {
                    var p = session.Query<Parent>().FirstOrDefault();

                    var c = new Child("child");
                    c.Parent = p;
                    p.Children.Add(c);

                    transaction.Commit();

                    WriteLine("*** Adding Child:");
                    
                    //session.Save(c);

                    session.Flush();


                    var ChildrenQuery = session.Query<Parent>().Where(x => x.Id == 1).Select(x => x.Children);
                    var ChildrenCount = ChildrenQuery.Select(x => x.Count()).First();

                    WriteLine(ChildrenCount);

                    var ChildrenConcat = ChildrenQuery.ToArray()[0].Aggregate("", (res, next) => res+","+(next as Child).Name).Substring(1);
                    
                        
                    Console.WriteLine($"{ChildrenCount} children: {ChildrenConcat}");
                }
                */
            }

            WriteLine();
            WriteSeparator();
        }



    }
}
