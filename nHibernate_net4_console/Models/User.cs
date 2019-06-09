using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nHibernate_net4_console.Models
{
    public class User
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
    }
}
