using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species;

public class PetBreed : Entity<Guid>
{
    private PetBreed(string name)
    {
        Name = name;
    }
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public Result<PetBreed, string> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Breed name can not be empty";

        return new PetBreed(name);
    }
}