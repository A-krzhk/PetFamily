using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record PetType
{
    //ef core
    private PetType() { }
    
    private PetType(Guid breedId, Guid speciesId)
    {
        BreedId = breedId;
        SpeciesId = speciesId;
    }
    
    public Guid BreedId { get; }
    public Guid SpeciesId { get; }

    public Result<PetType, Error> Create(Guid breedId, Guid speciesId)
    {
        if (breedId == null)
            return Errors.General.ValueIsRequired("breed id");
        if (speciesId == null)
            return Errors.General.ValueIsRequired("species Id");

        return new PetType(breedId, speciesId);
    }
}