namespace KanBagis.Application.DTOs;

public class CreateGroupResultDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public Guid GroupId { get; set; }
}