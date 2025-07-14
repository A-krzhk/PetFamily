using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public record Address
{
    private Address(string region, string city, string street)
    {
        Region = region;
        City = city;
        Street = street;
    }
    public string Region { get; }
    public string City { get; }
    public string Street { get; }

    public Result<Address, string> Create(string region, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(region))
            return "Region can not be null";
        if (string.IsNullOrWhiteSpace(city))
            return "City can not be null";
        if (string.IsNullOrWhiteSpace(street))
            return "Street can not be null";
        return new Address(region, city, street);
    }
}