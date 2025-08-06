using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Pages.Hospital
{
    [Authorize]
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalPatients { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalAppointments { get; set; }
        public int ActiveAppointments { get; set; }

        [TempData]
        public string? StatusMessage { get; set; }

        public string? CurrentUser { get; set; }
        public string? LastAccessed { get; set; }
        public string? SessionInfo { get; set; }

        public async Task OnGetAsync()
        {
            // Get session data
            CurrentUser = HttpContext.Session.GetString("CurrentUser");
            LastAccessed = HttpContext.Session.GetString("LastAccessed");
            
            // Store dashboard access in session
            HttpContext.Session.SetString("DashboardAccessed", DateTime.Now.ToString());
            
            // Create session info
            SessionInfo = $"Dashboard accessed at {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

            // Get statistics
            TotalPatients = await _context.Patients.CountAsync(p => p.IsActive);
            TotalDoctors = await _context.Doctors.CountAsync(d => d.IsActive);
            TotalAppointments = await _context.Appointments.CountAsync();
            ActiveAppointments = await _context.Appointments.CountAsync(a => a.Status == "Scheduled");

            // Set TempData for demonstration
            TempData["DashboardMessage"] = $"Welcome to Hospital Dashboard! Current time: {DateTime.Now:HH:mm:ss}";
        }
    }
} 