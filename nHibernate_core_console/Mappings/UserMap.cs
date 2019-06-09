using FluentNHibernate.Mapping;
using nHibernate_core_console.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace nHibernate_core_console.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(b => b.Id);
            Map(b => b.Name);
            Map(b => b.Age);
            Table("Users");
        }
    }
}
