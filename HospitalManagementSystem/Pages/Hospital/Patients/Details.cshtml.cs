using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Hospital.Patients
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Patient Patient { get; set; } = default!;
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            // Store view session data
            HttpContext.Session.SetString("ViewPatientId", id.ToString() ?? "");
            HttpContext.Session.SetString("ViewPatientName", patient.Name);

            // Get patient's appointments
            var appointments = await _context.Appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == id)
                .OrderByDescending(a => a.AppointmentDate)
                .ToListAsync();

            Patient = patient;
            Appointments = appointments;

            return Page();
        }
    }
} 