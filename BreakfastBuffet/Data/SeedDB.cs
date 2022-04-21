using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace BreakfastBuffet.Data
{
    public class SeedDB
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            const string ReceptionistEmail = "Recep@localhost.com";
            const string ReceptionistPassword = "Secret7$";

            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager));
            if (userManager.FindByNameAsync(ReceptionistEmail).Result == null)
            {
                var user = new IdentityUser();
                user.UserName = ReceptionistEmail;
                user.Email = ReceptionistEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, ReceptionistPassword).Result;

                if (result.Succeeded)
                {
                    var receptionistUser = userManager.FindByNameAsync(ReceptionistEmail).Result;
                    var claim = new Claim("IsReceptionist", "true");
                    var claimAdded = userManager.AddClaimAsync(receptionistUser, claim).Result;
                }
            }

            const string WaiterEmail = "Waiter@localhost.com";
            const string WaiterPassword = "Secret8$";

            if (userManager.FindByNameAsync(WaiterEmail).Result == null)
            {
                var user = new IdentityUser();
                user.UserName = WaiterEmail;
                user.Email = WaiterEmail;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, WaiterPassword).Result;

                if (result.Succeeded)
                {
                    var waiterUser = userManager.FindByNameAsync(WaiterEmail).Result;
                    var claim = new Claim("IsWaiter", "true");
                    var claimAdded = userManager.AddClaimAsync(waiterUser, claim).Result;
                }
            }
        }

    }
}

