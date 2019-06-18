using System;
using System.Collections.Generic;
using System.Text;

namespace nHibernate.entities
{
    public class Message
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }

        public virtual MessagePriority Priority { get; set; }
        
        public virtual User Author { get; set; }

        public virtual ISet<MessageRecipient> Recipients { get; set; }
    }

    public enum MessagePriority : byte
    {
        Lowest = 0,
        Low = 1,
        Mid = 2,
        High = 3,
        Highest = 4
    }

}
