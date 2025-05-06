namespace KanBagis.Application.DTOs;

public class UserByEmailDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string District { get; set; }
}