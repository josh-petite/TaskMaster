using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Server.Behavior
{
    public class TaskMasterBehaviorSection : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new TaskMasterLoggingEndpointBehavior();
        }

        public override Type BehaviorType
        {
            get { return typeof(TaskMasterLoggingEndpointBehavior); }
        }
    }

    public class TaskMasterLoggingEndpointBehavior : IEndpointBehavior
    {
        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CustomMessageInspector(endpoint.Contract));
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }
    }
}
