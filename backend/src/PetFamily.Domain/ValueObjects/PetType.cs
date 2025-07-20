using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public record PetType
{
    private PetType(Guid breedId, Guid speciesId)
    {
        BreedId = breedId;
        SpeciesId = speciesId;
    }
    
    public Guid BreedId { get; }
    public Guid SpeciesId { get; }

    public Result<PetType, string> Create(Guid breedId, Guid speciesId)
    {
        if (breedId == null)
            return "Breed Id cannot be null";
        if (speciesId == null)
            return "Species Id cannot be null";

        return new PetType(breedId, speciesId);
    }
}