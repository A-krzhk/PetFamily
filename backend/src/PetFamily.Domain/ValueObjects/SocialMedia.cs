using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record SocialMedia
{
    //ef core
    private SocialMedia() { }
    
    private SocialMedia(string name, string url)
    {
        Name = name;
        Url = url;
    }
    
    public string Name { get; }
    public string Url { get; }

    public Result<SocialMedia, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Errors.General.ValueIsRequired(nameof(name));
        if (string.IsNullOrWhiteSpace(url))
            return Errors.General.ValueIsRequired(nameof(url));

        return new SocialMedia(name, url);
    }
}