using BreakfastBuffet.Areas.Identity.Pages.Account;
using BreakfastBuffet.Data;
using BreakfastBuffet.Data.Model;
using BreakfastBuffet.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace BreakfastBuffet.Pages
{
    [Authorize("canAccessReception")]
    public class Reservation : PageModel
    {
        public IHubContext<MessageHub> _messageHub;

        public Reservation(IHubContext<MessageHub> messageHub)
        {
            _messageHub = messageHub;
        }
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
                if (!context.Reservations.Any(x => x.RoomNumber == Reservations.RoomNumber
                && x.ReservationDate == Reservations.ReservationDate))
                {
                    context.Reservations.Add(Reservations);
                    await context.SaveChangesAsync();
                    await _messageHub.Clients.All.SendAsync("ReceiveMessage");
                }
          }

          return LocalRedirect(returnUrl);
        }
  }
}
