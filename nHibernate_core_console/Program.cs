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
                
                var user = new User()
                {
                    FirstName = "Zenon",
                    LastName = "Łopata",
                    Messages = new HashSet<MessageRecipient>()
                };
                
                var message = new Message()
                {
                    Author = user,                    
                    Title = "tytuł",
                    Content = "jakaś wiadomość",
                    Priority = MessagePriority.Low,
                    Recipients = new HashSet<MessageRecipient>()
                };

                var mr = new MessageRecipient()
                {
                    Message = message,
                    User = user,
                    Status = ReadStatus.New
                };

                message.Recipients.Add(mr);
                user.Messages.Add(mr);

                using (var transaction = session.BeginTransaction())
                {

                    session.Save(message);
                    session.Save(user);

                    transaction.Commit();
                }
                
            }

            WriteLine();
            WriteSeparator();
        }



    }
}
