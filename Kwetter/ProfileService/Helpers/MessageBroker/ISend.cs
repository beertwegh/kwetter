using ProfileService.Models;

namespace ProfileService.Helpers.MessageBroker
{
    public interface ISendMessageBroker
    {
        void EditProfileName(EditProfileName model);
    }
}
