using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreakfastBuffet.Pages
{
    [Authorize("canAccessCheckIn")]
    public class CheckInModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
