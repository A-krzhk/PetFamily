using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species;

public class PetSpecies: Entity<Guid>
{
    private readonly List<PetBreed> _breeds = [];

    private PetSpecies(string name)
    {
        Name = name;
    }
    
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<PetBreed> Breeds => _breeds;

    public Result<PetSpecies, string> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name can not be null or empty";

        return new PetSpecies(name);
    }

    public Result AddBreed(PetBreed petBreed)
    {
        if (petBreed == null)
            return Result.Failure("Breed can not be null");
        
        _breeds.Add(petBreed);
        
        return Result.Success();
    }

    public Result RemoveBreed(PetBreed petBreed)
    {
        if (petBreed == null)
            return Result.Failure("Breed can not be null");
        
        _breeds.Remove(petBreed);
        
        return Result.Success();
    }
}
