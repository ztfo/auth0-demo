using auth0_demo;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// DB Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0Domain"]}";
    options.ClientId = builder.Configuration["Auth0ClientId"];
    options.ClientSecret = builder.Configuration["Auth0ClientSecret"];
    options.ResponseType = "code";
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.CallbackPath = "/Account/Callback";
    options.SignedOutCallbackPath = "/Home/Index";

    options.Events = new OpenIdConnectEvents
    {
        OnTicketReceived = async context =>
        {
            var db = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

            var auth0Id = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await db.Users.FirstOrDefaultAsync(u => u.Auth0ID == auth0Id);

            if (user != null)
            {
                // Get the role from Auth0
                var role = context.Principal.FindFirstValue(ClaimTypes.Role);

                // Update the user's role in the database
                user.Role = role;

                await db.SaveChangesAsync();
            }
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseStaticFiles();

app.Run();