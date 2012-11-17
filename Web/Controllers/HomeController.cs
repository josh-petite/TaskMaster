using System.Web.Mvc;
using Web.Agents;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var taskAgent = Resolve<ITaskAgent>();
            var tasks = taskAgent.GetTasks();

            return View(tasks);
        }
    }
}