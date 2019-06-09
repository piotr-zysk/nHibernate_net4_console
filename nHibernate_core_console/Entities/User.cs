using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nHibernate_core_console.Entities
{
    public class User
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
    }

  
}
