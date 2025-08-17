using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public record Email
{
    //ef core
    private Email() { }
    
    private Email(string email)
    {
        EmailAddress = email;
    }
    public string EmailAddress { get; }
    
    public Result<Email, string> Create(string email)
    {
        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        if (string.IsNullOrWhiteSpace(email))
            return "Email address cannot be null or empty";
        if (!Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase))
            return "Email format is invalid";
        return new Email(email);
    }
}