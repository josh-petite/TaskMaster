using Server.Entities;
using Shared.Contracts;

namespace Server.Translation
{
	public interface ITaskTranslator
	{
	    TaskDro EntityToDro(Task task);
	}
	public class TaskTranslator : ITaskTranslator
	{
	    public TaskDro EntityToDro(Task task)
	    {
	        return new TaskDro
	                   {
                           Id = task.Id,
                           Description = task.Description,
                           CompletedAt = task.CompletedAt,
                           CompletedBy = task.CompletedBy,
                           Deadline = task.Deadline
	                   };
	    }
	}
}