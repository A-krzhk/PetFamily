using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record Address
{
    //ef core
    private Address() { }
    
    private Address(string region, string city, string street)
    {
        Region = region;
        City = city;
        Street = street;
    }
    public string Region { get; }
    public string City { get; }
    public string Street { get; }

    public Result<Address, Error> Create(string region, string city, string street)
    {
        if (string.IsNullOrWhiteSpace(region))
            return Errors.General.ValueIsRequired(nameof(region));
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired(nameof(city));
        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsRequired(nameof(street));
        return new Address(region, city, street);
    }
}