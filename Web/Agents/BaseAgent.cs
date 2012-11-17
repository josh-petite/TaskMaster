using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace Web.Agents
{
    public class BaseAgent
    {
        public static string UserName { get; set; }
        public static string Password { get; set; }

        protected T GetClient<T>(string bindingName)
        {
            var factory = new ChannelFactory<T>(bindingName);
            factory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            var credentials = factory.Endpoint.Behaviors.Find<ClientCredentials>();
            credentials.UserName.UserName = UserName;
            credentials.UserName.Password = Password;
            var client = factory.CreateChannel();
            var channel = (IChannel)client;
            channel.Closed += (s, e) => factory.Close();

            return client;
        }
    }
}