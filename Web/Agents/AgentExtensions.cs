using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Services.Definitions;

namespace Web.Agents
{
    public static class AgentExtensions
    {
        public static void Execute<T>(this T client, Action<T> execution) where T : IBaseService
        {
            var channel = (IChannel)client;

            try
            {
                channel.Open();
                execution.DynamicInvoke(client);
            }
            catch (Exception ex)
            {
                var exception = HandleException(ex);
                if (exception != null)
                    throw exception;

                throw;
            }
            finally
            {
                CloseChannel(channel);
            }
        }

        public static T Execute<TClient, T>(this TClient client, Func<TClient, T> execution) where TClient : IBaseService
        {
            var channel = (IChannel)client;
            T result;

            try
            {
                channel.Open();
                result = (T)execution.DynamicInvoke(client);
            }
            catch (Exception ex)
            {
                var exception = HandleException(ex);
                if (exception != null)
                    throw exception;

                throw;
            }
            finally
            {
                CloseChannel(channel);
            }

            return result;
        }

        private static void CloseChannel(IChannel channel)
        {
            try
            {
                if (channel != null)
                {
                    if (channel.State == CommunicationState.Faulted)
                        channel.Abort();
                    else
                        channel.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Something blew up when closing a channel: {0}", ex.Message);
            }
        }

        private static Exception HandleException(Exception exception)
        {
            var ex = exception as TargetInvocationException;

            if (ex != null)
                return HandleException(ex.InnerException);

            return exception.InnerException != null ? HandleException(exception.InnerException) : null;
        }
    }
}