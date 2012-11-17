using System;

namespace Server.Entities
{
    public class BaseEntity : IPersistable
    {
        public virtual long Id { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }

        public virtual bool IsNew { get { return Id <= 0; } }
    }
}