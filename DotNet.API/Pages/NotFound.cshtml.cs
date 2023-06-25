using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNet.Presentation.Pages
{
    public class NotFoundModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }
    }
}