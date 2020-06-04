using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageService.Models;

namespace MessageService.Helpers.MessageBroker
{
    public interface ISendMessageBroker
    {
        void Registration(UserRegistrationModel user);
    }
}
