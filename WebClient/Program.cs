using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
{
    opt.Authority = "https://localhost:4000";
    opt.ClientId = "userInteractive";
    opt.ClientSecret = "secret";
    opt.ResponseType = "code";
    opt.Scope.Add("api1");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
