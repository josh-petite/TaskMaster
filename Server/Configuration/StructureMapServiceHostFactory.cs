using System.ServiceModel.Activation;

namespace Server.Configuration
{
    public class StructureMapServiceHostFactory : ServiceHostFactory
    {
        public StructureMapServiceHostFactory()
        {
            ServerRegistry.Configure();
        }
    }
}