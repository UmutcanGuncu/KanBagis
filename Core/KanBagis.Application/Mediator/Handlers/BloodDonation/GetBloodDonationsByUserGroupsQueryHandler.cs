using KanBagis.Application.Abstactions.Services;
using KanBagis.Application.Mediator.Queries.Group;
using KanBagis.Application.Mediator.Results.BloodDonations;
using MediatR;

namespace KanBagis.Application.Mediator.Handlers.BloodDonation;

public class GetBloodDonationsByUserGroupsQueryHandler(IBloodDonationService _donationService) : IRequestHandler<GetBloodDonationsByUserGroupsQuery, IEnumerable<GetBloodDonationsByUserGroupsResult>>
{
    public async Task<IEnumerable<GetBloodDonationsByUserGroupsResult>> Handle(GetBloodDonationsByUserGroupsQuery request, CancellationToken cancellationToken)
    {
        var donationList = await _donationService.GetBloodDonationsByUserGroupsAsync(request.UserId);
        return donationList.Select(x=> new GetBloodDonationsByUserGroupsResult()
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
            CreateDate = x.CreateDate,
            GroupName = x.GroupName
        });
    }
}