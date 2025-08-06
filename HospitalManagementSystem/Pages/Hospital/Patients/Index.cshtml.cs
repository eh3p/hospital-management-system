using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Hospital.Patients
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Patient> Patients { get; set; } = default!;
        
        [TempData]
        public string? StatusMessage { get; set; }

        public async Task OnGetAsync()
        {
            // Get session data
            var sessionData = HttpContext.Session.GetString("LastAccessed");
            if (sessionData == null)
            {
                HttpContext.Session.SetString("LastAccessed", DateTime.Now.ToString());
            }

            // Store current user info in session
            HttpContext.Session.SetString("CurrentUser", User.Identity?.Name ?? "Unknown");

            Patients = await _context.Patients
                .Where(p => p.IsActive)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
} 