using WebApplication10.DTOs;

namespace WebApplication10.Services
{
    public interface IPatientService
    {
        Task<PatientDto?> GetPatientAsync(int id);
    }
}