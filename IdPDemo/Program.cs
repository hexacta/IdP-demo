using IdentityServer4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var isBuilder = builder.Services.AddIdentityServer();
isBuilder.AddInMemoryClients(new List<Client>
{
    new Client
    {
        ClientId = "machine2machine",
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        AllowedScopes = { "api1" }
    }
});
isBuilder.AddInMemoryApiScopes(new List<ApiScope>
{
    new ApiScope("api1", "MyApi")
});
isBuilder.AddDeveloperSigningCredential();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseIdentityServer();

app.Run();

