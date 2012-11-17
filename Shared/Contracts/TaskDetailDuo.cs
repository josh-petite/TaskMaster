using System;
using System.Runtime.Serialization;

namespace Shared.Contracts
{
    [DataContract]
    public class TaskDetailDuo
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string CreatedBy { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public long TaskId { get; set; }
    }
}ÿ