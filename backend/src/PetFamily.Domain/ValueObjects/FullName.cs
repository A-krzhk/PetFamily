using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName, string? middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName ?? string.Empty;
    }
    
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }

    public static Result<FullName, Error> Create(string firstName, string lastName, string? middleName)
    {
        if  (string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsRequired("first name");
        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsRequired("last name");
        return new FullName(firstName, lastName, middleName);
    }
}
