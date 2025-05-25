using WebApplication10.DTOs;

namespace WebApplication10.Services
{
    public interface IPrescriptionService
    {
        Task<string> AddPrescriptionAsync(CreatePrescriptionRequest request);
    }
}