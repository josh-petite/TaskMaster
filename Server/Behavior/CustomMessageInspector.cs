using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Server.Log;
using StructureMap;

namespace Server.Behavior
{
    public class CustomMessageInspector : IDispatchMessageInspector
    {
        private readonly ContractDescription _contract;

        public CustomMessageInspector(ContractDescription contract)
        {
            _contract = contract;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var logger = ObjectFactory.GetInstance<ILogger>();
            logger.StartAction(request.Headers.To.ToString());
            logger.Write("Incoming Message", request.ToString());
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var logger = ObjectFactory.GetInstance<ILogger>();
            MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
            var newResult = buffer.CreateMessage();
            string message = buffer.CreateMessage().ToString();
            logger.Write("Outgoing Message", message);
            logger.PersistLog();
            reply = newResult;
        }
    }
}