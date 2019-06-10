using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using nHibernate_entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace nHibernate_core_console.SessionFactories
{
    public class SessionFactoryBuilder
    {
        //var listOfEntityMap = typeof(M).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(M))).ToList();  
        //var sessionFactory = SessionFactoryBuilder.BuildSessionFactory(dbmsTypeAsString, connectionStringName, listOfEntityMap, withLog, create, update);  
        public static ISessionFactory BuildSessionFactory(string connectionStringName)
        {

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionStringName))
                .Mappings(m => m.HbmMappings.AddFromAssembly(typeof(nHibernate_setup.NHSetup).Assembly))               //AddFromAssemblyOf<User>())
                .BuildSessionFactory();
        }
        /// <summary>  
        /// Build the schema of the database.  
        /// </summary>  
        /// <param name="config">Configuration.</param>  
        private static void BuildSchema(Configuration config, bool create = false, bool update = false)
        {
            if (create)
            {
                new SchemaExport(config).Create(false, true);
            }
            else
            {
                new SchemaUpdate(config).Execute(false, update);
            }
        }
    }
}
