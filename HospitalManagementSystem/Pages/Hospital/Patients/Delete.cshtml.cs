using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Hospital.Patients
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        [TempData]
        public string? StatusMessage { get; set; }

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

            // Store delete session data
            HttpContext.Session.SetString("DeletePatientId", id.ToString() ?? "");
            HttpContext.Session.SetString("DeletePatientName", patient.Name);

            Patient = patient;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                // Check if patient has appointments
                var hasAppointments = await _context.Appointments
                    .AnyAsync(a => a.PatientId == id);

                if (hasAppointments)
                {
                    TempData["StatusMessage"] = $"Cannot delete patient '{patient.Name}' because they have existing appointments.";
                    return RedirectToPage("./Index");
                }

                // Soft delete - set IsActive to false
                patient.IsActive = false;
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = $"Patient '{patient.Name}' has been deleted successfully!";
            }

            return RedirectToPage("./Index");
        }
    }
} 