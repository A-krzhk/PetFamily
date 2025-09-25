namespace PetFamily.Contracts.DTOs;

public record HelpRequisitesDTO(    
    string Recipient,
    string TIN,
    string TRRC,
    string RCRN,
    string RCBIC,
    string Acc,
    string CorrAcc);