namespace KanBagis.Application.DTOs;

public class GetGroupBySupervisorIdResultDto
{
    public Guid Id { get; set; }
    public Guid SupervisorId { get; set; }
    public string Name { get; set; }
}