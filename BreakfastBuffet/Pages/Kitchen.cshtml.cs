using BreakfastBuffet.Data;
using BreakfastBuffet.Data.Model;
using BreakfastBuffet.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Pages;

public class KitchenModel : PageModel
{
  
  [BindProperty]
  public InputModel Input { get; set; } = new InputModel();

  public class InputModel
  {
    public DateTime CurrentDate { get; set; } = DateTime.Now.Date;
  }

  public void OnGet()
  {
    DbContextOptions<ApplicationDbContext> option = new DbContextOptions<ApplicationDbContext>();
    using (var context = new ApplicationDbContext(option))
    {
      var list = context.Reservations.Where(r => r.ReservationDate.Date == Input.CurrentDate.Date).ToList();
      int totalAdult = 0;
      foreach (ReservationModel model in list)
      {
        totalAdult += model.ReservationsAdult;
      }
      
      ViewData["Adults"] = $"{totalAdult}";

    }
  }
}