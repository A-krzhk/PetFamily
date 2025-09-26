using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerRepository(ApplicationDbContext context) : IVolunteerRepository
{
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken)
    {
        await context.Volunteers.AddAsync(volunteer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return volunteer.Id;
    }
    
    public async Task<Result<Volunteer, Error>> GetById(Guid volunteerId, CancellationToken cancellationToken)
    {
       var volunteer =  await context
            .Volunteers
            .Include(v => v.SocialMedias)
            .Include(v => v.HelpRequisites)
            .FirstOrDefaultAsync(v => v.Id == volunteerId, cancellationToken);
       if (volunteer is null)
           return Errors.General.NotFound(volunteerId);
       
       return volunteer;
    }
    
    public async Task<Result<Volunteer, Error>> GetByPhoneNumber(
        string countryCode, 
        string phoneNumber, 
        CancellationToken cancellationToken)
    {
        var volunteer =  await context
            .Volunteers
            .Include(v => v.SocialMedias)
            .Include(v => v.HelpRequisites)
            .FirstOrDefaultAsync(v => 
                    v.PhoneNumber.CountryCode == countryCode && v.PhoneNumber.Number == phoneNumber, 
                    cancellationToken);
        if (volunteer is null)
            return Errors.General.NotFound($"{countryCode}{phoneNumber}");
       
        return volunteer;
    }
    
    public async Task<Result<Volunteer, Error>> GetByEmail(string email, CancellationToken cancellationToken)
    {
        var volunteer =  await context
            .Volunteers
            .Include(v => v.SocialMedias)
            .Include(v => v.HelpRequisites)
            .FirstOrDefaultAsync(v => v.Email.EmailAddress == email, cancellationToken);
        if (volunteer is null)
            return Errors.General.NotFound(email);
       
        return volunteer;
    }
    
}