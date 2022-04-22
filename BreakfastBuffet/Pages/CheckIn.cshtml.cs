using BreakfastBuffet.Data;
using BreakfastBuffet.Data.Model;
using BreakfastBuffet.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Pages
{
    [Authorize("canAccessCheckIn")]
    public class CheckInModel : PageModel
    {
      public ReservationModel Reservations { get; set; }
      [BindProperty]
      public InputModel Input { get; set; } = new InputModel();
      public class InputModel
      {
        public int RoomNumber { get; set; }
        public int CheckedInAdult { get; set; }
        public int CheckedInChildren { get; set; }
      }

        public IHubContext<MessageHub> _messageHub;

        public CheckInModel(IHubContext<MessageHub> messageHub)
        {
            _messageHub = messageHub;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
          returnUrl ??= Url.Content("~/CheckIn");

          DbContextOptions<ApplicationDbContext> option = new DbContextOptions<ApplicationDbContext>();
          using (var context = new ApplicationDbContext(option))
          {
                if (context.Reservations.Any(r => r.RoomNumber == Input.RoomNumber && r.ReservationDate == DateTime.Now.Date))
                {
                    ReservationModel reservation = await context.Reservations.FirstAsync(r => r.RoomNumber == Input.RoomNumber && r.ReservationDate == DateTime.Now.Date);

                    if (reservation.ReservationsAdult <= Input.CheckedInAdult && reservation.ReservationsChild <= Input.CheckedInChildren)
                    { 
                    reservation.AttendingAdults = Input.CheckedInAdult;
                    reservation.AttendingChildren = Input.CheckedInChildren;

                    await context.SaveChangesAsync();
                    await _messageHub.Clients.All.SendAsync("ReceiveMessage");
                    }
                }
            
          }

          return LocalRedirect(returnUrl);
        }
    }
}
