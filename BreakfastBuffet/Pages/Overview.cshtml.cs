using BreakfastBuffet.Data;
using BreakfastBuffet.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Pages;

public class OverviewModel : PageModel
{
  [BindProperty]
  public List<(int, int, int)> CheckInList { get; set; } = new List<(int, int, int)>();
  public void OnGet()
  {
    var option = new DbContextOptions<ApplicationDbContext>();
    using (var context = new ApplicationDbContext(option))
    {

      var list = context.Reservations.Where(r => r.ReservationDate.Date == DateTime.Now.Date).ToList();
      var checkedInAdult = new List<int>();
      var checkedInChildren = new List<int>();
      var roomNumber = new List<int>();

      foreach (ReservationModel model in list)
      {
        if (model.AttendingAdults != 0 || model.AttendingChildren != 0)
        {
          CheckInList.Add((model.RoomNumber, model.AttendingAdults, model.AttendingChildren));
        }
      }

    }
  }
}