using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.BloodDonation;
using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.BloodDonation;

public class GetAllBloodDonationQueryHandler(IBloodDonationService _bloodDonationService) : IRequestHandler<GetAllBloodDonationQuery, IEnumerable<GetAllBloodDonationQueryResult>>
{
    public async Task<IEnumerable<GetAllBloodDonationQueryResult>> Handle(GetAllBloodDonationQuery request, CancellationToken cancellationToken)
    {
        var results = await  _bloodDonationService.GetPublicAllAsync();
        var resultDto=  results.Select(x => new GetAllBloodDonationQueryResult()
        {
            NameSurname = x.NameSurname,
            Phone = x.Phone,
            BloodGroup = x.BloodGroup,
            Age = x.Age,
            Gender = x.Gender,
            CityDistrict = x.CityDistrict,
            HospitalName = x.HospitalName,
            DonationStatus = x.DonationStatus,
            Description = x.Description,
            CreateDate = x.CreateDate
        }).ToList();
        return resultDto;
    }
}