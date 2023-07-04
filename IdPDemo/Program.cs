using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var isBuilder = builder.Services.AddIdentityServer();
isBuilder.AddInMemoryClients(new List<Client>
{
    new Client
    {
        ClientId = "machine2machine",
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedGrantTypes = GrantTypes.ClientCredentials,
        AllowedScopes = { "app" }
    },
    new Client
    {
        ClientId = "userInteractive",
        ClientSecrets = { new Secret("secret".Sha256()) },
        AllowedGrantTypes = GrantTypes.Code,
        AllowedScopes =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            "user"
        },
        RedirectUris =
        {
            "https://localhost:4002/signin-oidc"
        }
    }
});

isBuilder.AddInMemoryApiScopes(new List<ApiScope>
{
    new ApiScope("app", "app"),
    new ApiScope("user", "user")
});

isBuilder.AddInMemoryIdentityResources(new List<IdentityResource> 
{ 
    new IdentityResources.OpenId(),
    new IdentityResources.Profile()
});


isBuilder.AddTestUsers(new List<TestUser>
{ 
    new TestUser
    {
        Username = "bob",
        Password = "password",
        SubjectId = "4145314a-06b3-4aa8-853b-666719d916c7",
        IsActive = true
    }
});

isBuilder.AddDeveloperSigningCredential();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseIdentityServer();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();

