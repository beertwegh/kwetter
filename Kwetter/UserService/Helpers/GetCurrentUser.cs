using System;
using Microsoft.AspNetCore.Mvc.Filters;
using RabbitMQ.Client;
using System.Linq;
using System.Net;
using System.Text;

namespace ProfileService.Helpers
{
    public class GetCurrentUser : ActionFilterAttribute
    {
        public static Guid UserId = Guid.Empty;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (UserId != Guid.Empty)
                return;
            string authorizationHeader = context.HttpContext.Request
                    .Headers[HttpRequestHeader.Authorization.ToString()]
                    .FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return;
            }

            var rpcClient = new RpcClient();
            Console.WriteLine(" [x] Requesting fib(30)");
            string response = rpcClient.Call(authorizationHeader);
            Console.WriteLine(" [.] Got '{0}'", response);
            UserId = Guid.Parse(response);
            rpcClient.Close();

        }
    }
}
