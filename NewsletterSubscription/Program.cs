using NewsletterSubscription;
using NewsletterSubscription.Implementations;
using NewsletterSubscription.Model;

var emailService = new DummyEmailService();
var subscriptionRepo = new SubscriptionFileRepository();
var subscriptionService = new SubscriptionService(emailService, subscriptionRepo);

while (true)
{
    Console.Write("Meny:\n1: Melde på nyhetsbrev\n2: Verifisere\n");
    var choice = Console.ReadLine();
    if (choice == "1")
    {
        Console.Write("Skriv inn epostadresse: ");
        var emailAddress = Console.ReadLine();
        subscriptionService.Subscribe(emailAddress);
    }
    else if (choice == "2")
    {
        Console.Write("Skriv inn epostadresse: ");
        var emailAddress = Console.ReadLine();
        Console.Write("Skriv inn verifikasjonskode: ");
        var code = Console.ReadLine();
        subscriptionService.Verify(emailAddress, code);
    }
}


// Demo code:

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