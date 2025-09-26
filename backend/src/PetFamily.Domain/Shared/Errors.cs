using PetFamily.Domain.Enums;

namespace PetFamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "value";
            return Error.Validation("value.is.invalid", $"{label} is invalid");
        }
        
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? string.Empty : $" for id: {id}";
            return Error.NotFound("record.not.found", $"record not found{forId}");
        }
        
        public static Error NotFound(string? name)
        {
            var label = name == null ? string.Empty : $" with property: {name}";
            return Error.NotFound("record.not.found", $"record not found{label}");
        }
        
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name ?? "value"; 
            return Error.Validation("length.is.invalid", $"invalid length {label}");
        }
        
        public static Error ValueAlreadyExist(string? name = null)
        {
            var label = name ?? "value"; 
            return Error.Conflict("record.already.exists", $"{label} already exists");
        }
    }
}