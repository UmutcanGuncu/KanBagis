namespace KanBagis.Application.DTOs;

public class BloodDonationDTO
{
    public bool CurrentUser {get;set;}
    public string? Name {get;set;}
    public string? Surname {get;set;}
    public string? PhoneNumber {get;set;}
    public string? Email {get;set;}
    public string? BloodGroup {get;set;}
    public string? Age {get;set;}
    public string? Gender {get;set;}
    public string Description { get; set; }
    public Guid HospitalId {get;set;}
    public Guid AppUserId {get;set;}
}