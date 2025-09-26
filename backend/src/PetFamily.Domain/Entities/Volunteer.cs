using CSharpFunctionalExtensions;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Domain.Entities;

public class Volunteer: Entity<Guid>
{
    private readonly List<Pet> _pets = [];
    private readonly List<SocialMedia> _socialMedias = [];
    private readonly List<HelpRequisites> _helpRequisites = [];
    
    //ef core
    private Volunteer(Guid id) : base(id)
    {
    }
    
    private Volunteer(
        FullName fullName,
        Email email,
        string description,
        double workExperience,
        PhoneNumber phoneNumber)
    {
        Id=Guid.NewGuid();
        FullName=fullName;
        Email=email;
        Description=description;
        WorkExperience=workExperience;
        PhoneNumber=phoneNumber;
    }
    
    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public string Description { get; private set; }
    public double WorkExperience { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;
    public int AdoptedPetCount => _pets.Count(p => p.Status.Equals(PetStatus.Adopted));
    public int LookingForHomePetCount => _pets.Count(p => p.Status.Equals(PetStatus.LookingForHome));
    public int SickPetCount => _pets.Count(p => p.HealthStates.Contains(PetHealthState.Sick));
    public IReadOnlyList<HelpRequisites> HelpRequisites => _helpRequisites;
    public IReadOnlyList<SocialMedia> SocialMedias => _socialMedias;
    
    public static Result<Volunteer, Error> Create(
        FullName fullName,
        Email email,
        string description,
        double workExperience,
        PhoneNumber phoneNumber)
    {
        if (workExperience < 0)
            return Errors.General.ValueIsInvalid($"work experience {workExperience}");

        return new Volunteer(
            fullName,
            email, 
            description, 
            workExperience, 
            phoneNumber);
    }

    public Result AddPet(Pet pet)
    {
        if (pet == null)
            return Result.Failure("Pet cannot be null");

        _pets.Add(pet);
        return Result.Success();
    }

    public Result RemovePet(Pet pet)
    {
        if (pet == null)
            return Result.Failure("Pet cannot be null");

        _pets.Remove(pet);
        return Result.Success();
    }

    public Result AddSocialMedia(SocialMedia socialMedia)
    {
        if (socialMedia == null)
            return Result.Failure("Social media cannot be null");

        _socialMedias.Add(socialMedia);
        return Result.Success();
    }

    public Result RemoveSocialMedia(SocialMedia socialMedia)
    {
        if (socialMedia == null)
            return Result.Failure("Social media cannot be null");

        _socialMedias.Remove(socialMedia);
        return Result.Success();
    }
    
    public Result AddHelpRequisite(HelpRequisites helpRequisites)
    {
        if (_helpRequisites.Contains(helpRequisites))
            return Result.Failure("Help Requisites already exists");
            
        _helpRequisites.Add(helpRequisites);
        return Result.Success();
    }

    public Result RemoveHelpRequisite(HelpRequisites helpRequisites)
    {
        if (_helpRequisites.Contains(helpRequisites))
            return Result.Failure("Help Requisites do not exists");

        _helpRequisites.Remove(helpRequisites);
        return Result.Success();
    }
}