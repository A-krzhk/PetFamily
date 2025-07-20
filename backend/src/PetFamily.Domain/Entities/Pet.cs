using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Species;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

public class Pet: Entity<Guid>
{
    private readonly List<PetHealthState> _healthStates = [];
    private readonly List<Vaccine> _vaccines = [];
    
    //ef core
    private Pet(Guid id): base(id)
    {
    }
    
    private Pet(
        string name,
        string description,
        PetType petType,
        string color,
        Address address,
        BodyParams bodyParams,
        PhoneNumber volunteerPhoneNumber,
        bool castrated,
        DateTime birthDate,
        HelpRequisites helpRequisites,
        PetStatus status)
    {
        Name = name;
        Description = description;
        PetType = petType;
        Color = color;
        Address = address;
        BodyParams = bodyParams;
        VolunteerPhoneNumber = volunteerPhoneNumber;
        Castrated = castrated;
        BirthDate = birthDate;
        HelpRequisites = helpRequisites;
        Created = DateTime.UtcNow;
        Status = status;
    }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public PetType PetType { get; private set; }
    public string Color { get; private set; }
    public IReadOnlyList<PetHealthState> HealthStates => _healthStates;
    public IReadOnlyList<Vaccine>  Vaccines => _vaccines;
    public Address Address { get; private set; }
    public BodyParams BodyParams { get; private set; }
    public PhoneNumber VolunteerPhoneNumber { get; private set; }
    public bool Castrated { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool Vaccinated => _vaccines.Count != 0;
    public PetStatus Status { get; private set; }
    public DateTime Created { get; private set; }
    public HelpRequisites HelpRequisites { get; private set; }
    
    public Result AddHealthState(PetHealthState state)
    {
        if (state == default)
            return Result.Failure("Health state cannot be null");

        _healthStates.Add(state);
        return Result.Success();
    }

    public Result RemoveHealthState(PetHealthState state)
    {
        if (state == default)
            return Result.Failure("Health state cannot be null");

        _healthStates.Remove(state);
        return Result.Success();
    }
    
    public Result AddVaccine(Vaccine vaccine)
    {
        if (vaccine == null)
            return Result.Failure("Vaccine can not be null");

        _vaccines.Add(vaccine);
        return Result.Success();
    }

    public Result RemoveVaccine(Vaccine vaccine)
    {
        if (vaccine == null)
            return Result.Failure("Vaccine can not be null");

        _vaccines.Remove(vaccine);
        return Result.Success();
    }
}