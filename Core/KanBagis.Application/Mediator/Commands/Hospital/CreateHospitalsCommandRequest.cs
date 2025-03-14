using KanBagis.Application.Mediator.Results.Hospital;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KanBagis.Application.Mediator.Commands.Hospital;

public class CreateHospitalsCommandRequest : IRequest<CreateHospitalsCommandResult>
{
    public CreateHospitalsCommandRequest(IFormFile file)
    {
        File = file;
    }
    public IFormFile File { get; set; }
}