using HospitalManagementSystem.DTOs;

namespace HospitalManagementSystem.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
        Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task<AppointmentDto?> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointmentDto);
        Task<bool> DeleteAppointmentAsync(int id);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId);
        Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId);
        Task<IEnumerable<AppointmentDto>> SearchAppointmentsAsync(AppointmentSearchDto searchDto);
        Task<bool> AppointmentExistsAsync(int id);
        Task<bool> IsTimeSlotAvailableAsync(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime, int? excludeAppointmentId = null);
    }
} 