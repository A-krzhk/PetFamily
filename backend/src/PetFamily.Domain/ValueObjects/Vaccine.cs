using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record Vaccine
{
    //ef core
    private Vaccine() { }
    
    private Vaccine(string name, string description, DateTime date)
    {
        Name = name;
        Description = description;
        Date = date;
    }
    
    public string Name { get; }
    public string Description { get; }
    public DateTime Date { get; }

    public Result<Vaccine, Error> Create(string name, string description, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(name))
            Errors.General.ValueIsRequired(nameof(name));
        if (string.IsNullOrWhiteSpace(description))
            Errors.General.ValueIsRequired(nameof(description));
        if (date == default)
            Errors.General.ValueIsRequired(nameof(date));

        return new Vaccine(name, description, date);
    }
}