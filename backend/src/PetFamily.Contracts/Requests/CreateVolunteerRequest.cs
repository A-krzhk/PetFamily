using PetFamily.Contracts.DTOs;

namespace PetFamily.Contracts.Requests;

public record CreateVolunteerRequest(
    FullNameDTO FullName,
    string Email,
    string Description,
    double WorkExperience,
    PhoneNumberDTO PhoneNumber, 
    IEnumerable<HelpRequisitesDTO> HelpRequisites,
    IEnumerable<SocialMediaDTO> SocialMedias
    );