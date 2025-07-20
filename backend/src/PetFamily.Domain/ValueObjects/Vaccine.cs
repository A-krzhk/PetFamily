using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;

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

    public Result<Vaccine, string> Create(string name, string description, DateTime date)
    {
        if (string.IsNullOrWhiteSpace(Name))
            return "Name can not be empty or null";
        if (string.IsNullOrWhiteSpace(Description))
            return "Description can not be empty or null";
        if (Date == default)
            return "Date can not be empty";

        return new Vaccine(name, description, date);
    }
}