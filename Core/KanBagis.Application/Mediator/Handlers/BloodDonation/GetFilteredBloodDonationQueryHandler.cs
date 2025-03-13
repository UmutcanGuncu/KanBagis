using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.BloodDonation;
using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.BloodDonation;

public class GetFilteredBloodDonationQueryHandler(IBloodDonationService _bloodDonationService) : IRequestHandler<GetFilteredBloodDonationQuery, IEnumerable<GetFilteredBloodDonationQueryResult>>
{
    public async Task<IEnumerable<GetFilteredBloodDonationQueryResult>> Handle(GetFilteredBloodDonationQuery request, CancellationToken cancellationToken)
    {
        var values = await _bloodDonationService.GetFiltederAsync(request.City, request.District, request.HospitalName);
        var resultDto = values.Select(x=> new GetFilteredBloodDonationQueryResult()
        {
            NameSurname = x.NameSurname,
            Phone = x.Phone,
            BloodGroup = x.BloodGroup,
            Age = x.Age,
            Gender = x.Gender,
            CityDistrict = x.CityDistrict,
            HospitalName = x.HospitalName,
            DonationStatus = x.DonationStatus,
            CreateDate = x.CreateDate
        }).ToList();
        return resultDto;
    }
}