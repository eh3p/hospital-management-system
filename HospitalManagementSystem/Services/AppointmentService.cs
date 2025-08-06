using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.DTOs;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .OrderByDescending(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            // Validate patient exists
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == createAppointmentDto.PatientId && p.IsActive);

            if (patient == null)
            {
                throw new InvalidOperationException("Patient not found or inactive.");
            }

            // Validate doctor exists and is available
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == createAppointmentDto.DoctorId && d.IsActive && d.IsAvailable);

            if (doctor == null)
            {
                throw new InvalidOperationException("Doctor not found, inactive, or unavailable.");
            }

            // Check if appointment date is in the future
            if (createAppointmentDto.AppointmentDate.Date < DateTime.Today)
            {
                throw new InvalidOperationException("Appointment date cannot be in the past.");
            }

            // Check if time slot is available
            var isAvailable = await IsTimeSlotAvailableAsync(
                createAppointmentDto.DoctorId,
                createAppointmentDto.AppointmentDate,
                createAppointmentDto.AppointmentTime);

            if (!isAvailable)
            {
                throw new InvalidOperationException("The selected time slot is not available.");
            }

            var appointment = _mapper.Map<Appointment>(createAppointmentDto);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Reload with includes for DTO mapping
            var createdAppointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == appointment.Id);

            return _mapper.Map<AppointmentDto>(createdAppointment);
        }

        public async Task<AppointmentDto?> UpdateAppointmentAsync(int id, UpdateAppointmentDto updateAppointmentDto)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
                return null;

            // Validate patient if being changed
            if (updateAppointmentDto.PatientId.HasValue)
            {
                var patient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Id == updateAppointmentDto.PatientId.Value && p.IsActive);

                if (patient == null)
                {
                    throw new InvalidOperationException("Patient not found or inactive.");
                }
            }

            // Validate doctor if being changed
            if (updateAppointmentDto.DoctorId.HasValue)
            {
                var doctor = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.Id == updateAppointmentDto.DoctorId.Value && d.IsActive && d.IsAvailable);

                if (doctor == null)
                {
                    throw new InvalidOperationException("Doctor not found, inactive, or unavailable.");
                }
            }

            // Check time slot availability if date or time is being changed
            if (updateAppointmentDto.AppointmentDate.HasValue || updateAppointmentDto.AppointmentTime.HasValue)
            {
                var appointmentDate = updateAppointmentDto.AppointmentDate ?? appointment.AppointmentDate;
                var appointmentTime = updateAppointmentDto.AppointmentTime ?? appointment.AppointmentTime;

                if (appointmentDate.Date < DateTime.Today)
                {
                    throw new InvalidOperationException("Appointment date cannot be in the past.");
                }

                var isAvailable = await IsTimeSlotAvailableAsync(
                    updateAppointmentDto.DoctorId ?? appointment.DoctorId,
                    appointmentDate,
                    appointmentTime,
                    id);

                if (!isAvailable)
                {
                    throw new InvalidOperationException("The selected time slot is not available.");
                }
            }

            _mapper.Map(updateAppointmentDto, appointment);
            await _context.SaveChangesAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
                return false;

            // Only allow deletion of future appointments
            if (appointment.AppointmentDate.Date <= DateTime.Today && 
                appointment.AppointmentTime <= DateTime.Now.TimeOfDay)
            {
                throw new InvalidOperationException("Cannot delete past or current appointments.");
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId)
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId)
        {
            var appointments = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.DoctorId == doctorId)
                .OrderByDescending(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentDto>> SearchAppointmentsAsync(AppointmentSearchDto searchDto)
        {
            var query = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable();

            if (searchDto.PatientId.HasValue)
            {
                query = query.Where(a => a.PatientId == searchDto.PatientId.Value);
            }

            if (searchDto.DoctorId.HasValue)
            {
                query = query.Where(a => a.DoctorId == searchDto.DoctorId.Value);
            }

            if (searchDto.FromDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate >= searchDto.FromDate.Value);
            }

            if (searchDto.ToDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate <= searchDto.ToDate.Value);
            }

            if (!string.IsNullOrEmpty(searchDto.Status))
            {
                query = query.Where(a => a.Status == searchDto.Status);
            }

            if (!string.IsNullOrEmpty(searchDto.AppointmentType))
            {
                query = query.Where(a => a.AppointmentType == searchDto.AppointmentType);
            }

            var appointments = await query
                .OrderByDescending(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<bool> AppointmentExistsAsync(int id)
        {
            return await _context.Appointments.AnyAsync(a => a.Id == id);
        }

        public async Task<bool> IsTimeSlotAvailableAsync(int doctorId, DateTime appointmentDate, TimeSpan appointmentTime, int? excludeAppointmentId = null)
        {
            var query = _context.Appointments
                .Where(a => a.DoctorId == doctorId &&
                           a.AppointmentDate == appointmentDate &&
                           a.Status != "Cancelled");

            if (excludeAppointmentId.HasValue)
            {
                query = query.Where(a => a.Id != excludeAppointmentId.Value);
            }

            // Check for exact time slot conflict
            var conflictingAppointment = await query
                .FirstOrDefaultAsync(a => a.AppointmentTime == appointmentTime);

            if (conflictingAppointment != null)
            {
                return false;
            }

            // Check for overlapping time slots (assuming 30-minute appointments)
            var appointmentEndTime = appointmentTime.Add(TimeSpan.FromMinutes(30));
            var overlappingAppointment = await query
                .FirstOrDefaultAsync(a => 
                    (a.AppointmentTime < appointmentEndTime && 
                     a.AppointmentTime.Add(TimeSpan.FromMinutes(30)) > appointmentTime));

            return overlappingAppointment == null;
        }
    }
} 