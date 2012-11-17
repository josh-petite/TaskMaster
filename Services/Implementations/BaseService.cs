using System.ServiceModel.Activation;
using StructureMap;

namespace Services.Implementations
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public abstract class BaseService
    {
        public T Resolve<T>()
        {
            return (T)ObjectFactory.GetInstance(typeof(T));
        }
    }
}