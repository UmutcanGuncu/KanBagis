using KanBagis.Application.Mediator.Results.Hospital;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.Hospital;

public class GetFilteredHospitalQuery : IRequest<List<GetFilteredHospitalQueryResult>>
{
    
    public string? City { get; set; }
    public string? District { get; set; }
}