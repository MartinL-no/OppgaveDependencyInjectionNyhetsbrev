using NewsletterSubscription.Model;

namespace NewsletterSubscription.Interfaces
{
    internal interface ISubscriptionRepository
    {
        void Save(Subscription subscription);
        Subscription Load(string email);
    }
}
