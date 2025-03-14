using KanBagis.Domain.Entities;

namespace KanBagis.Application.DTOs;

public class AddHospitalsResultDTO
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<HospitalDTO> Hospitals { get; set; }
}