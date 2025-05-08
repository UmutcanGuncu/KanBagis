namespace KanBagis.Application.Mediator.Results.BloodDonations;

public class GetGroupBySupervisorIdResult
{
    public Guid GroupId { get; set; }
    public Guid SupervisorId { get; set; }
    public string Name { get; set; }
}