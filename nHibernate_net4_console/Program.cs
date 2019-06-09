using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using nHibernate_net4_console.Models;

namespace nHibernate_net4_console
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
                var newUser = new User
                {
                    Name = "Zenek W.",
                    Age = 69
                };

                session.Save(newUser);
                tx.Commit();
            }

            sessionFactory.Close();
        }
    }
}
