namespace KanBagis.Application.DTOs;

public class GetGroupByUserIdResultDto
{
    public Guid Id { get; set; }
    public Guid SupervisorId { get; set; }
    public string Name { get; set; }
}