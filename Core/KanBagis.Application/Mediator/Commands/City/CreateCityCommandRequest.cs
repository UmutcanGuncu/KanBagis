using KanBagis.Application.Mediator.Results.City;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.City;

public class CreateCityCommandRequest : IRequest<CreateCityCommandResponse>
{
    public string Name { get; set; }
}