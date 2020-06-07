using System;

namespace MessageService.Helpers.MessageBroker
{
    public class RpcCalls : IRpcCalls
    {

        public string GetUserName(Guid userId)
        {
            var rpcClient = new RpcClient();
            Console.WriteLine(" [x] Requesting username");
            string response = rpcClient.Call(userId.ToString(), "getusername");
            Console.WriteLine(" [.] Got '{0}'", response);
            rpcClient.Close();
            return response;
        }
    }
}
