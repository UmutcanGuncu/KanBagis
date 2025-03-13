using KanBagis.Application.Mediator.Results.BloodDonation;
using MediatR;

namespace KanBagis.Application.Mediator.Queries.BloodDonation;

public class GetFilteredBloodDonationQuery :IRequest<IEnumerable<GetFilteredBloodDonationQueryResult>>
{
    public GetFilteredBloodDonationQuery(string city = null, string district= null, string hospitalName = null)
    {
        City = city;
        District = district;
        HospitalName = hospitalName;
    }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? HospitalName { get; set; }
}