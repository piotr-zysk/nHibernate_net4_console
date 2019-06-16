using System;
using System.Collections.Generic;
using System.Text;

namespace nHibernate.entities
{
    public class Parent
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ISet<Child> Children { get; set; }
    }
}
