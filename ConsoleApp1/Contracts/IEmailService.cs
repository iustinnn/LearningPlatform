using GlobalBuyTicket.Application.Models;

namespace GlobalBuyTicket.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);
    }
}
