using System.ServiceModel;
using System.ServiceModel.Web;
using Shared.Contracts;

namespace Services.Definitions
{
    [ServiceContract]
    public interface ITaskService : IBaseService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetTasks", ResponseFormat = WebMessageFormat.Json)]
        TaskDro[] GetTasks();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateTasks", RequestFormat = WebMessageFormat.Json)]
        void UpdateTasks(TaskDuo[] taskDuos);
    }
}