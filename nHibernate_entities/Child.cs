using System;
using System.Collections.Generic;
using System.Text;

namespace nHibernate.entities
{
    public class Child
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Parent Parent { get; set; }

        public Child() { }

        public Child(string name)
        {
            this.Name = name;
        }

    }
}
