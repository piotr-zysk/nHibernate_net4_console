using System.Collections.Generic;

namespace nHibernate.entities
{
    public class MessageRecipient
    {
        public virtual Message Message { get; set; }
        public virtual User User { get; set; }
        public virtual ReadStatus Status { get; set; }
    }

    public enum ReadStatus : byte
    {
        New = 0,
        Unread = 1,
        Read = 2,
        Deleted = 11
    }
}
