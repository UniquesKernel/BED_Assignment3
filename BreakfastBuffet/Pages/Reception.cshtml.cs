using BreakfastBuffet.Areas.Identity.Pages.Account;
using BreakfastBuffet.Data;
using BreakfastBuffet.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Pages
{
    [Authorize("canAccessReception")]
    public class Reservation : PageModel
    {
      public ReservationModel Reservations { get; set; }
      [BindProperty]
      public InputModel Input { get; set; } = new InputModel();
      public class InputModel
      {
        public int RoomNumber { get; set; }
        public int ReservationsAdult { get; set; }
        public int ReservationsChild { get; set; }
        public DateTime ReservationDate { get; set; }
      }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
          returnUrl ??= Url.Content("~/Reception");
          Reservations = new ReservationModel
          {
            ReservationsAdult = Input.ReservationsAdult,
            ReservationsChild = Input.ReservationsChild,
            ReservationDate = Input.ReservationDate,
            RoomNumber = Input.RoomNumber
          };

          DbContextOptions<ApplicationDbContext> option = new DbContextOptions<ApplicationDbContext>();
          using (var context = new ApplicationDbContext(option))
          {
            context.Reservations.Add(Reservations);
            await context.SaveChangesAsync();
          }

          return LocalRedirect(returnUrl);
        }
  }
}
