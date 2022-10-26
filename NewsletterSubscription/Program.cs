// Demo code:


using NewsletterSubscription.Implementations;using NewsletterSubscription.Model;

var emailService = new DummyEmailService();
var subscriptionRepo= new SubscriptionFileRepository();

var email = new Email(
    "per@getacademy.no",
    "pål@getacademy.no",
    "Hei",
    "Tekst...");

emailService.Send(email);

var subscription = new Subscription("Terje", "terje@getacademy.no");
subscriptionRepo.Save(subscription);

subscription = subscriptionRepo.Load("terje@getacademy.no");
Console.WriteLine(subscription);