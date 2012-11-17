using System;

namespace Server.Entities
{
    public class Task : BaseEntity
    {
        public virtual string Description { get; set; }
        public virtual DateTime CompletedAt { get; set; }
        public virtual string CompletedBy { get; set; }
        public virtual DateTime Deadline { get; set; }
    }
}