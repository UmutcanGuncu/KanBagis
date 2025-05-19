namespace KanBagis.Application.Mediator.Results.BloodDonations;

public class CreateGroupCommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid GroupId { get; set; }
}