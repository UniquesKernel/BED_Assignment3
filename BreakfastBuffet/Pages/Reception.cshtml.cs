using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreakfastBuffet.Pages
{
    // [Authorize("canAccessReception")]
    public class Reservation : PageModel
    {
        public void OnGet()
        {

        }
    }
}
