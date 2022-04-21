using BreakfastBuffet;
using BreakfastBuffet.Data;
using BreakfastBuffet.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();;

// Add services to the container.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthorization(option =>
{
  option.AddPolicy(
    "canAccessReception",
    policyBuilder => policyBuilder
      .RequireClaim("IsReceptionist")
    );
  option.AddPolicy(
    "canAccessCheckIn",
    policyBuilder => policyBuilder
      .RequireClaim("IsWaiter"));
});

builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<KitchenHub>("/kitchenHub");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
    if (userManager != null)
        SeedDB.SeedUsers(userManager);
    else throw new Exception("Unable to get UserManager!");
}


app.Run();
