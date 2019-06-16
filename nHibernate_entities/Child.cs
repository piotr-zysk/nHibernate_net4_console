using System;
using System.Collections.Generic;
using System.Text;

namespace nHibernate.entities
{
    public class Child
    {
        public virtual int Id { get; set; }
        public virtual int Name { get; set; }
        public virtual Parent Parent { get; set; }

    }
}
