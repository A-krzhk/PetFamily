using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public record FullName
{
    private FullName(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
    
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }

    public Result<FullName, string> Create(string firstName, string lastName, string middleName)
    {
        if  (string.IsNullOrWhiteSpace(firstName))
            return "First name cannot be empty";
        if (string.IsNullOrWhiteSpace(lastName))
            return "Last name cannot be empty";
        return new FullName(firstName, lastName, middleName);
    }
}
