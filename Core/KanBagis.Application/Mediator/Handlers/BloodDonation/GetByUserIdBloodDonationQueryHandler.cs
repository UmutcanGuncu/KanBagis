using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.BloodDonation;
using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.BloodDonation;

public class GetByUserIdBloodDonationQueryHandler(IBloodDonationService _bloodDonationService): IRequestHandler<GetByUserIdBloodDonationQuery, IEnumerable<GetByUserIdBloodDonationQueryResult>>
{
    public async Task<IEnumerable<GetByUserIdBloodDonationQueryResult>> Handle(GetByUserIdBloodDonationQuery request, CancellationToken cancellationToken)
    {
        var values =await _bloodDonationService.GetByUserIdAsync(request.UserId);
        var resultDto =  values.Select(x => new GetByUserIdBloodDonationQueryResult()
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