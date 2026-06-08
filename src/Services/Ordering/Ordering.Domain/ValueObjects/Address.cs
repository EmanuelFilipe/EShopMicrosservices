namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string EmailAddress { get; private set; } = default!;
    public string AddressLine { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string State { get; init; } = default!;
    public string ZipCode { get; init; } = default!;

    // for EF Core 
    protected Address() { }

    private Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(string firstName, string lastName, string emailAddress, string addressLine, string country, string state, string zipCode)
    {
        //ArgumentNullException.ThrowIfNull(firstName, nameof(firstName));
        //ArgumentNullException.ThrowIfNull(lastName, nameof(lastName));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(emailAddress, nameof(emailAddress));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(addressLine, nameof(addressLine));
        //ArgumentNullException.ThrowIfNull(country, nameof(country));
        //ArgumentNullException.ThrowIfNull(state, nameof(state));
        //ArgumentNullException.ThrowIfNull(zipCode, nameof(zipCode));

        return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);    
    }
}
