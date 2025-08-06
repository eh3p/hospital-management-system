using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Hospital.Patients
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        [TempData]
        public string? StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            // Store form access in session
            HttpContext.Session.SetString("FormAccessed", DateTime.Now.ToString());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate ModelState
            if (!ModelState.IsValid)
            {
                // Store validation errors in session
                var errors = string.Join(", ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                HttpContext.Session.SetString("ValidationErrors", errors);
                return Page();
            }

            // Check if email already exists
            var existingPatient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Email == Patient.Email);
            
            if (existingPatient != null)
            {
                ModelState.AddModelError("Patient.Email", "A patient with this email already exists.");
                return Page();
            }

            // Set registration date
            Patient.RegistrationDate = DateTime.Now;
            Patient.IsActive = true;

            // Store patient data in session for confirmation
            HttpContext.Session.SetString("NewPatientName", Patient.Name);
            HttpContext.Session.SetString("NewPatientEmail", Patient.Email);

            _context.Patients.Add(Patient);
            await _context.SaveChangesAsync();

            // Set TempData message
            TempData["StatusMessage"] = $"Patient '{Patient.Name}' has been created successfully!";

            return RedirectToPage("./Index");
        }
    }
} 