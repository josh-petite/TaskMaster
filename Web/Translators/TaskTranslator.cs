using System.Collections.Generic;
using System.Linq;
using Shared.Contracts;
using Web.Entities;

namespace Web.Translators
{
    public interface ITaskTranslator
    {
        Task DroToEntity(TaskDro taskDro);
        TaskDuo[] EntitiesToDuos(IEnumerable<Task> tasks);
        IEnumerable<Task> DrosToEntities(TaskDro[] taskDros);
    }

    public class TaskTranslator : ITaskTranslator
    {
        public Task DroToEntity(TaskDro taskDro)
        {
            return new Task
                       {
                           Completed = !string.IsNullOrEmpty(taskDro.CompletedBy),
                           Description = taskDro.Description,
                           CompletedAt = taskDro.CompletedAt,
                           CompletedBy = taskDro.CompletedBy,
                           Deadline = taskDro.Deadline,
                           Id = taskDro.Id
                       };
        }

        public TaskDuo[] EntitiesToDuos(IEnumerable<Task> tasks)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Task> DrosToEntities(TaskDro[] taskDros)
        {
            return taskDros.Select(DroToEntity);
        }
    }
}