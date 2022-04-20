using Microsoft.AspNetCore.Identity;

namespace BreakfastBuffet.Data.Model;

public class User : IdentityUser<int>
{
  public bool IsWaiter { get; set; }
  public bool IsReceptionist { get; set; }
}