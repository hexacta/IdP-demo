using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

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
    },
    new Client
    {
        ClientId = "userInteractive",
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        AllowedScopes = 
        { 
            IdentityServerConstants.StandardScopes.OpenId,
            "api1"
        }
    }
});

isBuilder.AddInMemoryApiScopes(new List<ApiScope>
{
    new ApiScope("api1", "MyApi")
});

isBuilder.AddInMemoryIdentityResources(new List<IdentityResource> 
{ 
    new IdentityResources.OpenId() 
});

isBuilder.AddTestUsers(new List<TestUser>
{ 
    new TestUser
    {
        Username = "bob",
        Password = "password",
        SubjectId = "12345",
        IsActive = true,
        Claims = { new Claim("myClaim","myClaim") }
    }
});

isBuilder.AddDeveloperSigningCredential();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseIdentityServer();

app.Run();

