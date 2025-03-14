using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.Hospital;
using KanBagis.Application.Mediator.Results.Hospital;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.Hospital;

public class CreateHospitalsCommandRequestHandler(IHospitalService _hospitalService) : IRequestHandler<CreateHospitalsCommandRequest,CreateHospitalsCommandResult>
{
    public async Task<CreateHospitalsCommandResult> Handle(CreateHospitalsCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _hospitalService.AddHospitalsAsync(request.File);
        return new()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}