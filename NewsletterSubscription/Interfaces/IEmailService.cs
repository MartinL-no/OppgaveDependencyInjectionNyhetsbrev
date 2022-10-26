using NewsletterSubscription.Model;

namespace NewsletterSubscription.Interfaces
{
    internal interface IEmailService
    {
        void Send(Email email);
    }
}
