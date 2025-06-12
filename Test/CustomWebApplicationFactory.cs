using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Configurazioni test specifiche, es. InMemory DB
        });

        builder.ConfigureServices(services =>
        {
            // Puoi mockare servizi qui
        });


    }

    public HttpClient CreateExternalClient()
    {
        var client = CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost:8001")
        });

        return client;
    }
}