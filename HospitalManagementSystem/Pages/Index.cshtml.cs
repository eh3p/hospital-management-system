using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // Store home page access in session
            HttpContext.Session.SetString("HomeAccessed", DateTime.Now.ToString());
        }
    }
}
