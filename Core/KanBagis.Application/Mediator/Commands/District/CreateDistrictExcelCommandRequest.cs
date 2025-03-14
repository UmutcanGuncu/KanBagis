using KanBagis.Application.Mediator.Results.District;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KanBagis.Application.Mediator.Commands.District;

public class CreateDistrictExcelCommandRequest : IRequest<CreateDistrictExcelCommandResponse>
{
    public CreateDistrictExcelCommandRequest(IFormFile file)
    {
        File = file;
    }
    public   IFormFile File { get; set; }
}