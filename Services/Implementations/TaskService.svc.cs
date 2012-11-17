using System.ServiceModel;
using Server.Logic;
using Server.Translation;
using Services.Definitions;
using Shared.Contracts;
using System.Linq;

namespace Services.Implementations
{
    [ServiceContract]
    public class TaskService : BaseService, ITaskService
    {
        public TaskDro[] GetTasks()
        {
            var taskLogic = Resolve<ITaskLogic>();
            var taskTranslator = Resolve<ITaskTranslator>();
            var tasks = taskLogic.GetTasks();

            return tasks.Select(taskTranslator.EntityToDro).ToArray();
        }

        public void UpdateTasks(TaskDuo[] taskDuos)
        {
            throw new System.NotImplementedException();
        }
    }
}