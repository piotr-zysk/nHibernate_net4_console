﻿using System.Collections.Generic;

namespace nHibernate.entities
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual ISet<MessageRecipient> Messages { get; set; }
        public virtual ISet<Message> SentMessages { get; set; }
    }

  
}
