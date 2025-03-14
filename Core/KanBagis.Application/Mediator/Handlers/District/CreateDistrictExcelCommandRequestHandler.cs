using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.District;
using KanBagis.Application.Mediator.Results.District;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.District;

public class CreateDistrictExcelCommandRequestHandler(IDistrictService _districtService) : IRequestHandler<CreateDistrictExcelCommandRequest, CreateDistrictExcelCommandResponse>
{
    public async Task<CreateDistrictExcelCommandResponse> Handle(CreateDistrictExcelCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _districtService.AddDistirctsWithExcelAsync(request.File);
        return new()
        {
            Success = result.Success,
            Message = result.Message,
        };
    }
}