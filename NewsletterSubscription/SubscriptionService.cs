using NewsletterSubscription.Interfaces;
using NewsletterSubscription.Model;

namespace NewsletterSubscription
{
    internal class SubscriptionService
    {
        private readonly IEmailService _emailService;
        private ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(
            IEmailService emailService, 
            ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _emailService = emailService;
        }

        public void Subscribe(string emailAddress)
        {
            var previousSubscription = _subscriptionRepository.Load(emailAddress);

            if (previousSubscription != null)
            {
                SendAlreadySubscribed(previousSubscription);
            }
            else
            {
                var name = emailAddress.Split('@')[0];
                var nameUpperCaseFirstLetter = char.ToUpper(name[0]) + name.Substring(1);

                var verificationCode = GetVerificationCode();
                var subscription = new Subscription(nameUpperCaseFirstLetter, emailAddress, verificationCode);
                _subscriptionRepository.Save(subscription);

                var email = new Email(
                    subscription.Email,
                    "info@getacademy.no",
                    "Get Academy Verification Code",
                    $"Hei {subscription.Name} your verification code is {subscription.VerificationCode}");

                _emailService.Send(email);
            }
        }
        private string GetVerificationCode()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();

            return _rdm.Next(_min, _max).ToString();
        }
        private void SendAlreadySubscribed(Subscription subscription)
        {
            var email = new Email(
                subscription.Email,
                "info@getacademy.no",
                "Your already subscribed!",
                $"Hei {subscription.Name} you are already subscribed to our newsletter");

            _emailService.Send(email);
        }

        public void Verify(string emailAddress, string verificationCode)
        {
            var subscription = _subscriptionRepository.Load(emailAddress);

            if (subscription.VerificationCode == verificationCode)
            {
                subscription.IsVerified = true;
                _subscriptionRepository.Save(subscription);
                var email = new Email(
                    subscription.Email,
                    "info@getacademy.no",
                    "Get Academy Subscription",
                    $"Hei {subscription.Name} your subscription to the Get Academy newsletter has started");
                _emailService.Send(email);
            }
        }
    }
}
