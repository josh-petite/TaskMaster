namespace Server.Entities.Maps
{
    public class TaskMap : BaseClassMap<Task>
    {
        public TaskMap()
        {
            Id(o => o.Id).Column("TaskId");
            Map(o => o.Description);
            Map(o => o.CompletedAt);
            Map(o => o.CompletedBy);
            Map(o => o.Deadline);
        }
    }
}