namespace KanBagis.Application.DTOs;

public class CreateGroupDto
{
    public string Name { get; set; }
    public Guid SupervisorId { get; set; }
}