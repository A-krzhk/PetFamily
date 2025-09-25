using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record BodyParams
{
    //ef core
    private BodyParams() { }
    
    private BodyParams(double weight, double height)
    {
        Weight = weight;
        Height = height;
    }
    
    public double Weight { get; }
    public double Height { get; }

    public Result<BodyParams, Error> Create(double weight, double height)
    {
        if  (weight <= 0)
            return Errors.General.ValueIsInvalid(nameof(weight));
        if (height <= 0)
            return Errors.General.ValueIsInvalid(nameof(height));
        return new BodyParams(weight, height);
    }
}