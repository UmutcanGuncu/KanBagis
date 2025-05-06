namespace KanBagis.Application.Mediator.Results.BloodDonation;

public class CreateBloodDonationCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid BloodDonationId { get; set; }
}