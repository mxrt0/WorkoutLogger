using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorkoutLogger.Pages
{
    public class SuccessModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
