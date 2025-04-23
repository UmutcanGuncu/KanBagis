namespace KanBagis.Application.Mediator.Results.UserInformation;

public class UpdateUserInformationCommandResponse
{
    public string UserId { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}