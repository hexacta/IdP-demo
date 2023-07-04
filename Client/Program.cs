using IdentityModel.Client;

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:4000");
if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "machine2machine",
    ClientSecret = "secret"
});

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);
Console.WriteLine("\n\n");

var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken);

var response = await apiClient.GetAsync("https://localhost:4001/overview");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}