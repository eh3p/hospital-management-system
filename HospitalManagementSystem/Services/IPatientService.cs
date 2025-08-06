using HospitalManagementSystem.DTOs;

namespace HospitalManagementSystem.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto);
        Task<PatientDto?> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto);
        Task<bool> DeletePatientAsync(int id);
        Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm);
        Task<bool> PatientExistsAsync(int id);
    }
} 