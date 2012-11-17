using System.Configuration;

namespace Server.Configuration
{
    public interface IConfiguration
    {
        string ConnectionString { get; }
        void Initialize();
    }

    public class Configuration : IConfiguration
    {
        public string ConnectionString { get; private set; }

        public void Initialize()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }
    }
}

