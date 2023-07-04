using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = "https://localhost:4000";
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("M2M", policy =>
    {
        policy.RequireAuthenticatedUser()
            .RequireClaim("scope", "app");
    });
    opt.AddPolicy("Interactive", policy =>
    {
        policy.RequireAuthenticatedUser()
            .RequireClaim("scope", "user");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
var accounts = new Dictionary<string, int>()
{
    { "4145314a-06b3-4aa8-853b-666719d916c7", 10 },
    { "10781461-5950-4f85-a25b-2e56b8aefed9", 20 }
};

app.MapGet("/overview", () =>
{
    return $"There are {accounts.Count} accounts totaling ${accounts.Sum(x => x.Value)}";
}).RequireAuthorization("M2M");

app.MapGet("/account", (HttpContext ctx) =>
{
    var sub = ctx.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    return $"Your balance is ${accounts[sub]}";
}).RequireAuthorization("Interactive");
app.Run();

