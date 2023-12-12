using System;

//review code first before even submitting it! old code that was refurbished
class Address
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {State}, {Country}";
    }
}

class Event
{
    private string Name { get; set; }
    private DateTime Date { get; set; }
    private Address Location { get; set; }

    public Event(string name, DateTime date, Address location)
    {
        Name = name;
        Date = date;
        Location = location;
    }

    public string GetEventDetails()
    {
        return $"{Name} on {Date.ToShortDateString()} at {Location.GetFullAddress()}";
    }

    public virtual string GetReminderMessage()
    {
        return $"Don't forget about the upcoming {Name} on {Date.ToShortDateString()}!";
    }

    public virtual string GetThankYouMessage()
    {
        return $"Thank you for purchasing ticket's at {Name} on {Date.ToShortDateString()} at {Location.GetFullAddress()}. We hope you enjoyed the concert!!!";
    }
}

class Concert : Event
{
    // reminder that you might have to fix yoy strings
    private string Headliner { get; set; }

    public Concert(string name, DateTime date, Address location, string headliner) 
        : base(name, date, location)
    {
        Headliner = headliner;
    }

    public override string GetReminderMessage()
    {
        return $"Get ready for an amazing concert! {Headliner} will be performing at {base.GetEventDetails()}.";
    }
}

class Seminar : Event
{
    private string Speaker { get; set; }

    public Seminar(string name, DateTime date, Address location, string speaker) 
        : base(name, date, location)
    {
        Speaker = speaker;
    }

    public override string GetThankYouMessage()
    {
        return $"Thank you for attending the seminar by {Speaker} at {base.GetEventDetails()}. We hope you gained valuable insights!";
    }
}

class Wedding : Event
{
    private string Couple { get; set; }

    public Wedding(string name, DateTime date, Address location, string couple) 
        : base(name, date, location)
    {
        Couple = couple;
    }

    public string GetAnnouncementMessage()
    {
        return $"We are excited to announce the wedding of {Couple} at {base.GetEventDetails()}!";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address eventAddress = new Address("1410 Olympic Way SE", "Calgary", "AB", "Canada");

        Event genericEvent = new Event("Calgary Stampede", DateTime.Now.AddDays(30), eventAddress);
        Concert concertEvent = new Concert("Cowboys Club", DateTime.Now.AddDays(45), eventAddress, "Drake & J Cole");
        Seminar seminarEvent = new Seminar("The mindset of a strong man", DateTime.Now.AddDays(60), eventAddress, "Andrew Tate");
        Wedding weddingEvent = new Wedding("Noah and Kennedy's Wedding", DateTime.Now.AddDays(90), eventAddress, "Noah & Kennedy");

        Console.WriteLine(genericEvent.GetReminderMessage());
        Console.WriteLine(genericEvent.GetThankYouMessage());
        Console.WriteLine();

        Console.WriteLine(concertEvent.GetReminderMessage());
        Console.WriteLine(concertEvent.GetThankYouMessage());
        Console.WriteLine();

        Console.WriteLine(seminarEvent.GetReminderMessage());
        Console.WriteLine(seminarEvent.GetThankYouMessage());
        Console.WriteLine();

        Console.WriteLine(weddingEvent.GetReminderMessage());
        Console.WriteLine(weddingEvent.GetThankYouMessage());
        Console.WriteLine(weddingEvent.GetAnnouncementMessage());
    }  // if there's anny mistakes fie em
}
