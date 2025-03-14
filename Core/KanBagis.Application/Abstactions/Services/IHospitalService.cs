using KanBagis.Application.DTOs;
using KanBagis.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace KanBagis.Application.Abstactions.Services;

public interface IHospitalService
{
    public Task<IEnumerable<HospitalDTO>> GetFilteredHospitalsAsync(string city = null, string district = null);
    public Task<AddHospitalsResultDTO> AddHospitalsAsync(IFormFile file);
}