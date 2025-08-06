using AutoMapper;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.DTOs;
using HospitalManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PatientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            // Check if email already exists
            var existingPatient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == createPatientDto.Email);

            if (existingPatient != null)
            {
                throw new InvalidOperationException("A patient with this email already exists.");
            }

            var patient = _mapper.Map<Patient>(createPatientDto);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto?> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (patient == null)
                return null;

            // Check if email is being changed and if it already exists
            if (!string.IsNullOrEmpty(updatePatientDto.Email) && 
                updatePatientDto.Email != patient.Email)
            {
                var existingPatient = await _context.Patients
                    .FirstOrDefaultAsync(p => p.Email == updatePatientDto.Email && p.Id != id);

                if (existingPatient != null)
                {
                    throw new InvalidOperationException("A patient with this email already exists.");
                }
            }

            _mapper.Map(updatePatientDto, patient);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

            if (patient == null)
                return false;

            // Check if patient has any appointments
            var hasAppointments = await _context.Appointments
                .AnyAsync(a => a.PatientId == id && a.Status != "Cancelled");

            if (hasAppointments)
            {
                throw new InvalidOperationException("Cannot delete patient with active appointments.");
            }

            patient.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PatientDto>> SearchPatientsAsync(string searchTerm)
        {
            var patients = await _context.Patients
                .Where(p => p.IsActive && 
                           (p.Name.Contains(searchTerm) || 
                            p.Email.Contains(searchTerm) || 
                            p.PhoneNumber.Contains(searchTerm)))
                .OrderBy(p => p.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }

        public async Task<bool> PatientExistsAsync(int id)
        {
            return await _context.Patients
                .AnyAsync(p => p.Id == id && p.IsActive);
        }
    }
} 