using BreakfastBuffet.Data;
using BreakfastBuffet.Data.Model;
using BreakfastBuffet.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace BreakfastBuffet.Pages;

// [Authorize("canAccessReception")]
public class Reservation : PageModel
{

  private readonly KitchenHub _messageHub;
  public ReservationModel Reservations { get; set; }
  public InputModel Input { get; set; }
  public Reservation()
  {
    _messageHub = new KitchenHub();
  }

  public class InputModel
  {
    public int ReservationsAdult { get; set; }
    public int ReservationsChild { get; set; }
    public int RoomNumber { get; set; }
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

    await _messageHub.sendMessage(Reservations, "Kitchen List");

    DbContextOptions<ApplicationDbContext> option = new DbContextOptions<ApplicationDbContext>();
    using (var context = new ApplicationDbContext(option))
    {
      context.Reservations.Add(Reservations);
      context.SaveChanges();
    }

    return LocalRedirect(returnUrl);
  }
}