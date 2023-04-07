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

/*
    Start with this code:  github.com/GetAcademy/NewsletterSubscription

    Things are pretty much set up - and what is missing is to implement the methods Subscribe and
    Verify in SubscriptionService. In Program you will find both finished code with user interface
    and example code on how to use DummyEmailService and SubscriptionFileRepository.

    The logic is as follows.

    Subscribe 

    Create an object of class Subscription with the email address of the user
    Save this Subscription using subscriptionRepository.Save
    Send an email to the user with the verification code using emailService

    Verify

    Read Subscription from file using subscriptionRepository.Load and the email address of the user.
    Check if the code is correct. If it is
    set IsVerified to true and save Subscription with subscriptionRepository.Save
    send an email to the user that the subscription to the newsletter has started

    Extra task

    Do more error checking, so that you are notified if you subscribe to an email address that already exists
    Learn about interface mocking and check if your code works using unit tests and mocking
 */