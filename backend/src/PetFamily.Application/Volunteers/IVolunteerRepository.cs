using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Volunteers;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByPhoneNumber( 
        string countryCode, 
        string phoneNumber,  
        CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByEmail(string email, CancellationToken cancellationToken);
}