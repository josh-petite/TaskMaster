namespace Server.Entities.Maps
{
    public class TaskDetailMap : BaseClassMap<TaskDetail>
    {
        public TaskDetailMap()
        {
            Id(o => o.Id).Column("TaskDetailId");
            Map(o => o.Text);
            HasOne(o => o.Task);
        }
    }
}