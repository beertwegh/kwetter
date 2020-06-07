using System;

namespace MessageService.Helpers.MessageBroker
{
    public interface IRpcCalls
    {
        string GetUserName(Guid userId);
    }
}