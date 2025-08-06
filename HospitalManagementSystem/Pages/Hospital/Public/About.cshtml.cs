using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages.Hospital.Public
{
    [AllowAnonymous]
    public class AboutModel : PageModel
    {
        public void OnGet()
        {
            // Store public access in session
            HttpContext.Session.SetString("PublicAccessed", DateTime.Now.ToString());
        }
    }
} 