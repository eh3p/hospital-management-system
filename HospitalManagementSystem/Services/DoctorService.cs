using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.DTOs;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DoctorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await _context.Doctors
                .Where(d => d.IsActive)
                .OrderBy(d => d.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }

        public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);

            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto createDoctorDto)
        {
            // Check if email already exists
            var existingDoctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Email == createDoctorDto.Email);

            if (existingDoctor != null)
            {
                throw new InvalidOperationException("A doctor with this email already exists.");
            }

            // Check if license number already exists
            var existingLicense = await _context.Doctors
                .FirstOrDefaultAsync(d => d.LicenseNumber == createDoctorDto.LicenseNumber);

            if (existingLicense != null)
            {
                throw new InvalidOperationException("A doctor with this license number already exists.");
            }

            var doctor = _mapper.Map<Doctor>(createDoctorDto);
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<DoctorDto?> UpdateDoctorAsync(int id, UpdateDoctorDto updateDoctorDto)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);

            if (doctor == null)
                return null;

            // Check if email is being changed and if it already exists
            if (!string.IsNullOrEmpty(updateDoctorDto.Email) && 
                updateDoctorDto.Email != doctor.Email)
            {
                var existingDoctor = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.Email == updateDoctorDto.Email && d.Id != id);

                if (existingDoctor != null)
                {
                    throw new InvalidOperationException("A doctor with this email already exists.");
                }
            }

            // Check if license number is being changed and if it already exists
            if (!string.IsNullOrEmpty(updateDoctorDto.LicenseNumber) && 
                updateDoctorDto.LicenseNumber != doctor.LicenseNumber)
            {
                var existingLicense = await _context.Doctors
                    .FirstOrDefaultAsync(d => d.LicenseNumber == updateDoctorDto.LicenseNumber && d.Id != id);

                if (existingLicense != null)
                {
                    throw new InvalidOperationException("A doctor with this license number already exists.");
                }
            }

            _mapper.Map(updateDoctorDto, doctor);
            await _context.SaveChangesAsync();

            return _mapper.Map<DoctorDto>(doctor);
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.Id == id && d.IsActive);

            if (doctor == null)
                return false;

            // Check if doctor has any appointments
            var hasAppointments = await _context.Appointments
                .AnyAsync(a => a.DoctorId == id && a.Status != "Cancelled");

            if (hasAppointments)
            {
                throw new InvalidOperationException("Cannot delete doctor with active appointments.");
            }

            doctor.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DoctorDto>> SearchDoctorsAsync(string searchTerm)
        {
            var doctors = await _context.Doctors
                .Where(d => d.IsActive && 
                           (d.Name.Contains(searchTerm) || 
                            d.Email.Contains(searchTerm) || 
                            d.PhoneNumber.Contains(searchTerm) ||
                            d.Specialization.Contains(searchTerm)))
                .OrderBy(d => d.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsBySpecializationAsync(string specialization)
        {
            var doctors = await _context.Doctors
                .Where(d => d.IsActive && d.Specialization.Contains(specialization))
                .OrderBy(d => d.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
        }

        public async Task<bool> DoctorExistsAsync(int id)
        {
            return await _context.Doctors
                .AnyAsync(d => d.Id == id && d.IsActive);
        }
    }
} 