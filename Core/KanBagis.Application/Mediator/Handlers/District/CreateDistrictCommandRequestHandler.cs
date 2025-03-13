using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.City;
using KanBagis.Application.Mediator.Commands.District;
using KanBagis.Application.Mediator.Results.City;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.District;

public class CreateDistrictCommandRequestHandler(IDistrictService _districtService) : IRequestHandler<CreateDistrictCommandRequest, CreateCityCommandResponse>
{
    public async Task<CreateCityCommandResponse> Handle(CreateDistrictCommandRequest request, CancellationToken cancellationToken)
    {
        var value =  await _districtService.AddDistrictsAsync(request.Districts);
        return new()
        {
            Message = value.Message,
            Success = value.Success
        };
    }
}