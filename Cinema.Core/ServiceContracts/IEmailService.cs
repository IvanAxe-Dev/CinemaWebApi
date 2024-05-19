using Cinema.Core.Models;

namespace Cinema.Core.ServiceContracts
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
