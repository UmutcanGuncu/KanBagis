using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Commands.City;
using KanBagis.Application.Mediator.Results.City;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.City;

public class CreateCityCommandRequestHandler(ICityService _cityService) : IRequestHandler<CreateCityCommandRequest,CreateCityCommandResponse>
{
    public async Task<CreateCityCommandResponse> Handle(CreateCityCommandRequest request, CancellationToken cancellationToken)
    {
       var values = await _cityService.AddCityAsync(new (){ Name = request.Name });
       return new CreateCityCommandResponse()
       {
            Success = values.Success,
            Message = values.Message
       };
    }
}