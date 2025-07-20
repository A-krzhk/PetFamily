using CSharpFunctionalExtensions;

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

    public Result<BodyParams, string> Create(double weight, double height)
    {
        if  (weight <= 0)
            return "Weight can not be less than zero";
        if (height <= 0)
            return "Height can not be less than zero";
        return new BodyParams(weight, height);
    }
}