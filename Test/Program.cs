using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Register services (e.g. controllers, db contexts, DI, etc.)
builder.Services.AddControllers();
// builder.Services.AddDbContext<...>();
// builder.Services.AddScoped<...>();

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
public partial class Program { }
