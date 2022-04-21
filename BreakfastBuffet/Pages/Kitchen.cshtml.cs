using BreakfastBuffet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BreakfastBuffet.Pages;

public class KitchenModel : PageModel
{
  public DateTime Date { get; set; } = DateTime.Now;


  public void OnGet()
  {
    DbContextOptions<ApplicationDbContext> option = new DbContextOptions<ApplicationDbContext>();
    using (var context = new ApplicationDbContext(option))
    {
      
    }
  }
}