namespace KanBagis.Application.DTOs;

public class LoginDTO
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public DTOs.Token Token { get; set; }
}