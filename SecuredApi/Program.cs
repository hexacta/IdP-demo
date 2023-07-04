using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "app");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
var accounts = new Dictionary<Guid,int>()
{
    { Guid.NewGuid(), 10 },
    { Guid.NewGuid(), 20 }
};

app.MapGet("/overview", () =>
{
    return $"There are {accounts.Count} accounts totaling ${accounts.Sum(x => x.Value)}";
}).RequireAuthorization("M2M");

app.Run();

