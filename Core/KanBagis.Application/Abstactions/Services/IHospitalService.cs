using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;

namespace KanBagis.Application.Abstactions.Services;

public interface IHospitalService
{
    public Task<IEnumerable<HospitalDTO>> GetFilteredHospitalsAsync(string city = null, string district = null);
}