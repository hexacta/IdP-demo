using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
{
    opt.Authority = "https://localhost:4000";
    opt.ClientId = "userInteractive";
    opt.ClientSecret = "secret";
    opt.ResponseType = "code";
    opt.Scope.Add("user");
    opt.SaveTokens = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.MapDefaultControllerRoute().RequireAuthorization();

app.Run();
