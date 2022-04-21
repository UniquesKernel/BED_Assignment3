using BreakfastBuffet.Areas.Identity.Pages.Account;
using BreakfastBuffet.Data;
using BreakfastBuffet.Data.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Pages
{
    // [Authorize("canAccessReception")]
    public class Reservation : PageModel
    {
      public ReservationModel Reservations { get; set; }
      public InputModel Input { get; set; }
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
          returnUrl ??= Url.Content("~/");
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
            context.SaveChanges();
          }

          return LocalRedirect(returnUrl);
        }
  }
}
