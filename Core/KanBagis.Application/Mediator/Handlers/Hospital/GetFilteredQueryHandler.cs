using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.Hospital;
using KanBagis.Application.Mediator.Results.Hospital;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.Hospital;

public class GetFilteredQueryHandler : IRequestHandler<GetFilteredHospitalQuery, List<GetFilteredHospitalQueryResult>>
{
    private readonly IHospitalService _hospitalService;

    public GetFilteredQueryHandler(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    public async Task<List<GetFilteredHospitalQueryResult>> Handle(GetFilteredHospitalQuery request, CancellationToken cancellationToken)
    {
        var result = await _hospitalService.GetFilteredHospitalsAsync(request.City, request.District);
        var filteredHospitals =  result.Select(x=>new GetFilteredHospitalQueryResult()
        {
            Id = x.Id,
            Name = x.Name,
            Location = x.Location
        }).ToList();
        return filteredHospitals;
    }
}