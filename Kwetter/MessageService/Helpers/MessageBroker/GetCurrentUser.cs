using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using MessageService.Helpers.MessageBroker;

namespace MessageService.Helpers
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
            Console.WriteLine(" [x] Requesting userid");
            string response = rpcClient.Call(authorizationHeader, "getuserid");
            Console.WriteLine(" [.] Got '{0}'", response);
            UserId = Guid.Parse(response);
            rpcClient.Close();

        }
    }
}
