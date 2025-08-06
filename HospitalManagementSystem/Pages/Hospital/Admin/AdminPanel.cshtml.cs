using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Hospital.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelModel : PageModel
    {
        public void OnGet()
        {
            // Store admin access in session
            HttpContext.Session.SetString("AdminAccessed", DateTime.Now.ToString());
        }
    }
} 