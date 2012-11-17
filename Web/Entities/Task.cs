using System;

namespace Web.Entities
{
    public class Task
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public string CompletedBy { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime Deadline { get; set; }
    }
}