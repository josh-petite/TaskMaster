using System;
using System.Runtime.Serialization;

namespace Shared.Contracts
{
    [DataContract]
    public class TaskDuo
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string CompletedBy { get; set; }
        [DataMember]
        public DateTime CompletedAt { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTime Deadline { get; set; }
    }
}