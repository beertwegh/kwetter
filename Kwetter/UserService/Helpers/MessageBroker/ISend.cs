using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Helpers.MessageBroker
{
    public interface ISendMessageBroker
    {
        void Registration(UserRegistrationModel user);
    }
}
