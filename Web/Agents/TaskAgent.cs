using System.Collections.Generic;
using System.Linq;
using Services.Definitions;
using Web.Entities;
using Web.Translators;

namespace Web.Agents
{
    public interface ITaskAgent
    {
        Task GetTask();
        void UpdateTasks(IEnumerable<Task> tasks);
        IEnumerable<Task> GetTasks();
    }

    public class TaskAgent : BaseAgent, ITaskAgent
    {
        private readonly ITaskTranslator _taskTranslator;

        public TaskAgent(ITaskTranslator taskTranslator)
        {
            _taskTranslator = taskTranslator;
            UserName = "Josh";
            Password = "test";
        }

        public Task GetTask()
        {
            var client = GetClient();
            var taskDros = client.Execute(o => o.GetTasks());

            return _taskTranslator.DroToEntity(taskDros.First());
        }

        public void UpdateTasks(IEnumerable<Task> tasks)
        {
            var client = GetClient();
            var translatedTasks = _taskTranslator.EntitiesToDuos(tasks);
            client.Execute(o => o.UpdateTasks(translatedTasks));
        }

        public IEnumerable<Task> GetTasks()
        {
            var client = GetClient();
            var taskDros = client.Execute(o => o.GetTasks());

            return _taskTranslator.DrosToEntities(taskDros);
        }

        private ITaskService GetClient()
        {
            return GetClient<ITaskService>("TaskService");
        }
    }
}