using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;


namespace MySignalR.Hubs
{
    public class CustomFilter : IHubFilter
    {
        public async ValueTask<object> InvokeMethodAsync(
            HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            Console.WriteLine($"Calling hub method '{invocationContext.HubMethodName}'");
            try
            {
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception calling '{invocationContext.HubMethodName}'");
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        // Optional method
        public Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            return next(context);
        }

        // Optional method
        public Task OnDisconnectedAsync(
            HubLifetimeContext context, Exception exception, Func<HubLifetimeContext, Exception, Task> next)
        {
            return next(context, exception);
        }
    }
}
