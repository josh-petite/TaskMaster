namespace Server.Entities
{
    public class TaskDetail : BaseEntity
    {
        public virtual string Text { get; set; }
        public virtual Task Task { get; set; }
    }
}