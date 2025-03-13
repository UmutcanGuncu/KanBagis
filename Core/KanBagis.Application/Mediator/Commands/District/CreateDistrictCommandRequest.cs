using KanBagis.Application.DTOs;
using KanBagis.Application.Mediator.Results.City;
using MediatR;

namespace KanBagis.Application.Mediator.Commands.District;

public class CreateDistrictCommandRequest : IRequest<CreateCityCommandResponse>
{
    public IEnumerable<AddDistrictDTO> Districts { get; set; }

}