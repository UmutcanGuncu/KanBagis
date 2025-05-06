namespace KanBagis.Application.Mediator.Results.Group;

public class GetGroupBySupervisorIdResult
{
    public Guid GroupId { get; set; }
    public Guid SupervisorId { get; set; }
    public string Name { get; set; }
}