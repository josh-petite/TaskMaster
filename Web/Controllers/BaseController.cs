using System.Web.Mvc;
using StructureMap;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected T Resolve<T>()
        {
            return (T)ObjectFactory.GetInstance(typeof(T));
        }
    }
}